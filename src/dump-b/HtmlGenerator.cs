using System;
using System.IO;
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

        public void RenderThreads(ParsedThread[] threads)
        {

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
