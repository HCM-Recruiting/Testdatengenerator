﻿using ApplicationLib;
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
        private string[] contacts;
        private string _fileName;

        public IConfiguration configuration { get; set; }

        public CreateService(AuthService authService, IConfiguration iConfig)
        {
            _authService = authService;
            random = new Random();
            dateFormat = "yyyy-MM-dd";
            firstNames = File.ReadAllLines("MockData/firstName.csv").Skip(1).ToArray();
            lastNames = File.ReadAllLines("MockData/lastName.csv").Skip(1).ToArray();
            addresses = File.ReadAllLines("MockData/address.csv").Skip(1).ToArray();
            contacts = File.ReadAllLines("MockData/contacts.csv").Skip(1).ToArray();
            configuration = iConfig;
            _fileName = configuration.GetValue<string>("MyAuthData:FileName");
        }



        public bool CreateApplicationsXML(Dictionary<string, Dictionary<string, int>> classPropertyDictionary, int amount)
        {
            List<Application> generatedApplicationList = new List<Application>();
            List<JobOffer> jobOffers = _authService.GetAvailableJobOffers();
            int jobOfferIndex = 0;

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
                            generatedApplicant.SocioDemographicData = GenerateSocioDemographicData(item.Value);
                            break;
                        case "Contact":
                            generatedAddress.Contact = GenerateContact(item.Value);
                            break;
                    }
                }
                generatedApplication.JobOffer = jobOffers[jobOfferIndex];
                jobOfferIndex = (jobOfferIndex + 1) % jobOffers.Count;
                generatedApplicant.Address = generatedAddress;
                generatedApplication.Applicant = generatedApplicant;

                generatedApplicationList.Add(generatedApplication);
            }



            using (XmlWriter writer = XmlWriter.Create(_fileName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("entities");
                foreach (var application in generatedApplicationList)
                {
                    application.Serialize(writer);
                }
                writer.WriteEndElement();
            }

            RemoveElementsWithDefaultValues(_fileName);

            return _authService.PostApplicationXML();
        }

        private SocioDemographicData GenerateSocioDemographicData(Dictionary<string, int> socioProperties)
        {
            SocioDemographicData socioDemographicData = new();

            foreach (var item in socioProperties)
            {
                if (ShouldCreate(item.Value))
                    switch (item.Key)
                    {
                        case "birthName":
                            socioDemographicData.BirthName = lastNames[random.Next(lastNames.Length)];
                            break;
                        case "familyStatus":
                            socioDemographicData.FamilyStatus = random.Next(1,11).ToString();
                            break;
                        case "religion":
                            socioDemographicData.Religion = "RK";
                            break;
                        case "militaryService":
                            socioDemographicData.MilitaryService = random.Next(1, 5).ToString();
                            break;
                        case "militaryFrom":
                            socioDemographicData.MilitaryFrom = GenerateRandomDate(5);
                            break;
                        case "militaryTo":
                            socioDemographicData.MilitaryFrom = GenerateRandomDate(2);
                            break;
                        case "numberOfChildren":
                            socioDemographicData.NumberOfChildren = random.Next(30);
                            break;
                        case "hobbies":
                            socioDemographicData.Hobbies = "Gaming";
                            break;
                        case "comments":
                            socioDemographicData.Comments = "Hallo";
                            break;
                    }
            }
            return socioDemographicData;
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

        private Contact GenerateContact(Dictionary<string, int> contactProperties)
        {
            Contact contact = new Contact();
            foreach (var item in contactProperties)
            {
                if (ShouldCreate(item.Value))
                    switch (item.Key)
                    {
                        case "telNumber":
                            contact.TelNumber = contacts[random.Next(contacts.Length)].Split(',')[0]; ;
                            break;
                        case "mobileNumber":
                            contact.TelNumber = contacts[random.Next(contacts.Length)].Split(',')[0]; ;
                            break;
                        case "email":
                            contact.TelNumber = contacts[random.Next(contacts.Length)].Split(',')[0]; ;
                            break;
                        case "website": //There are no values associated with website on the database
                            
                            break;
                    }
            }
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
                        case "ActivityDimension":
                            break;
                        case "OfferKnownBy":
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
                            applicant.BirthLocation = xProData.countryShortcodes[random.Next(xProData.countryShortcodes.Length)];
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
        private string GenerateRandomDate(int yearsBeforeNow)
        {
            DateTime today = DateTime.Now;
            int maxYear = today.Year - 1;
            int minYear = today.Year - yearsBeforeNow;
            int randomYear = random.Next(minYear, maxYear);
            int randomMonth = random.Next(1, 13);
            int maxDay = DateTime.DaysInMonth(randomYear, randomMonth);
            int randomDay = random.Next(1, maxDay + 1);
            return new DateTime(randomYear, randomMonth, randomDay).ToString(dateFormat);
        }

        private bool ShouldCreate(int chance)
        {
            if (chance < 100)
            {
                int randomNumber = random.Next(1, 100);
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
