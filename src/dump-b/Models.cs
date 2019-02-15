using System.Collections.Generic;

namespace dump_b
{
    public class Threads
    {
        public Dictionary<ParsedThread.ThreadType, ParsedThread[]> Items { get; set; }
    }

    public class ParsedThread
    {
        public int Id { get; set; }
        public ThreadType Type { get; set; }

        public enum ThreadType
        {
            ThreadPoolWorker,
            ThreadPoolIocp,
            Other
        }
    }

    public class Heap
    {

    }
}
