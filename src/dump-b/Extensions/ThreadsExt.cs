using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace dump_b.Extensions
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

                html.Append($"<div>thread#{thread.ManagedThreadId} {isGc} {isBackground} {isFinalizer} {isThreadpoolWorker} {isThreadpoolIocp}</div>");


                html.Append("</li>");
            }

            return html.ToString();
        }
    }
}
