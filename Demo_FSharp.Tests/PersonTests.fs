namespace Demo_FSharp.Tests

open Xunit
open Demo_FSharp.Person

type PersonTests() =

    // Test for the constructor of the Person class to ensure it initializes properties correctly
    [<Fact>]
    member this.``Constructor_ShouldInitializeProperties`` () =
        // Arrange: Create a new Person object with specific values for Name, Age, and City
        let person = Person("Jane Doe", 25, "Berlin")

        // Act: The constructor initializes the properties (this is done in the creation step)

        // Assert: Verify that the person's properties are correctly initialized
        Assert.Equal("Jane Doe", person.Name) // Verify that the Name property is set correctly
        Assert.Equal(25, person.Age) // Verify that the Age property is set correctly
        Assert.Equal("Berlin", person.City) // Verify that the City property is set correctly
