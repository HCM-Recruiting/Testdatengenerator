using ApplicationLib;
using System.Xml;
using System.Xml.Linq;

namespace HCMBackend.Services
{
    public class CreateService
    {
        private AuthService _authService;

        public CreateService(AuthService authService)
        {
            _authService = authService;
        }

        public bool CreateApplicationsXML(Dictionary<string, List<Dictionary<string, int>>> classPropertyDictionary, int amount)
        {


            List<Application> generatedApplicationList = new List<Application>();

            for (int i = 0; i < amount; i++)
            {
                Application generatedApplication = new();

                generatedApplicationList.Add(generatedApplication);
            }



            using (XmlWriter writer = XmlWriter.Create("generatedApplicationList.xml"))
            {
                writer.WriteStartElement("entities");
                foreach (var application in generatedApplicationList)
                {
                    application.Serialize(writer);
                }
                writer.WriteEndElement();
            }


            RemoveElementsWithDefaultValues("generatedApplicationList.xml");

            return _authService.PostApplicationXML("generatedApplicationList.xml");
        }

        static void RemoveElementsWithDefaultValues(string fileName)
        {
            XDocument xmlDoc = XDocument.Load(fileName);
            RemoveEmptyElements(xmlDoc.Root);
            xmlDoc.Save(fileName);
        }
        private static void RemoveEmptyElements(XElement element)
        {
            foreach (var child in element.Elements().ToList())
            {
                if (child.HasElements)
                {
                    RemoveEmptyElements(child);
                }
                else if (child.IsEmpty || (child.Value == "0" && child.Name.LocalName != "gender") || child.Value.ToLower() == "false")
                {
                    child.Remove();
                }
            }
        }
    }
}
