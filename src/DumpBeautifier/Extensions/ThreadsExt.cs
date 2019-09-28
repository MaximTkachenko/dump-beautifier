using System.Collections.Generic;
using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace DumpBeautifier.Extensions
{
    static class ThreadsExt
    {
        public static string CreateMarkup(this IList<ClrThread> threads)
        {
            var html = new StringBuilder();

            foreach (var thread in threads)
            {
                html.Append("<li>");

                var isGc = thread.IsGC ? "GC" : string.Empty;
                var isBackground = thread.IsBackground ? "background" : string.Empty;
                var isFinalizer = thread.IsFinalizer ? "finalizer" : string.Empty;
                var isThreadpoolIocp = thread.IsThreadpoolCompletionPort ? "threadpool iocp" : string.Empty;
                var isThreadpoolWorker = thread.IsThreadpoolWorker ? "threadpool worker" : string.Empty;
                var isAlive = thread.IsAlive ? "alive" : string.Empty;
                var isTimer = thread.IsThreadpoolTimer ? "timer" : string.Empty;
                var isUnstarted = thread.IsUnstarted ? "unstarted" : string.Empty;

                html.Append($"<div>thread#{thread.ManagedThreadId} [{isGc} {isBackground} {isFinalizer} {isThreadpoolWorker} {isThreadpoolIocp} {isAlive} {isTimer} {isUnstarted}]</div>");
                foreach (var frame in thread.EnumerateStackTrace())
                {
                    html.Append($"<div>* {frame.DisplayString}</div>");
                }

                html.Append("</li>");
            }

            return html.ToString();
        }
    }
}
