using System;
using System.IO;
using dump_b.Extensions;
using Microsoft.Diagnostics.Runtime;

namespace dump_b
{
    //https://blog.maartenballiauw.be/post/2017/01/03/exploring-.net-managed-heap-with-clrmd.html
    // https://github.com/Microsoft/clrmd/blob/master/Documentation/GettingStarted.md
    // https://labs.criteo.com/2017/02/going-beyond-sos-clrmd-part-1/
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            args = new[] { @"C:\code\dumps\dotnet.dmp" };
            if (args == null || args.Length == 0)
            {
                WriteError("provide path to dump file");
                return;
            }

            var path = args[0];
            if (!File.Exists(path))
            {
                WriteError("file not found");
                return;
            }

            Console.ResetColor();

            DataTarget dump = DataTarget.LoadCrashDump(path);
            var html = new HtmlTemplate(path);
            try
            {
                ClrInfo clrInfo = dump.ClrVersions[0];
                ClrRuntime runtime = clrInfo.CreateRuntime();

                html.RenderClrInfo(clrInfo.CreateMarkup());
                html.RenderThreads(runtime.Threads.CreateMarkup());
                html.RenderThreadpool(runtime.ThreadPool.CreateMarkup());
                html.RenderHeap(runtime.Heap.CreateMarkup());

                Console.ForegroundColor = ConsoleColor.Green;
                WriteOk(html.OutputFilePath);
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
            finally
            {
                dump.Dispose();
                html.Dispose();
            }
        }

        static void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void WriteOk(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void Write(string text)
        {
            Console.ResetColor();
            Console.WriteLine(text);
        }
    }
}
