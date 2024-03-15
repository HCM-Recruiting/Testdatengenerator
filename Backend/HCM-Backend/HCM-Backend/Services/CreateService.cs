using ApplicationLib;
using HCMBackend.MockData;
using Humanizer;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HCMBackend.Services
{
    public class CreateService
    {
        private AuthService _authService;
        private string dateFormat;
        Random random;
        private string[] firstNames;
        private string[] lastNames;
        private string[] addresses;

        public CreateService(AuthService authService)
        {
            _authService = authService;
            random = new Random();
            dateFormat = "yyyy-MM-dd";
            firstNames = File.ReadAllLines("MockData/firstName.csv").Skip(1).ToArray();
            lastNames = File.ReadAllLines("MockData/lastName.csv").Skip(1).ToArray();
            addresses = File.ReadAllLines("MockData/address.csv").Skip(1).ToArray();
        }



        public bool CreateApplicationsXML(Dictionary<string, Dictionary<string, int>> classPropertyDictionary, int amount)
        {
            List<Application> generatedApplicationList = new List<Application>();

            for (int i = 0; i < amount; i++)
            {
                Application generatedApplication = new();
                Applicant generatedApplicant = new();
                Address generatedAddress = new();
                if (classPropertyDictionary.ContainsKey("Application"))
                {
                    generatedApplication = GenerateApplication(classPropertyDictionary["Application"]);
                }
                if (classPropertyDictionary.ContainsKey("Applicant"))
                {
                    generatedApplicant = GenerateApplicant(classPropertyDictionary["Applicant"]);
                }
                if (classPropertyDictionary.ContainsKey("Address"))
                {
                    generatedAddress = GenerateAddress(classPropertyDictionary["Address"]);
                }
                foreach (var item in classPropertyDictionary)
                {
                    switch (item.Key)
                    {
                        case "SocioDemographicData":
                            GenerateSocioDemographicData(item.Value);
                            break;
                        case "Contact":
                            GenerateContact(item.Value);
                            break;
                    }
                }
                    generatedApplication.JobOffer = new()
                    {
                        Identifier = "TOP - 2023 - 000006",
                        Id = "8a0bc0a88adab669018adacb06730022"
                    };
                generatedApplicant.Address = generatedAddress;
                generatedApplication.Applicant = generatedApplicant;

                generatedApplicationList.Add(generatedApplication);
            }



            using (XmlWriter writer = XmlWriter.Create("generatedApplicationList.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("entities");
                foreach (var application in generatedApplicationList)
                {
                    application.Serialize(writer);
                }
                writer.WriteEndElement();
            }

            RemoveElementsWithDefaultValues("generatedApplicationList.xml");

            return _authService.PostApplicationXML("applicant.xml");
        }

        private void GenerateSocioDemographicData(Dictionary<string, int> value)
        {
            
        }
        #region GenerateAddressAndContact
        private Address GenerateAddress(Dictionary<string, int> addressProperties)
        {
            Address address = new Address();
            address.FirstName = firstNames[random.Next(firstNames.Length)];
            address.LastName = lastNames[random.Next(lastNames.Length)];
            foreach (var item in addressProperties)
            {
                if (ShouldCreate(item.Value))
                    switch (item.Key)
                    {
                        case "Title":
                            address.Title = xProData.title[random.Next(xProData.title.Length)];
                            break;
                        case "Street":
                            address.Street = addresses[random.Next(addresses.Length)].Split(',')[0];
                            break;
                        case "StreetNumber":
                            address.StreetNumber = addresses[random.Next(addresses.Length)].Split(',')[1];
                            break;
                        case "ZipCode":
                            address.ZipCode = addresses[random.Next(addresses.Length)].Split(',')[2];
                            break;
                        case "City":
                            address.City = addresses[random.Next(addresses.Length)].Split(',')[3];
                            break;
                        case "Region":
                            address.Region = xProData.countryShortcodes[random.Next(xProData.countryShortcodes.Length)];
                            break;
                        case "Country":
                            address.Country = xProData.countryShortcodes[random.Next(xProData.countryShortcodes.Length)];
                            break;
                    }
            }
            return address;
        }

        private Contact GenerateContact(Dictionary<string, int> addressProperties)
        {
            Contact contact = new Contact();
            return contact;
        }
        #endregion

        #region GenerateApplication
        public Application GenerateApplication(Dictionary<string, int> applicationProperties)
        {
            Application application = new Application();
            foreach (var item in applicationProperties)
            {
                if (ShouldCreate(item.Value))
                    switch (item.Key)
                    {
                        case "KeepOnFile":
                            application.KeepOnFile = true;
                            break;
                        case "RetentionDate":
                            application.RetentionDate = DateTime.Now.ToString(dateFormat);
                            break;
                        case "PrivacyTermsAccepted":
                            application.PrivacyTermsAccepted = true;
                            break;
                        case "PrivacyTermsAcceptedDate":
                            application.PrivacyTermsAcceptedDate = DateTime.Now.ToString(dateFormat);
                            break;
                        case "ForwardTermsAccepted":
                            application.ForwardTermsAccepted = true;
                            break;
                        case "ForwardTermsAcceptedDate":
                            application.ForwardTermsAcceptedDate = DateTime.Now.ToString(dateFormat);
                            break;
                        case "EarliestBeginDate":
                            application.EarliestBeginDate = DateTime.Now.ToString(dateFormat);
                            break;
                        case "SalaryWish":
                            application.SalaryWish = random.Next(2500, 5000);
                            break;
                        case "SalaryCurrency":
                            application.SalaryCurrency = "EUR";
                            break;
                        case "SalaryUnit":
                            application.SalaryUnit = xProData.salaryUnit[random.Next(1, xProData.salaryUnit.Length)];
                            break;
                        case "ActivityDimension": //not yet found out
                            break;
                        case "OfferKnownBy": // not yet found out
                            break;
                    }
            }
            return application;
        }
        #endregion

        #region GenerateApplicant
        private Applicant GenerateApplicant(Dictionary<string, int> applicantProperties)
        {
            Applicant applicant = new Applicant();
            foreach (var item in applicantProperties)
            {
                if (ShouldCreate(item.Value))
                    switch (item.Key)
                    {
                        case "Gender":
                            applicant.Gender = random.Next(1, 5).ToString();
                            break;
                        case "Birthday":
                            applicant.Birthday = GenerateRandomBirthday(18, 50);
                            break;
                        case "BirthLocation":
                            applicant.BirthLocation = "";
                            break;
                        case "Nationality":
                            applicant.Nationality = xProData.countryShortcodes[random.Next(xProData.countryShortcodes.Length)];
                            break;
                        case "InsuranceNumber":
                            applicant.InsuranceNumber = $"{xProData.countryShortcodes[random.Next(xProData.countryShortcodes.Length)]} {DateTime.Now.Year.ToString().Substring(2)} {random.Next(10000, 100000)} A";
                            break;
                    }
            }
            return applicant;
        }

        private string GenerateRandomBirthday(int minAge, int maxAge)
        {
            DateTime today = DateTime.Now;
            int maxYear = today.Year - minAge;
            int minYear = today.Year - maxAge;
            int randomYear = random.Next(minYear, maxYear);
            int randomMonth = random.Next(1, 13);
            int maxDay = DateTime.DaysInMonth(randomYear, randomMonth);
            int randomDay = random.Next(1, maxDay + 1);
            return new DateTime(randomYear, randomMonth, randomDay).ToString(dateFormat);
        }

        #endregion
        private bool ShouldCreate(int chance)
        {
            if (chance < 100)
            {
                int randomNumber = random.Next(1, chance);
                return randomNumber <= chance;
            }
            return true;
        }

        #region XMLEmptyValueRemover

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
        #endregion
    }
}
