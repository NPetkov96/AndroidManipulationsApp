using HtmlAgilityPack;

namespace MedSestriManipulations.Services.Laboratory
{
    public class LabResultsParser
    {
        public (string FullName, string BirthDate)? ParsePatientInfo(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nameNode = doc.DocumentNode
                .SelectSingleNode("//span[@class='text-md']/strong");

            var birthNode = doc.DocumentNode
                .SelectSingleNode("//span[@class='text-lg' and contains(text(),'Роден на:')]");

            var name = nameNode?.InnerText?.Trim();
            var birthDate = birthNode?.InnerText?.Split(':').LastOrDefault()?.Trim();

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(birthDate))
            {
                return (name, birthDate);
            }

            return null;
        }

    }
}
