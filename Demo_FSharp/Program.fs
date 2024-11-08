// Program.fs
namespace Demo_FSharp

open System
open System.Threading.Tasks
open Demo_FSharp.Database
open Demo_FSharp.Person

module Program =

    // Method to exit the program
    let ExitProgram() =
        Console.WriteLine("Exiting...")
        Environment.Exit(0)

    // Main loop to interact with the user and provide menu options
    let rec MainLoop(db: Database.Database) = task {
        while true do
            Console.WriteLine("\nSelect an option:")
            Console.WriteLine("1. Add Person")
            Console.WriteLine("2. List People")
            Console.WriteLine("3. Filter People by Age Range")
            Console.WriteLine("4. Sort People by Age")
            Console.WriteLine("5. Calculate Age Statistics")
            Console.WriteLine("6. Search by Name Initial")
            Console.WriteLine("7. Save Data to File (Async)")
            Console.WriteLine("8. Load Data from File (Async)")
            Console.WriteLine("9. Exit")

            let choice = Console.ReadLine()

            match choice with
            | "1" ->
                try
                    Console.WriteLine("Enter Name:")
                    let name = Console.ReadLine()
                    if String.IsNullOrEmpty(name) then raise (Exception("Name cannot be empty."))

                    Console.WriteLine("Enter Age:")
                    let age = Int32.Parse(Console.ReadLine())
                    if age <= 0 then raise (Exception("Age must be a positive number."))

                    Console.WriteLine("Enter City:")
                    let city = Console.ReadLine()
                    if String.IsNullOrEmpty(city) then raise (Exception("City cannot be empty."))

                    let person = Person.Person(name, age, city)
                    db.AddPerson(person)
                with
                | ex -> Console.WriteLine($"Error: {ex.Message}")

            | "2" -> db.ListPeople()
            | "3" ->
                try
                    Console.WriteLine("Enter minimum age:")
                    let minAge = Int32.Parse(Console.ReadLine())
                    Console.WriteLine("Enter maximum age:")
                    let maxAge = Int32.Parse(Console.ReadLine())
                    db.FilterPeopleByAgeRange(minAge, maxAge)
                with
                | ex -> Console.WriteLine($"Error: {ex.Message}")
            | "4" -> db.SortPeopleByAge()
            | "5" -> db.CalculateStatistics()
            | "6" ->
                try
                    Console.WriteLine("Enter initial character:")
                    let initial = Console.ReadLine().[0]
                    db.SearchByInitial(initial)
                with
                | ex -> Console.WriteLine($"Error: {ex.Message}")
            | "7" ->
                try
                    Console.WriteLine("Enter the file name to save data:")
                    let fileName = Console.ReadLine()
                    do! db.SaveToFileAsync(fileName)
                with
                | ex -> Console.WriteLine($"Error: {ex.Message}")
            | "8" ->
                try
                    Console.WriteLine("Enter the file name to load data:")
                    let fileName = Console.ReadLine()
                    do! db.LoadFromFileAsync(fileName)
                with
                | ex -> Console.WriteLine($"Error: {ex.Message}")
            | "9" -> ExitProgram()
            | _ -> Console.WriteLine("Invalid choice, try again.")
    }

    // Main async method to initialize the database and start the main loop
    let MainAsync() = task {
        let db = Database.Database() // Initialize the database
        do! MainLoop(db) // Start the main loop for user interaction
    }

    // Entry point of the program
    [<EntryPoint>]
    let main _ =
        MainAsync().GetAwaiter().GetResult()
        0
