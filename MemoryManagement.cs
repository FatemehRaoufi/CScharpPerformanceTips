using System.Diagnostics;

namespace CScharpPerformanceTips
{/// <summary>
/// Premature optimizations can be counterproductive, making your C# code harder to read, maintain, 
/// and extend. It’s essential to first focus on writing clean, 
/// efficient code and only optimize when necessary after thoroughly profiling your application.
/// </summary>
    internal class MemoryManagement
    {
        public MemoryManagement()
        {

        }
        //---------------
        #region
        /// <summary>
        /// 1.Using the IDisposable interface
        /// In the bad way above, the ResourceHolder class doesn’t implement the IDisposable interface, 
        /// which means the unmanaged resources might not be released, causing potential memory leaks.
        /// By implementing the IDisposable interface, 
        /// you ensure that unmanaged resources will be released when no longer needed, 
        /// preventing memory leaks and reducing pressure on the garbage collector. 
        /// </summary>

        //Bad Way
        /*
        public class ResourceHolder
        {
            private Stream _stream;

            public ResourceHolder(string filePath)
            {
                _stream = File.OpenRead(filePath);
            }

            // Missing: IDisposable implementation
        }
        */
        public class ResourceHolder : IDisposable
        {
            private Stream _stream;

            public ResourceHolder(string filePath)
            {
                _stream = File.OpenRead(filePath);
            }

            public void Dispose()
            {
                _stream?.Dispose(); // Properly disposing the unmanaged resource.
            }
        }
        #endregion
        //-------------------------------
        /// <summary>
        /// 2.Avoid premature optimizations
        /// </summary>
        // Bad Way
        private void ProcessData()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // [Complex processing logic with unnecessary micro-optimizations]

            stopwatch.Stop();
            Console.WriteLine($"Processing time: {stopwatch.ElapsedMilliseconds} ms");
        }
        //-------------------------
    }
}
