using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

List<MethodObject> methodInfoList = GetMethods();

Dictionary<string, string> methodCodeDictionary = methodInfoList.
    ToDictionary(method => method.Name, method => method.Code);

int i = 1;
Dictionary<int, string> methodNameDictionary = methodInfoList.
    Select(methodCodeDictionary => new {Key = i++, Value = methodCodeDictionary.Name }).
    ToDictionary(x => x.Key, x => x.Value);


Dictionary<int, Action> methodDictionary = new Dictionary<int, Action>
        {
            { 1, ExamplePlainThreads },
            { 2, ExampleThreadPool },
            { 3, ExampleTasks },
            { 4, ExampleWaitAll },
            { 5, ExampleContinuationsContinueWith },
            { 6, ExampleContinuationsContinueWhenAll },
            { 7, ExampleChildTasks },
            { 8, ExampleTaskFromResult },
            { 9, ExampleCancellingTasks },
            { 10, ExampleAggregateException },
            { 11, ExampleMultipleContinuations },
            { 12, ExampleLockSynchronization },
            { 13, ExampleAsyncAndAwait },
            { 15, MethodToJson },
        };

string userInput = "";

do
{
    Console.WriteLine();
    Console.WriteLine("Select a method to run or type 'exit' to quit");
    Console.WriteLine();

    foreach (var kvp in methodDictionary)
    {
        Console.WriteLine($"{kvp.Key}. {GetMethodNameByKey(kvp.Key,methodNameDictionary)}");
    }

    Console.WriteLine();

    userInput = Console.ReadLine();

    if (int.TryParse(userInput, out int choice) && methodDictionary.ContainsKey(choice))
    {
        if (!methodNameDictionary.ContainsKey(choice))
            Console.WriteLine("Can't print code.");
        else
        {
            string codeName = methodNameDictionary.GetValueOrDefault(choice);
            PrintMethodBody(codeName, methodCodeDictionary);
        }
        if(choice == 15)
        {
            MethodToJson();
        }

        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("Executing...");
        Console.WriteLine("");

        methodDictionary[choice].Invoke();

        Thread.Sleep(5000);
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------");
    }
    else
    {
        if(userInput != "exit")
            Console.WriteLine("Invalid choice.");
    }
} while (userInput != "exit");

static Dictionary<int, MethodInfo> GetMethodInfos()
{
    Dictionary<int, MethodInfo> methodDictionary = new Dictionary<int, MethodInfo>();
    MethodInfo[] methods = typeof(Program).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);

    int index = 1;
    foreach (MethodInfo method in methods)
    {
        methodDictionary.Add(index++, method);
    }

    return methodDictionary;
}

static List<MethodObject> GetMethods()
{
    return JsonToObject("methods.json");
}

static string GetMethodName(Action action)
{
    return action.Method.Name;
}
static string GetMethodNameByKey(int key, Dictionary<int, string> methodNameDictionary)
{
    if (!methodNameDictionary.ContainsKey(key))
        return "name not found";

    return methodNameDictionary.GetValueOrDefault(key);
}

static void PrintMethodBody(string methodName, Dictionary<string, string> methodCodeDictionary)
{
    var originalColor = Console.ForegroundColor;
    Console.WriteLine("-------------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Method: {methodName}");
    Console.WriteLine();

    if(methodCodeDictionary.TryGetValue(methodName, value: out string methodBody))
    {
        Console.WriteLine($"Method Body:\n{methodBody}");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Method body not available.");
    }

    Console.ForegroundColor = originalColor;
}


static void PrintPluses(int n)
{
    Console.WriteLine("\nPrintPluses thread's ID: " + Thread.CurrentThread.ManagedThreadId);
    for (int i = 0; i < n; i++)
    {
        Console.Write("+");
    }
}

static void PrintMinuses(int n)
{
    Console.WriteLine("\nPrintMinuses thread's ID: " + Thread.CurrentThread.ManagedThreadId);
    for (int i = 0; i < n; i++)
    {
        Console.Write("-");
    }
}

static void PrintA(object obj)
{
    Console.Write("A");
}

static int CalculateLength(string input)
{
    Console.WriteLine("Starting the CalculateLength method");
    Thread.Sleep(2000);
    return input.Length;
}

static async Task<int> CalculateLengthAsync(string input)
{
    Console.WriteLine("Starting the CalculateLength method");
    await Task.Delay(2000);
    return input.Length;
}



