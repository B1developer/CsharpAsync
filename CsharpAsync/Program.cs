class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting task examples...\n");

        // SIMPLE ASYNC METHOD
        Console.WriteLine("=== SIMPLE ASYNC METHOD ===");
        await PerformTaskAsync();
        Console.WriteLine();

        // TASK AND TASK<T>
        Console.WriteLine("=== TASK AND TASK<T> ===");
        await TaskAndTaskT(args);
        Console.WriteLine();

        // HANDLING EXCEPTIONS IN ASYNC CODE
        Console.WriteLine("=== HANDLING EXCEPTIONS IN ASYNC CODE ===");
        await HandlingExceptions(args);
        Console.WriteLine();

        // SYNCHRONOUS FILE READ
        Console.WriteLine("=== SYNCHRONOUS FILE READ ===");
        SynchronousFile(args);
        Console.WriteLine();

        // ASYNCHRONOUS FILE READ
        Console.WriteLine("=== ASYNCHRONOUS FILE READ ===");
        await AsynchronousFile(args);
        Console.WriteLine();

        // TASK.WHENALL WITH MULTIPLE TASKS
        Console.WriteLine("=== TASK.WHENALL WITH MULTIPLE TASKS ===");
        await TaskWhenAll(args);
    }

    #region SIMPLE ASYNC METHOD
    static async Task PerformTaskAsync()
    {
        Console.WriteLine("Task in progress...");
        await Task.Delay(2000);  // Simulates a long-running operation
        Console.WriteLine("Task finished after 2 seconds.");
    }
    #endregion

    #region TASK AND TASK<T>
    static async Task TaskAndTaskT(string[] args)
    {
        int sumResult = await ComputeSumAsync(5, 10);  // Task<T> returns a result
        Console.WriteLine($"\nThe sum is: {sumResult}");

        await PrintMessageAsync();  // Task doesn't return a result
    }

    static async Task<int> ComputeSumAsync(int a, int b)
    {
        await Task.Delay(1000);  // Simulates a long-running operation
        return a + b;  // Returns the result of the async operation
    }

    static async Task PrintMessageAsync()
    {
        await Task.Delay(500);  // Another async task
        Console.WriteLine("This is an async message.");
    }
    #endregion

    #region HANDLING EXCEPTIONS IN ASYNC CODE
    static async Task HandlingExceptions(string[] args)
    {
        try
        {
            await DivideAsync(10, 0);  // This will throw an exception
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught an exception: {ex.Message}");
        }
    }

    static async Task DivideAsync(int a, int b)
    {
        await Task.Delay(500);  // Simulate some delay
        if (b == 0)
            throw new DivideByZeroException("You cannot divide by zero!");

        Console.WriteLine($"Result: {a / b}");
    }
    #endregion

    #region SYNCHRONOUS FILE READ
    static void SynchronousFile(string[] args)
    {
        string filePath = "example.txt"; // Change with your own example.txt PATH
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);  // Synchronous file read
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
    #endregion

    #region ASYNCHRONOUS FILE READ
    static async Task AsynchronousFile(string[] args)
    {
        string filePath = "example.txt"; // Change with your own example.txt PATH
        if (File.Exists(filePath))
        {
            string content = await File.ReadAllTextAsync(filePath);  // Asynchronous file read
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
    #endregion

    #region TASK.WHENALL WITH MULTIPLE TASKS
    static async Task TaskWhenAll(string[] args)
    {
        // Links are safe, you can check it in if you want 
        string[] urls =
        {
            "https://norvig.com/big.txt",
            "https://upload.wikimedia.org/wikipedia/commons/4/47/PNG_transparency_demonstration_1.png",
            "https://www.w3schools.com/xml/note.xml"
        };

        using HttpClient client = new HttpClient();

        Task<string>[] downloadTasks = new Task<string>[urls.Length];

        for (int i = 0; i < urls.Length; i++)
        {
            downloadTasks[i] = client.GetStringAsync(urls[i]);  // Start each download asynchronously
        }

        // Wait for all downloads to complete
        string[] results = await Task.WhenAll(downloadTasks);
        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"\nDownloaded file {i + 1}: {results[i].Substring(0, 100)}..."); // Print the first 100 characters of each download
        }
    }
    #endregion
}
