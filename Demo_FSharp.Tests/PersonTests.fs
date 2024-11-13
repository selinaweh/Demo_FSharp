namespace Demo_FSharp.Tests

open Xunit
open Demo_FSharp.Person


type PersonTests() =

    // Test for the creation of the Person record
    [<Fact>]
    member this.``Constructor_ShouldInitializeProperties`` () =
        // Arrange: Create a new Person record
        let person = { Name = "Jane Doe"; Age = 25; City = "Berlin" }

        // Act: Record initialization happens in the creation step, no explicit constructor

        // Assert: Verify the person's properties
        Assert.Equal("Jane Doe", person.Name)
        Assert.Equal(25, person.Age)
        Assert.Equal("Berlin", person.City)

