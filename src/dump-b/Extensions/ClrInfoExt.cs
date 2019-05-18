using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace dump_b.Extensions
{
    static class ClrInfoExt
    {
        public static string CreateMarkup(this ClrInfo clrInfo)
        {
            var html = new StringBuilder();

            var dacInfo = clrInfo.DacInfo;

            html.Append($"<div>version: {clrInfo.Version}</div>");
            html.Append($"<div>dac file: {dacInfo.FileName}</div>");

            return html.ToString();
        }
    }
}
