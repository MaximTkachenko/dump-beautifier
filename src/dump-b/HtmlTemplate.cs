using System;
using System.IO;
using HtmlAgilityPack;

namespace dump_b
{
    class HtmlTemplate : IDisposable
    {
        private readonly HtmlDocument _doc;

        public HtmlTemplate(string dumpPath)
        {
            OutputFilePath = Path.Combine(Path.GetDirectoryName(dumpPath), $"{Path.GetFileNameWithoutExtension(dumpPath)}-beautified.html");
            if (File.Exists(OutputFilePath))
            {
                File.Delete(OutputFilePath);
            }
            _doc = new HtmlDocument();
            _doc.LoadHtml(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.html")));
        }

        public string OutputFilePath { get; }

        public void RenderRuntime(string runtime) => Render("runtime", runtime);

        public void RenderThreads(string threads) => Render("threads-details", threads);

        public void RenderThreadpool(string threadpool) => Render("threadpool", threadpool);

        public void RenderHeap(string heap) => Render("heap", heap);

        private void Render(string id, string html) => _doc.GetElementbyId(id).InnerHtml = html;

        public void Dispose()
        {
            using (var writer = new StreamWriter(OutputFilePath))
            {
                _doc.Save(writer);
            }
        }
    }
}
