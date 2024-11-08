# People Management System

This project is a console application written in F# that allows users to manage a list of people. Users can add, list, filter, and sort people based on various criteria, such as age and name initials. Additionally, the system supports saving and loading data from a file asynchronously.

## Features

The application provides the following features:

- **Add Person**: Add a new person with their name, age, and city to the system.
- **List People**: Display all people currently stored in the system.
- **Filter People by Age Range**: Filter and display people within a specified age range.
- **Sort People by Age**: Sort the people by their age in ascending order and display them.
- **Calculate Age Statistics**: Calculate and display statistics like the average age, the oldest person, and the youngest person.
- **Search by Name Initial**: Search for people whose name starts with a specific character.
- **Save Data to File (Async)**: Save the list of people to a file asynchronously.
- **Load Data from File (Async)**: Load the list of people from a file asynchronously.
- **Exit**: Exit the program.

## Usage

When the program is run, it presents a menu with options. The user selects an option by entering the corresponding number.

### Example Workflow

1. **Add Person**:
   - The user is prompted to enter the person’s name, age, and city.
   - The data is validated, and if correct, the person is added to the system.

2. **List People**:
   - Displays all stored records.

3. **Filter People by Age Range**:
   - Prompts for minimum and maximum age and shows people within that range.

4. **Sort People by Age**:
   - Sorts and displays people by age in ascending order.

5. **Calculate Age Statistics**:
   - Calculates and shows the average age, youngest, and oldest person.

6. **Search by Name Initial**:
   - Searches and lists people whose names start with a specified character.

7. **Save Data to File (Async)** and **Load Data from File (Async)**:
   - Saves or loads data asynchronously, based on a provided file name.

8. **Exit**:
   - Exits the program.

## Installation

To run this application, follow these steps:

1. Ensure you have .NET SDK installed.
2. Clone or download this repository to your local machine.
3. Open the project in a .NET-compatible IDE (e.g., Visual Studio or JetBrains Rider) and build the solution.
4. Run the application.

## Dependencies

- .NET SDK is required to compile and run this application.
- The application uses `System.IO` for file operations and `System.Linq` for LINQ functionality.

## Code Structure

The project consists of the following main files:

- **Program.fs**: The entry point of the application, containing the main menu and calls to database functions based on user input.
- **Person.fs**: Defines the `Person` class with properties `Name`, `Age`, and `City`.
- **Database.fs**: Manages the list of people, providing methods to add, list, filter, sort, save, and load data.

### Example Menu

```plaintext
Select an option:
1. Add Person
2. List People
3. Filter People by Age Range
4. Sort People by Age
5. Calculate Age Statistics
6. Search by Name Initial
7. Save Data to File (Async)
8. Load Data from File (Async)
9. Exit
```

## Tests

This project includes unit tests for the `Database` and `Person` classes to ensure functionality and correctness. The tests are written using `xUnit` and validate that the system behaves as expected in different scenarios.

### Running the Tests

To run the tests, ensure the following NuGet packages are installed:

- **xUnit**: The test framework used to write and execute the tests.
    ```shell
    dotnet add Demo_FSharp.Tests package xunit
    ```

- **xUnit.runner.visualstudio**: Allows Visual Studio and other .NET test tools to recognize and run xUnit tests.
    ```shell
    dotnet add Demo_FSharp.Tests package xunit.runner.visualstudio
    ```

- **Microsoft.NET.Test.Sdk**: Required for running tests from the .NET CLI and Visual Studio.
    ```shell
    dotnet add Demo_FSharp.Tests package Microsoft.NET.Test.Sdk
    ```

### Step 1: Run Tests in the IDE

1. Open the test explorer in your IDE (e.g., Visual Studio).
2. All tests should appear in the Test Explorer.
3. Run tests by clicking "Run All" or selecting specific tests.

### Step 2: Run Tests via the .NET CLI

To run the tests from the command line, navigate to the project folder where the tests are located and run:

```shell
dotnet test
