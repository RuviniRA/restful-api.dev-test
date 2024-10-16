# Restful API Tests

This project contains tests for a simple RESTful API available at [https://restful-api.dev/]. The tests cover basic CRUD operations, including:

1. Retrieve all objects.
2. Add a new object.
3. Retrieve a specific object by its ID.
4. Update an existing object.
5. Delete an object.

## Prerequisites

- .NET SDK 6.0 or higher
- Visual Studio 2022 or higher
- Internet connection (for accessing the API)

## Running the Tests

1. Clone this repository or download the source code.
   ```bash
   git clone <https://github.com/RuviniRA/restful-api.dev-test>
   ```

2. Navigate to the project directory.
   ```bash
   cd RestfulApiTests
   ```

3. Restore dependencies.
   ```bash
   dotnet restore
   ```

4. Run the tests.
   ```bash
   dotnet test
   ```

This will execute all the tests and provide a summary of the results.
```
