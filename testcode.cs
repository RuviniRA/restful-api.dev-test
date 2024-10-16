using RestSharp;
using Newtonsoft.Json;
using System.Net;
using Xunit;

public class RestfulApiTests
{
    private readonly string baseUrl = "https://restful-api.dev/";
    private readonly RestClient client;

    public RestfulApiTests()
    {
        client = new RestClient(baseUrl);
    }

    [Fact]
    public async Task Get_All_Objects_Should_Return_List()
    {
        var request = new RestRequest("objects", Method.Get);
        var response = await client.ExecuteAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);

        var objects = JsonConvert.DeserializeObject<List<dynamic>>(response.Content);
        Assert.True(objects.Count > 0);
    }

    [Fact]
    public async Task Add_Object_Should_Create_New_Object()
    {
        var newObject = new { name = "Test Object", description = "This is a test." };
        var request = new RestRequest("objects", Method.Post);
        request.AddJsonBody(newObject);

        var response = await client.ExecuteAsync(request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdObject = JsonConvert.DeserializeObject<dynamic>(response.Content);

        Assert.Equal("Test Object", (string)createdObject.name);
        Assert.NotNull(createdObject.id);
    }

    [Fact]
    public async Task Get_Single_Object_Should_Return_Object_By_Id()
    {
        var newObject = new { name = "Test Object", description = "This is a test." };
        var postRequest = new RestRequest("objects", Method.Post);
        postRequest.AddJsonBody(newObject);
        var postResponse = await client.ExecuteAsync(postRequest);

        var createdObject = JsonConvert.DeserializeObject<dynamic>(postResponse.Content);
        var id = (string)createdObject.id;

        var getRequest = new RestRequest($"objects/{id}", Method.Get);
        var getResponse = await client.ExecuteAsync(getRequest);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var fetchedObject = JsonConvert.DeserializeObject<dynamic>(getResponse.Content);
        Assert.Equal(id, (string)fetchedObject.id);
    }

    [Fact]
    public async Task Update_Object_Should_Modify_Object()
    {
        var newObject = new { name = "Test Object", description = "This is a test." };
        var postRequest = new RestRequest("objects", Method.Post);
        postRequest.AddJsonBody(newObject);
        var postResponse = await client.ExecuteAsync(postRequest);

        var createdObject = JsonConvert.DeserializeObject<dynamic>(postResponse.Content);
        var id = (string)createdObject.id;

        var updatedObject = new { name = "Updated Test Object", description = "This is an updated test." };
        var putRequest = new RestRequest($"objects/{id}", Method.Put);
        putRequest.AddJsonBody(updatedObject);
        var putResponse = await client.ExecuteAsync(putRequest);

        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
        var updated = JsonConvert.DeserializeObject<dynamic>(putResponse.Content);
        Assert.Equal("Updated Test Object", (string)updated.name);
    }

    [Fact]
    public async Task Delete_Object_Should_Remove_Object()
    {
        var newObject = new { name = "Test Object", description = "This is a test." };
        var postRequest = new RestRequest("objects", Method.Post);
        postRequest.AddJsonBody(newObject);
        var postResponse = await client.ExecuteAsync(postRequest);

        var createdObject = JsonConvert.DeserializeObject<dynamic>(postResponse.Content);
        var id = (string)createdObject.id;

        var deleteRequest = new RestRequest($"objects/{id}", Method.Delete);
        var deleteResponse = await client.ExecuteAsync(deleteRequest);

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getRequest = new RestRequest($"objects/{id}", Method.Get);
        var getResponse = await client.ExecuteAsync(getRequest);

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
