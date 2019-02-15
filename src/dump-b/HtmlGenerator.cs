using System;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace dump_b
{
    class HtmlGenerator : IDisposable
    {
        private readonly string _targetFilePath;
        private readonly HtmlDocument _doc;

        public HtmlGenerator(string dumpPath)
        {
            _targetFilePath = Path.Combine(Path.GetDirectoryName(dumpPath), $"{Path.GetFileNameWithoutExtension(dumpPath)}-beautified.html");
            if (File.Exists(_targetFilePath))
            {
                File.Delete(_targetFilePath);
            }
            _doc = new HtmlDocument();
            _doc.LoadHtml(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.html")));
        }

        public void RenderThreads(Threads threads)
        {
            var node = _doc.GetElementbyId("threads");
            var html = new StringBuilder();
            foreach (var item in threads.Items)
            {
                html.Append($"<div>{item.Key.ToString().ToUpper()}</div>");
                foreach (var thread in item.Value)
                {
                    html.Append($"<div>thred#{thread.Id}</div>");
                }
            }

            node.InnerHtml = html.ToString();
        }

        public void RenderHeap(Heap heap)
        {

        }

        public void Dispose()
        {
            using (var writer = new StreamWriter(_targetFilePath))
            {
                _doc.Save(writer);
            }
        }
    }
}
