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
                using (WordprocessingDocument package = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                {
                    CreateParts(package);
                }
            }
        }

        private static void CreateParts(WordprocessingDocument document)
        {
            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

            MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
            GenerateMainDocumentPart1Content(mainDocumentPart1);

            WebSettingsPart webSettingsPart1 = mainDocumentPart1.AddNewPart<WebSettingsPart>("rId3");
            GenerateWebSettingsPart1Content(webSettingsPart1);

            DocumentSettingsPart documentSettingsPart1 = mainDocumentPart1.AddNewPart<DocumentSettingsPart>("rId2");
            GenerateDocumentSettingsPart1Content(documentSettingsPart1);

            StyleDefinitionsPart styleDefinitionsPart1 = mainDocumentPart1.AddNewPart<StyleDefinitionsPart>("rId1");
            GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);

            ThemePart themePart1 = mainDocumentPart1.AddNewPart<ThemePart>("rId5");
            GenerateThemePart1Content(themePart1);

            FontTablePart fontTablePart1 = mainDocumentPart1.AddNewPart<FontTablePart>("rId4");
            GenerateFontTablePart1Content(fontTablePart1);

            SetPackageProperties(document);
        }

        private static void SetPackageProperties(WordprocessingDocument document)
        {
            throw new NotImplementedException();
        }

        private static void GenerateFontTablePart1Content(FontTablePart fontTablePart1)
        {
            throw new NotImplementedException();
        }

        private static void GenerateThemePart1Content(ThemePart themePart1)
        {
            throw new NotImplementedException();
        }

        private static void GenerateStyleDefinitionsPart1Content(StyleDefinitionsPart styleDefinitionsPart1)
        {
            throw new NotImplementedException();
        }

        private static void GenerateDocumentSettingsPart1Content(DocumentSettingsPart documentSettingsPart1)
        {
            throw new NotImplementedException();
        }

        private static void GenerateWebSettingsPart1Content(WebSettingsPart webSettingsPart1)
        {
            throw new NotImplementedException();
        }

        private static void GenerateMainDocumentPart1Content(MainDocumentPart mainDocumentPart1)
        {
            throw new NotImplementedException();
        }

        private static void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            //throw new NotImplementedException();
        }
    }
}
