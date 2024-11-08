// Person.fs
namespace Demo_FSharp

// Module Person
module Person =

    // Represents a person with a name, age, and city
    type Person(name: string, age: int, city: string) =
        // Public properties to store the name, age, and city of the person
        member val Name = name with get, set
        member val Age = age with get, set
        member val City = city with get, set
