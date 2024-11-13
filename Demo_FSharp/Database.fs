// Database.fs
namespace Demo_FSharp

open System
open System.IO
open System.Collections.Generic
open Demo_FSharp.Person

module Database =
    type Database() =
        let mutable records = List<Person>() //private mutable list of Person objects

        // Adds a person to the records list after validating input values
        member this.AddPerson(person: Person) =
            if String.IsNullOrWhiteSpace(person.Name) then
                Console.WriteLine("Error: Name cannot be empty.")
            elif person.Age <= 0 then
                Console.WriteLine("Error: Age must be a positive number.")
            elif String.IsNullOrWhiteSpace(person.City) then
                Console.WriteLine("Error: City cannot be empty.")
            else
                records.Add(person)
                Console.WriteLine($"Person added: {person.Name}, {person.Age}, {person.City}")

        // Asynchronously saves all records to a file
        member this.SaveToFileAsync(fileName: string) =
            async {
                if records.Count = 0 then
                    raise (Exception("No data available to save."))
                else
                    let lines =
                        records |> Seq.map (fun p -> $"{p.Name},{p.Age},{p.City}") |> Seq.toArray

                    do! File.WriteAllLinesAsync(fileName, lines) |> Async.AwaitTask
                    Console.WriteLine($"Data saved to file {fileName} asynchronously.")
            }
            |> Async.StartAsTask

        // Asynchronously loads records from a file
        member this.LoadFromFileAsync(fileName: string) =
            async {
                if File.Exists(fileName) then
                    let! lines = File.ReadAllLinesAsync(fileName) |> Async.AwaitTask
                    records.Clear()

                    for line in lines do
                        let parts = line.Split(',')
                        records.Add({ Name = parts.[0]; Age = int parts.[1]; City = parts.[2] })

                    Console.WriteLine($"Data loaded from file {fileName} asynchronously.")
                else
                    Console.WriteLine("File not found.")
            }
            |> Async.StartAsTask

        // Displays all people in the records list
        member this.ListPeople() =
            if records.Count = 0 then
                Console.WriteLine("No records found.")
            else
                records
                |> Seq.iter (fun person -> Console.WriteLine($"{person.Name}, {person.Age}, {person.City}"))

        // Sorts people in the records list by age in ascending order
        member this.SortPeopleByAge() =
            records.Sort(fun a b -> a.Age.CompareTo(b.Age))
            Console.WriteLine("People sorted by Age.")

        // Filters people within a specified age range and displays the results
        member this.FilterPeopleByAgeRange(minAge: int, maxAge: int) =
            let filteredPeople =
                records |> Seq.filter (fun p -> p.Age >= minAge && p.Age <= maxAge)

            if Seq.isEmpty filteredPeople then
                Console.WriteLine("No people found in this age range.")
            else
                filteredPeople
                |> Seq.iter (fun person -> Console.WriteLine($"{person.Name}, {person.Age}, {person.City}"))

        // Calculates statistics like average age, oldest, and youngest person
        member this.CalculateStatistics() =
            if records.Count = 0 then
                Console.WriteLine("No data available for statistics.")
            else
                // Calculate average age and find the oldest and youngest person
                let averageAge = records |> Seq.averageBy (fun p -> float p.Age)
                let oldestPerson = records |> Seq.maxBy (_.Age)                     // _.Age is a shorthand for (fun p -> p.Age)
                let youngestPerson = records |> Seq.minBy (_.Age)

                // Display the calculated statistics
                Console.WriteLine($"Average Age: {averageAge:F2}")
                Console.WriteLine($"Oldest Person: {oldestPerson.Name}, Age: {oldestPerson.Age}")
                Console.WriteLine($"Youngest Person: {youngestPerson.Name}, Age: {youngestPerson.Age}")

        // Searches for people based on the initial character of their name
        member this.SearchByInitial(initial: char) =
            let filteredPeople =
                records
                |> Seq.filter (_.Name.StartsWith(initial.ToString(), StringComparison.OrdinalIgnoreCase))

            if Seq.isEmpty filteredPeople then
                Console.WriteLine($"No people found with initial: {initial}")
            else
                filteredPeople
                |> Seq.iter (fun person -> Console.WriteLine($"{person.Name}, {person.Age}, {person.City}"))

        // Retrieves the list of all Person objects in the records
        member this.GetRecords() = records
