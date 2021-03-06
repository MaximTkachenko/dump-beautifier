﻿using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace DumpBeautifier.Extensions
{
    static class ClrThreadPoolExt
    {
        public static string CreateMarkup(this ClrThreadPool threadpool)
        {
            var html = new StringBuilder();

            html.Append($"<div>idle: {threadpool.IdleThreads}</div>");
            html.Append($"<div>running: {threadpool.RunningThreads}</div>");
            html.Append($"<div>total: {threadpool.TotalThreads}</div>");
            
            return html.ToString();
        }
    }
}
