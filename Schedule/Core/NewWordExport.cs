using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Core
{
    public static class NewWordExport
    {
        internal static void AltExportSchedulePage(
            Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> schedule,
            string facultyName,
            string filename,
            string dow,
            ScheduleRepository _repo,
            bool save,
            bool quit,
            bool print)
        {
            using (WordprocessingDocument wordDocument =
        WordprocessingDocument.Create(filename, WordprocessingDocumentType.Document))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text("Create text in body - CreateWordprocessingDocument"));
            }
        }
    }
}
