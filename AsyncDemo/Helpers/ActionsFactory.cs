using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo.Helpers
{
    public class ActionsFactory
    {
        private readonly Dictionary<int, IActionFactory> _factories = new Dictionary<int, IActionFactory>();
        public Dictionary<int, IActionFactory> Actions { get { return _factories; } }

        public ActionsFactory()
        {
            // Initialize dictionary with factories
            _factories.Add(1, new PlainThreadsFactory());
            _factories.Add(2, new ThreadPoolFactory());
            _factories.Add(3, new TasksFactory());
            _factories.Add(4, new ExampleWaitAllFactory());
            _factories.Add(5, new ExampleContinuationsContinueWithFactory());
            _factories.Add(6, new ExampleContinuationsContinueWhenAllFactory());
            _factories.Add(7, new ExampleChildTasksFactory());
            _factories.Add(8, new ExampleTaskFromResultFactory());
            _factories.Add(9, new ExampleCancellingTasksFactory());
            _factories.Add(10, new ExampleAggregateExceptionFactory());
            _factories.Add(11, new ExampleMultipleContinuationsFactory());
            _factories.Add(12, new ExampleLockSynchronizationFactory());
            _factories.Add(13, new ExampleAsyncAndAwaitFactory());
        }

    }
    public abstract class BaseActionFactory : IActionFactory
    {
        public abstract Action GetAction();

        public virtual void CancelTask()
        {

        }

        public event EventHandler<string> MessageGenerated;

        protected void RaiseMessageGenerated(string message)
        {
            MessageGenerated?.Invoke(this, message);
        }

        protected void PrintPluses(int n)
        {
            RaiseMessageGenerated("\nPrintPluses thread's ID: " + Thread.CurrentThread.ManagedThreadId + "\n");
            for (int i = 0; i < n; i++)
            {
                RaiseMessageGenerated("+");
            }
        }
        protected void PrintMinuses(int n)
        {
            RaiseMessageGenerated("\nPrintMinuses thread's ID: " + Thread.CurrentThread.ManagedThreadId + "\n");
            for (int i = 0; i < n; i++)
            {
                RaiseMessageGenerated("-");
            }
        }

        protected void PrintA(object obj)
        {
            RaiseMessageGenerated("A");
        }
        protected void Print(int result)
        {
            RaiseMessageGenerated("\nPrint thread's ID: " + Thread.CurrentThread.ManagedThreadId + "\n");
            RaiseMessageGenerated("\nStarting the Print method\n");
            Thread.Sleep(2000);
            RaiseMessageGenerated("The result is " + result + ".\nPrint method is done\n");
        }

        protected int CalculateLength(string input)
        {
            RaiseMessageGenerated("Starting the CalculateLength method. Thread ID:" + Thread.CurrentThread.ManagedThreadId + "\n");
            Thread.Sleep(2000);
            return input.Length;
        }
        protected float Divide(int? a, int? b)
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
    }
    public class PlainThreadsFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                RaiseMessageGenerated("Executing Plain Threads..." + "\n");
                RaiseMessageGenerated("Cores count: " + Environment.ProcessorCount + "\n");
                RaiseMessageGenerated("Main thread's ID: " + Thread.CurrentThread.ManagedThreadId + "\n");

                Stopwatch stopwatch = Stopwatch.StartNew();
                Thread thread1 = new Thread(() => PrintPluses(1000));
                Thread thread2 = new Thread(() => PrintMinuses(1000));

                thread1.Start();
                thread2.Start();
                stopwatch.Stop();
                RaiseMessageGenerated("Took: " + stopwatch.ElapsedMilliseconds + " ms to start 2 Threads threads\n");
            };
        }
    }

    // Concrete factory for tasks
    public class ThreadPoolFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                RaiseMessageGenerated("Executing ThreadpoolFactory Action..." + "\n");
                const int iterations = 1000;
                Stopwatch stopwatch = Stopwatch.StartNew();

                for (int i = 0; i < iterations; i++)
                {
                    ThreadPool.QueueUserWorkItem(PrintA);
                }
                stopwatch.Stop();
                RaiseMessageGenerated("Took: " + stopwatch.ElapsedMilliseconds + " ms to start 1000 Threadpool threads\n");
                // Your code for tasks
            };
        }
    }
    public class TasksFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                RaiseMessageGenerated("Executing PrintPlusses as Task" + "\n");
                Task task1 = Task.Run(() => PrintPluses(1000));
                RaiseMessageGenerated("Executing PrintMinusses as Task" + "\n");
                Task task2 = Task.Run(() => PrintMinuses(1000));
                RaiseMessageGenerated("You should see a mix of plusses and minusses" + "\n");
                // Your code for tasks
            };
        }
    }
    public class ExampleWaitAllFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                RaiseMessageGenerated("Starting Task 1\n");
                var task3 = Task.Run(() =>
                {
                    Thread.Sleep(1000);

                });
                RaiseMessageGenerated("Starting Task 2\n");
                var task4 = Task.Run(() =>
                {
                    Thread.Sleep(1000);
                });


                Task.WaitAll(task3, task4);
                RaiseMessageGenerated("Task.WaitAll blocked until both tasks were complete.");
            };
        }
    }
    public class ExampleContinuationsContinueWithFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                Task taskContinuation = Task.Run(() => CalculateLength("Hello there"))
                    .ContinueWith(taskWithResult => RaiseMessageGenerated("Length is " + taskWithResult.Result + "\n"))
                    .ContinueWith(completedTask =>
                    {
                        Thread.Sleep(500);
                        RaiseMessageGenerated("The second continuation.\n");
                    });
            };
        }
    }
    public class ExampleContinuationsContinueWhenAllFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                var tasks = new[]
                {
                    Task.Run(() => CalculateLength("hello there")),
                    Task.Run(() => CalculateLength("hi")),
                    Task.Run(() => CalculateLength("hola")),
                };

                var continuationTask = Task.Factory.ContinueWhenAll(
                    tasks,
                    completedTasks => RaiseMessageGenerated(
                        string.Join(", ", completedTasks.Select(task => task.Result))));
            };
        }
    }


    public class ExampleChildTasksFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                var parent = Task.Run(() =>
                {
                    RaiseMessageGenerated("Parent task executing.");

                    var child = Task.Run(() =>
                    {
                        RaiseMessageGenerated("Child task starting.");
                    });
                });
            };
        }
    }
    public class ExampleTaskFromResultFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                //Task<int> taskFromResult = Task.FromResult(10);
                RaiseMessageGenerated("Starting Task from main Thread ID:" + Thread.CurrentThread.ManagedThreadId + "\n");
                Task.Run(() => CalculateLength("Hello"))
                    .ContinueWith(completedTask => Print(completedTask.Result))
                    .ContinueWith(previousContinuation => RaiseMessageGenerated("The process is finished."));

                RaiseMessageGenerated("Back in Main Thread ID:" + Thread.CurrentThread.ManagedThreadId + "\n");

            };
        }
    }
    public class ExampleCancellingTasksFactory : BaseActionFactory
    {
        private CancellationTokenSource _cancellationTokenSource;
        public override Action GetAction()
        {
            return () =>
            {
                _cancellationTokenSource = new CancellationTokenSource();
                var task = Task.Run(
                    () => NeverendingMethod(_cancellationTokenSource),
                    _cancellationTokenSource.Token)
                    .ContinueWith(canceledTask =>
                        RaiseMessageGenerated($"\nTask with ID {canceledTask.Id} has been canceled."),
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
                RaiseMessageGenerated("Working...");
                Thread.Sleep(1500);
            }
        }
    }

    public class ExampleAggregateExceptionFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                var taskThatMayFault = Task.Run(() => Divide(2, 0))
                    .ContinueWith(
                    faultedTask =>
                    {
                        faultedTask.Exception.Handle(ex =>
                        {
                            RaiseMessageGenerated("Division task finished\n");
                            if (ex is ArgumentNullException)
                            {
                                RaiseMessageGenerated("Arguments can't be null.\n");
                                return true;
                            }
                            if (ex is DivideByZeroException)
                            {
                                RaiseMessageGenerated("Can't divide by zero.\n");
                                return true;
                            }
                            RaiseMessageGenerated("Unexpected exception type.\n");
                            return false;
                        });
                    },TaskContinuationOptions.OnlyOnFaulted);};
        }
    }

    public class ExampleMultipleContinuationsFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                var taskWithMultipleContinuations = new Task(() => Divide(10, 2));

                taskWithMultipleContinuations.ContinueWith(faultedTask =>
                    RaiseMessageGenerated("Success"),
                    TaskContinuationOptions.OnlyOnRanToCompletion);

                taskWithMultipleContinuations.ContinueWith(faultedTask =>
                    RaiseMessageGenerated("Exception thrown: " + faultedTask.Exception.Message),
                    TaskContinuationOptions.OnlyOnFaulted);

                taskWithMultipleContinuations.Start();
            };
        }
    }
 

    public class ExampleLockSynchronizationFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                RaiseMessageGenerated("Counter value is: 0 and should be 0 at completions");
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
                RaiseMessageGenerated("\nUsing Task.WaitAll which will block");
                Task.WaitAll(tasksAccessingTheSameResource.ToArray());
                RaiseMessageGenerated("\nDone. Counter value is: " + counter.Value);
            };
        }
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
    }
    public class ExampleAsyncAndAwaitFactory : BaseActionFactory
    {
        public override Action GetAction()
        {
            return () =>
            {
                RaiseMessageGenerated("\nStarting in caller Thread Id:" + Thread.CurrentThread.ManagedThreadId + "\n");
                var taskFromAsyncMethod = Process("Fun with async and await");
                RaiseMessageGenerated("\nBack in original caller.\n");

            };
        }
        private async Task<int> CalculateLengthAsync(string input)
        {
            RaiseMessageGenerated("Starting the CalculateLengthAsync method. Thread ID:" + Thread.CurrentThread.ManagedThreadId + "\n");
            await Task.Delay(4000);
            RaiseMessageGenerated("\nCalculateLengthAsync is done\n");
            return input.Length;
        }
        private async Task Process(string input)
        {
            try
            {
                RaiseMessageGenerated("\nProcess thread's ID: " + Thread.CurrentThread.ManagedThreadId + "\n");
                var length = await CalculateLengthAsync(input);
                //control will go back to caller until the Calculate
                //LengthAsync is completed
                RaiseMessageGenerated("\nBack in Process Method. About to call PrintAsync: " + Thread.CurrentThread.ManagedThreadId + "\n");
                //Once complete then PrintAsync will be invoked
                await PrintAsync(length);
                //control will again go back to caller until PrintAsync is done
                RaiseMessageGenerated("\nBack in Process Method.The process is finished.\n");
            }
            catch (NullReferenceException ex)
            {
                RaiseMessageGenerated("\nThe input can't be null.\n");
            }
        }
        private async Task PrintAsync(int result)
        {
            RaiseMessageGenerated("\nPrintAsync thread's ID: " + Thread.CurrentThread.ManagedThreadId + "\n");
            RaiseMessageGenerated("\nStarting the Print method\n");
            await Task.Delay(4000);
            RaiseMessageGenerated("The result is " + result + ".\nPrint method is done");
        }
    }
}
