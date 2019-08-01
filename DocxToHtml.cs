using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.IO;
using System.Text;
using System.Xml.Linq;
using OpenXmlPowerTools;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class DocxToHtml {
    static void Main(string[] args) {
    }

    public static string Convert(string inputPath) {
        var fi = new FileInfo(inputPath);
        byte[] byteArray = File.ReadAllBytes(fi.FullName);

        using (MemoryStream memoryStream = new MemoryStream()) {
            memoryStream.Write(byteArray, 0, byteArray.Length);

            using (WordprocessingDocument wDoc = WordprocessingDocument.Open(memoryStream, true)) {
                HtmlConverterSettings settings = new HtmlConverterSettings() {
                    FabricateCssClasses = true,
                    RestrictToSupportedLanguages = false,
                    RestrictToSupportedNumberingFormats = false
                };

                XElement html = HtmlConverter.ConvertToHtml(wDoc, settings);
                return html.ToString(SaveOptions.DisableFormatting);
            }
        }
    }
}
