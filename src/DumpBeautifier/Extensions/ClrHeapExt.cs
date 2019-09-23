using System.Linq;
using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace DumpBeautifier.Extensions
{
    static class ClrHeapExt
    {
        public static string CreateMarkup(this ClrHeap heap)
        {
            var html = new StringBuilder();
            html.Append("<table>");
            html.Append($"<tr><th>type</th><th>count</th><th>size</th></tr>");

            var objStats = heap.EnumerateObjects().GroupBy(x => x.Type)
                .Select(x => (Type: x.Key.Name, Count: x.Count(), Size: x.Sum(o => (long)o.Size)))
                .OrderByDescending(x => x.Size);
            foreach (var stat in objStats)
            {
                html.Append($"<tr><td>{stat.Type}</td><td>{stat.Count}</td><td>{stat.Size}</td></tr>");
            }

            html.Append("</table>");
            return html.ToString();
        }
    }
}