static float Divide(int? a, int? b)
{
    if (a is null || b is null)
    {
        throw new ArgumentNullException("Arguments cannot be null");
    }
    if (b == 0)
    {
        throw new DivideByZeroException("Division by zero is not allowed.");
    }

    return a.Value / (float)b.Value;
}

static async Task Process(string input)
{
    try
    {
        var length = await CalculateLengthAsync(input);
        //control will go back to caller until the Calculate
        //LengthAsync is completed

        //Once complete then PrintAsync will be invoked
        await PrintAsync(length);
        //control will again go back to caller until PrintAsync is done
        Console.WriteLine("The process is finished.");
    }
    catch (NullReferenceException ex)
    {
        Console.WriteLine("The input can't be null.");
    }
}

static void Print(int result)
{
    Console.WriteLine("Starting the Print method");
    Thread.Sleep(2000);
    Console.WriteLine("The result is " + result);
}

static async Task PrintAsync(int result)
{
    Console.WriteLine("Starting the Print method");
    await Task.Delay(2000);
    Console.WriteLine("The result is " + result);
}

//HttpClient
async Task<IEnumerable<Datum>> GetQuotes(int limit, int page)
{
    using var httpClient = new HttpClient();

    var endpoint = $"https://quote-garden.onrender.com/api/v3/quotes?limit={limit}&page={page}";

    HttpResponseMessage response = await httpClient.GetAsync(endpoint);
    response.EnsureSuccessStatusCode();
    string json = await response.Content.ReadAsStringAsync();

    var root = JsonSerializer.Deserialize<Root>(json);

    return root.data;
}

static void ExamplePlainThreads()
{
    Console.WriteLine("Cores count: " + Environment.ProcessorCount);
    Console.WriteLine("Main thread's ID: " + Thread.CurrentThread.ManagedThreadId);

    Thread thread1 = new Thread(() => PrintPluses(1000));
    Thread thread2 = new Thread(() => PrintMinuses(1000));

    thread1.Start();
    thread2.Start();
}

static void ExampleThreadPool()
{
    const int iterations = 1000;
    Stopwatch stopwatch = Stopwatch.StartNew();

    for (int i = 0; i < iterations; i++)
    {
        ThreadPool.QueueUserWorkItem(PrintA);
    }
    stopwatch.Stop();
    Console.WriteLine("Took: " + stopwatch.ElapsedMilliseconds);
}
static void ExampleTasks()
{
    Task task1 = Task.Run(() => PrintPluses(1000));
    Task task2 = Task.Run(() => PrintMinuses(1000));
}

static void ExampleWaitAll()
{
    var task3 = Task.Run(() =>
    {
        Thread.Sleep(1000);
        Console.WriteLine("Task 1 is finished.");
    });
    var task4 = Task.Run(() =>
    {
        Thread.Sleep(1000);
        Console.WriteLine("Task 2 is finished.");
    });

    Task.WaitAll(task3, task4);
}

static void ExampleContinuationsContinueWith()
{
    Task taskContinuation =
        Task.Run(() => CalculateLength("Hello there"))
        .ContinueWith(taskWithResult =>
        Console.WriteLine("Length is " + taskWithResult.Result))
        .ContinueWith(completedTask =>
        {
            Thread.Sleep(500);
            Console.WriteLine("The second continuation.");
        });


}
static void ExampleContinuationsContinueWhenAll()
{
    var tasks = new[]
  {
    Task.Run(() => CalculateLength("hello there")),
    Task.Run(() => CalculateLength("hi")),
    Task.Run(() => CalculateLength("hola")),
  };

    var continuationTask = Task.Factory.ContinueWhenAll(
        tasks,
        completedTasks => Console.WriteLine(
            string.Join(", ", completedTasks.Select(task => task.Result))));
}

static void ExampleChildTasks()
{
    //child tasks
    var parent = Task.Run(() =>
    {
        Console.WriteLine("Parent task executing.");

        var child = Task.Run(() =>
        {
            Console.WriteLine("Child task starting.");
        });
    });
}

static void ExampleTaskFromResult()
{
    Task<int> taskFromResult = Task.FromResult(10);
}

static void ExampleCancellingTasks()
{
    var cancellationTokenSource = new CancellationTokenSource();
    var task = Task.Run(
        () => NeverendingMethod(cancellationTokenSource),
        cancellationTokenSource.Token)
        .ContinueWith(canceledTask =>
            Console.WriteLine($"Task with ID {canceledTask.Id} has been canceled."),
            TaskContinuationOptions.OnlyOnCanceled);
}
static void NeverendingMethod(CancellationTokenSource cancellationTokenSource)
{
    while (true)
    {
        cancellationTokenSource.Token.ThrowIfCancellationRequested();
        Console.WriteLine("Working...");
        Thread.Sleep(1500);
    }
}

