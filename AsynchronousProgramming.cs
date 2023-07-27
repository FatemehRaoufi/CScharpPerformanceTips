namespace CScharpPerformanceTips
{
    internal class AsynchronousProgramming
    {
        public AsynchronousProgramming() { }

        //----------------------------
        /// <summary>
        /// Limit the number of concurrent operations:
        ///Managing concurrency is crucial for C# performance optimization. 
        /// By limiting the number of concurrent operations in your application, 
        ///you help to reduce the system’s overall load.

        /// </summary>
     
        //Bad Way:
        //In the bad way, tasks are spawned concurrently for each item without a proper limit, 
        //potentially causing significant strain on the system.
        public async Task ProcessManyItems(List<string> items)
        {
            //var tasks = items.Select(async item => await ProcessItem(items));
            //await Task.WhenAll(tasks);
        }

        //Good Way:
        //use a SemaphoreSlim to control the number of concurrent operations.
        //This is a great example of how to improve application performance in C# without sacrificing readability or maintainability.
        public async Task ProcessManyItems(List<string> items, int maxConcurrency = 10)
        {
            using (var semaphore = new SemaphoreSlim(maxConcurrency))
            {
                var tasks = items.Select(async item =>
                {
                    await semaphore.WaitAsync(); // Limit concurrency by waiting for the semaphore.
                    try
                    {
                        //await ProcessItem(item);
                    }
                    finally
                    {
                        semaphore.Release(); // Release the semaphore to allow other operations.
                    }
                });

                await Task.WhenAll(tasks);
            }
        }
        //----------------------------------
    }
}
