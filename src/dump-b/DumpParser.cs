using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Runtime;

namespace dump_b
{
    class DumpParser :IDisposable
    {
        private readonly DataTarget _dump;
        private readonly ClrRuntime _runtime;

        public DumpParser(string dumpPath)
        {
            _dump = DataTarget.LoadCrashDump(dumpPath);
            ClrInfo runtimeInfo = _dump.ClrVersions[0];
            Console.WriteLine(runtimeInfo.LocalMatchingDac);
            _runtime = runtimeInfo.CreateRuntime();
        }

        public Threads GetThreads()
        {
            var threads = new List<ParsedThread>(_runtime.Threads.Count);
            foreach (var clrThread in _runtime.Threads)
            {
                var thread = new ParsedThread
                {
                    Id = clrThread.ManagedThreadId,
                    Type = clrThread.IsThreadpoolCompletionPort
                        ? ParsedThread.ThreadType.ThreadPoolIocp
                        : clrThread.IsThreadpoolWorker
                            ? ParsedThread.ThreadType.ThreadPoolWorker
                            : ParsedThread.ThreadType.Other
                };

                threads.Add(thread);
            }

            return new Threads
            {
                Items = threads.GroupBy(x => x.Type).ToDictionary(x => x.Key, x => x.ToArray())
            };
        }

        public Heap GetHeap()
        {
            return null;
        }

        public void Dispose()
        {
            _dump.Dispose();
        }
    }
}
