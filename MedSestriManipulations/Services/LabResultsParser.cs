using HtmlAgilityPack;

namespace MedSestriManipulations.Services
{
    public class LabResultsParser
    {
        public (string FullName, string BirthDate)? ParsePatientInfo(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // Търсим <span class='text-md'><strong>ИВАНКА МЛАДЕНОВА ЯНАЧКОВА</strong></span>
            var nameNode = doc.DocumentNode
                .SelectSingleNode("//span[@class='text-md']/strong");

            // Търсим следващия <span class='text-lg'> Роден на: 06.11.1931</span>
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
