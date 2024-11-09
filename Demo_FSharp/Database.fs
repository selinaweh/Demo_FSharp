// Database.fs
namespace Demo_FSharp

open System
open System.IO
open System.Collections.Generic
open Demo_FSharp.Person

module Database =

    // Represents a database for managing Person objects
    type Database() =
        // Private list to store Person records
        let mutable records = List<Person>() // usage of Person type from Person module

        // Adds a person to the records list after validating input values
        member this.AddPerson(person: Person) =
            // Validate that the person's Name, Age, and City are correct
            if String.IsNullOrWhiteSpace(person.Name) then
                Console.WriteLine("Error: Name cannot be empty.")
            elif person.Age <= 0 then
                Console.WriteLine("Error: Age must be a positive number.")
            elif String.IsNullOrWhiteSpace(person.City) then
                Console.WriteLine("Error: City cannot be empty.")
            else
                // Add the person to the list if all inputs are valid
                records.Add(person)
                Console.WriteLine($"Person added: {person.Name}, {person.Age}, {person.City}")

        // Asynchronously saves all records to a file
        member this.SaveToFileAsync(fileName: string) =
            async {
                // Check if there is any data to save
                if records.Count = 0 then
                    raise (Exception("No data available to save."))
                else
                    // Format each person record as a CSV line (Name, Age, City)
                    let lines = records |> Seq.map (fun p -> $"{p.Name},{p.Age},{p.City}") |> Seq.toArray
                    do! File.WriteAllLinesAsync(fileName, lines) |> Async.AwaitTask
                    Console.WriteLine($"Data saved to file {fileName} asynchronously.")
            } |> Async.StartAsTask

        // Asynchronously loads records from a file
        member this.LoadFromFileAsync(fileName: string) =
            async {
                // Check if the file exists before attempting to load data
                if File.Exists(fileName) then
                    let! lines = File.ReadAllLinesAsync(fileName) |> Async.AwaitTask
                    records.Clear()
                    for line in lines do
                        let parts = line.Split(',')
                        records.Add(Person(parts.[0], int parts.[1], parts.[2]))
                    Console.WriteLine($"Data loaded from file {fileName} asynchronously.")
                else
                    Console.WriteLine("File not found.")
            } |> Async.StartAsTask

        // Displays all people in the records list
        member this.ListPeople() =
            if records.Count = 0 then
                Console.WriteLine("No records found.")
            else
                records |> Seq.iter (fun person -> Console.WriteLine($"{person.Name}, {person.Age}, {person.City}"))

        // Sorts people in the records list by age in ascending order
        member this.SortPeopleByAge() =
            records.Sort(fun a b -> a.Age.CompareTo(b.Age))
            Console.WriteLine("People sorted by Age.")

        // Filters people within a specified age range and displays the results
        member this.FilterPeopleByAgeRange(minAge: int, maxAge: int) =
            let filteredPeople = records |> Seq.filter (fun p -> p.Age >= minAge && p.Age <= maxAge)
            if Seq.isEmpty filteredPeople then
                Console.WriteLine("No people found in this age range.")
            else
                filteredPeople |> Seq.iter (fun person -> Console.WriteLine($"{person.Name}, {person.Age}, {person.City}"))

        // Calculates statistics like average age, oldest, and youngest person
        member this.CalculateStatistics() =
            if records.Count = 0 then
                Console.WriteLine("No data available for statistics.")
            else
                // Calculate average age and find the oldest and youngest person
                let averageAge = records |> Seq.averageBy (fun p -> float p.Age)
                let oldestPerson = records |> Seq.maxBy (fun p -> p.Age)
                let youngestPerson = records |> Seq.minBy (fun p -> p.Age)

                // Display the calculated statistics
                Console.WriteLine($"Average Age: {averageAge:F2}")
                Console.WriteLine($"Oldest Person: {oldestPerson.Name}, Age: {oldestPerson.Age}")
                Console.WriteLine($"Youngest Person: {youngestPerson.Name}, Age: {youngestPerson.Age}")

        // Searches for people based on the initial character of their name
        member this.SearchByInitial(initial: char) =
            let filteredPeople = records |> Seq.filter (fun p -> p.Name.StartsWith(initial.ToString(), StringComparison.OrdinalIgnoreCase))
            if Seq.isEmpty filteredPeople then
                Console.WriteLine($"No people found with initial: {initial}")
            else
                filteredPeople |> Seq.iter (fun person -> Console.WriteLine($"{person.Name}, {person.Age}, {person.City}"))

        // Retrieves the list of all Person objects in the records
        member this.GetRecords() =
            records
