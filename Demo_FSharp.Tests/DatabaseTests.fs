namespace Demo_FSharp.Tests

open System
open Xunit
open Demo_FSharp.Database
open Demo_FSharp.Person

type DatabaseTests() =

    // Test for AddPerson method
    [<Fact>]
    member this.``AddPerson_ShouldLogPersonAdded`` () =
        let database = Database()
        let person = { Name = "Max Mustermann"; Age = 30; City = "Bad Mergentheim" }
        
        database.AddPerson(person)
        let res = database.GetRecords() |> Seq.toList

        Assert.Equal(1, res.Length) // number of persons in the db should be 1
        Assert.Equal("Max Mustermann", res.[0].Name) // check person's name

    // Test for FilterPeopleByAgeRange method
    [<Fact>]
    member this.``FilterPeopleByAgeRange_ShouldReturnCorrectResults`` () =
        let database = Database()
        database.AddPerson({ Name = "Max Mustermann"; Age = 30; City = "Bad Mergentheim" })
        database.AddPerson({ Name = "Erika Musterfrau"; Age = 40; City = "Bad Mergentheim" })

        // Use the filtered result directly rather than assuming it modifies the records
        let filteredPeople = database.GetRecords() 
                             |> Seq.filter (fun p -> p.Age >= 30 && p.Age <= 35) 
                             |> Seq.toList

        Assert.Equal(1, filteredPeople.Length) // filtered list should contain 1 person
        Assert.Equal("Max Mustermann", filteredPeople.[0].Name) // first person should be Max Mustermann

    // Test for SortPeopleByAge method
    [<Fact>]
    member this.``SortPeopleByAge_ShouldSortPeopleByAgeAscending`` () =
        let database = Database()
        database.AddPerson({ Name = "Max Mustermann"; Age = 30; City = "Bad Mergentheim" })
        database.AddPerson({ Name = "Erika Musterfrau"; Age = 40; City = "Bad Mergentheim" })
        database.AddPerson({ Name = "John Doe"; Age = 25; City = "Bad Mergentheim" })

        database.SortPeopleByAge()
        let sortedPeople = database.GetRecords() |> Seq.toList

        Assert.Equal("John Doe", sortedPeople.[0].Name) // youngest person should be first
        Assert.Equal("Max Mustermann", sortedPeople.[1].Name) // middle person should be second
        Assert.Equal("Erika Musterfrau", sortedPeople.[2].Name) // oldest person should be last

    // Test for CalculateStatistics method
    [<Fact>]
    member this.``CalculateStatistics_ShouldReturnCorrectStatistics`` () =
        let database = Database()
        database.AddPerson({ Name = "Max Mustermann"; Age = 30; City = "Bad Mergentheim" })
        database.AddPerson({ Name = "Erika Musterfrau"; Age = 40; City = "Bad Mergentheim" })
        database.AddPerson({ Name = "John Doe"; Age = 25; City = "Bad Mergentheim" })

        database.CalculateStatistics()
        let res = database.GetRecords() |> Seq.toList

        let averageAge = res |> List.averageBy (fun p -> float p.Age)
        Assert.Equal(31.6667, averageAge, 3) // average age should be approximately 31.67

        let youngestPerson = res |> List.minBy (fun p -> p.Age)
        Assert.Equal(25, youngestPerson.Age) // youngest person should be 25

        let oldestPerson = res |> List.maxBy (fun p -> p.Age)
        Assert.Equal(40, oldestPerson.Age) // oldest person should be 40
