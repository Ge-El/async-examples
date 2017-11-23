using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace async_examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var GetHtmlTask = GetHtml();
            Console.WriteLine(GetHtmlTask);

            var GetHtmlAsyncTask = GetHtmlAsync();
            Console.WriteLine(await GetHtmlAsyncTask);

            var calculateResultAsyncTask = CalculateResultAsync();
            Console.WriteLine(await calculateResultAsyncTask);

            var GetNameAsyncTask = GetNameAsync();
            Console.WriteLine(await GetNameAsyncTask);




            await Task.WhenAll(GetHtmlAsyncTask, calculateResultAsyncTask);
            var GetHtmlAsyncResult = await GetNameAsyncTask;
            var calculateResultAsyncResult = await calculateResultAsyncTask;

        }

        /// <summary>
        /// Calls an async method returnes an active task, likley yet to complete
        /// </summary>
        /// <returns>
        /// A task of type string
        /// </returns>
        static Task<string> GetHtml()
        {
            // Execution is synchronous here
            var client = new HttpClient();

            return client.GetStringAsync("http://www.dotnetfoundation.org");
        }


        /// <summary>
        /// Calls an async method returnes an completet task
        /// </summary>
        /// <remarks>
        /// The use of await keyword causes the method to return a newly created task object. 
        /// The members of Task<T> enables the caller to monitor the progress of the task.
        /// No thread waits for the request
        /// </remarks>
        /// A freshly created task object
        /// <returns>
        /// </returns>
        static async Task<string> GetHtmlAsync()
        {
            // Execution is synchronous here
            var client = new HttpClient();

            return await client.GetStringAsync("http://www.dotnetfoundation.org");
        }

        /// <summary>
        /// Calculates alot of hard shit.
        /// </summary>
        /// <returns>
        ///    A task of type string
        /// </returns>
        static async Task<int> CalculateResultAsync()
        {
            // This queues up the work on the threadpool.
            var expensiveResultTask = Task.Run(async () =>
            {
                await Task.Delay(10000);

                return 69;
            });


            Console.WriteLine("Doing shit while waiting for task");
            // Note that at this point, you can do some other work concurrently,
            // as CalculateResult() is still executing!

            // Execution of CalculateResult is yielded here!
            var result = await expensiveResultTask;

            return result;
        }

        static async ValueTask<string> GetNameAsync()
        {
            await Task.Delay(3000);

            return "George";
        }


    }


}