static void ExampleAggregateException()
{
    var taskThatMayFault = Task.Run(() => Divide(2, 0))
        .ContinueWith(
        faultedTask =>
        {
            faultedTask.Exception.Handle(ex =>
            {
                Console.WriteLine("Division task finished");
                if (ex is ArgumentNullException)
                {
                    Console.WriteLine("Arguments can't be null.");
                    return true;
                }
                if (ex is DivideByZeroException)
                {
                    Console.WriteLine("Can't divide by zero.");
                    return true;
                }
                Console.WriteLine("Unexpected exception type.");
                return false;
            });
        },
        TaskContinuationOptions.OnlyOnFaulted);
}

static void ExampleMultipleContinuations()
{
    var taskWithMultipleContinuations = new Task(() => Divide(10, 2));

    taskWithMultipleContinuations.ContinueWith(faultedTask =>
        Console.WriteLine("Success"),
        TaskContinuationOptions.OnlyOnRanToCompletion);

    taskWithMultipleContinuations.ContinueWith(faultedTask =>
        Console.WriteLine("Exception thrown: " + faultedTask.Exception.Message),
        TaskContinuationOptions.OnlyOnFaulted);

    taskWithMultipleContinuations.Start();
}

static void ExampleLockSynchronization()
{
    var counter = new Counter();

    var tasksAccessingTheSameResource = new List<Task>();

    for (int i = 0; i < 10; i++)
    {
        tasksAccessingTheSameResource.Add(Task.Run(() => counter.Increment()));
    }
    for (int i = 0; i < 10; i++)
    {
        tasksAccessingTheSameResource.Add(Task.Run(() => counter.Decrement()));
    }

    Task.WaitAll(tasksAccessingTheSameResource.ToArray());
    Console.WriteLine("Counter value is: " + counter.Value);
}

static void ExampleAsyncAndAwait()
{
    var taskFromAsyncMethod = Process(null);

    string userInput;
    do
    {
        Console.WriteLine("Enter a command:");
        userInput = Console.ReadLine();
        //process the command
    } while (userInput != "stop");
}
static List<MethodObject> JsonToObject(string jsonFilePath)
{
    string jsonText = File.ReadAllText(jsonFilePath);

    // Deserialize JSON into MethodObject
    List<MethodObject> methodObjects = JsonSerializer.Deserialize<List<MethodObject>>(jsonText);

    return methodObjects;
}
static void MethodToJson()
{
    string filePath = "methods.json";

    string methodText = @"
public override Action GetAction()
        {
            return () =>
            {              
                var task = Task.Run(
                    () => NeverendingMethod(_cancellationTokenSource),
                    _cancellationTokenSource.Token)
                    .ContinueWith(canceledTask =>
                        RaiseMessageGenerated($""\nTask with ID {canceledTask.Id} has been canceled.""),
                        TaskContinuationOptions.OnlyOnCanceled);
            };
        }
        public override void CancelTask()
        {
            _cancellationTokenSource?.Cancel();
        }
        private void NeverendingMethod(CancellationTokenSource cancellationTokenSource)
        {
            while (true)
            {
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                RaiseMessageGenerated(""Working..."");
                Thread.Sleep(1500);
            }
        }";

    var methodObject = new { code = methodText };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    string jsonString = JsonSerializer.Serialize(methodObject, options);

    // Write JSON string to file
   AppendTextToFile(filePath, jsonString);
}
static void AppendTextToFile(string filePath, string textToAppend)
{
    // Create a StreamWriter with append mode
    using (StreamWriter writer = new StreamWriter(filePath, append: true))
    {
        // Write the text to the file
        writer.WriteLine(textToAppend);
    }
}



public record Datum(
string _id,
string quoteText,
string quoteAuthor,
string quoteGenre,
int __v
   );

public record Pagination(
int currentPage,
int nextPage,
int totalPages
);

public record Root(
int statusCode,
string message,
Pagination pagination,
int totalQuotes,
IReadOnlyList<Datum> data
);

class Counter
{
    private object _valueLock = new object();
    public int Value { get; private set; }

    public void Increment()
    {
        lock (_valueLock)
        {
            Value++;
        }
    }

    public void Decrement()
    {
        lock (_valueLock)
        {
            Value--;
        }
    }
}

class MethodObject
{
    [JsonPropertyName("name")]

    public string Name { get; set; }
    [JsonPropertyName("code")]
    public string Code { get; set; }
}
