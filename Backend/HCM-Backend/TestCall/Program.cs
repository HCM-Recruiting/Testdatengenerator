using ApplicationLib;
using HCMBackend.Services;
using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        var authService = new AuthService();
        authService.PostApplicationXML();
        //var applicationService = new ApplicationService();

        //var dic = applicationService.GetClassProperties();
        //foreach ( var item in dic )
        //{
        //    Console.WriteLine("Class: "+item.Key);
        //    foreach ( var prop in item.Value )
        //    {
        //        Console.WriteLine(prop);
        //    }
        //}
        // Create a mock application instance
        //var mockApplication = new Root
        //{
        //    Entities = new List<Application>
        //    {
        //        new Application
        //        {
        //            JobOffer = new JobOffer
        //            {
        //                Identifier = "MockIdentifier",
        //                Id = "MockId"
        //            },
        //            IncomingDate = DateTime.Now,
        //            KeepOnFile = true,
        //            RetentionDate = DateTime.Now.AddDays(30),
        //            PrivacyTermsAccepted = true,
        //            PrivacyTermsAcceptedDate = DateTime.Now,
        //            ForwardTermsAccepted = true,
        //            ForwardTermsAcceptedDate = DateTime.Now,
        //            EarliestBeginDate = DateTime.Now.AddMonths(1),
        //            SalaryWish = 50000,
        //            SalaryCurrency = "EUR",
        //            SalaryUnit = "Yearly",
        //            ActivityDimension = "Full-time",
        //            OfferKnownBy = "Referral",
        //            Applicant = new Applicant
        //            {
        //                Gender = "Male",
        //                Birthday = new DateTime(1990, 5, 15),
        //                BirthLocation = "City, Country",
        //                Nationality = "Austrian",
        //                InsuranceNumber = "123456789",
        //                Address = new Address
        //                {
        //                    Title = "Mr",
        //                    FirstName = "John",
        //                    LastName = "Doe",
        //                    Street = "Main Street",
        //                    StreetNumber = "123",
        //                    ZipCode = "12345",
        //                    City = "City",
        //                    Region = "Region",
        //                    Country = "Austria",
        //                    Contact = new Contact
        //                    {
        //                        TelNumber = "123456789",
        //                        MobileNumber = "987654321",
        //                        Email = "john.doe@example.com",
        //                        Website = "www.example.com"
        //                    }
        //                },
        //                SocioDemographicData = new SocioDemographicData
        //                {
        //                    BirthName = "Doe",
        //                    FamilyStatus = "Married",
        //                    FamilyStatusDescription = "Married with two children",
        //                    MilitaryService = "Completed",
        //                    MilitaryFrom = new DateTime(2010, 1, 1),
        //                    MilitaryTo = new DateTime(2012, 12, 31),
        //                    Religion = "Christian",
        //                    ReligionDescription = "Catholic",
        //                    WeddingDay = new DateTime(2015, 8, 20),
        //                    NameChanges = new List<NameChange>
        //                    {
        //                        new NameChange
        //                        {
        //                            Name = "PreviousName",
        //                            BeginDate = new DateTime(2000, 1, 1),
        //                            EndDate = new DateTime(2010, 1, 1),
        //                            Reason = "Changed name after marriage"
        //                        }
        //                    },
        //                    NumberOfChildren = 2,
        //                    Hobbies = "Reading, Traveling",
        //                    Comments = "No specific comments"
        //                },
        //                Photo = new Photo
        //                {
        //                    UseAsEmployeePhoto = true,
        //                    AssignmentType = "Personal",
        //                    Category = "Portrait",
        //                    SubCategory = "Official",
        //                    Name = "JohnDoePhoto",
        //                    Title = "Employee Photo",
        //                    Size = 1024,
        //                    Mimetype = "image/jpeg",
        //                    Description = "Employee photo",
        //                    DocumentBytes = "Base64EncodedImageString"
        //                }
        //            },
        //            SpecificData = new SpecificData
        //            {
        //                RegionalDomain = "IT",
        //                OwnCar = true,
        //                BusinessTrip = true,
        //                ChangeHome = false,
        //                DisabilityRightsApply = false,
        //                ApplicationToReductionInEarningCapacity = true,
        //                SevereDisability = "Not Applicable",
        //                BusinessTripPercent = 30,
        //                ReductionInEarningCapacity = 10,
        //                WorkPermitStatus = "Valid",
        //                WorkPermitType = "Type A",
        //                WorkPermitValidTo = DateTime.Now.AddYears(1),
        //                WorkPermitIdCardNr = "W123456789",
        //                ApplicationDateToReductionInEarningCapacity = DateTime.Now.AddDays(15),
        //                AdditionalFunction = "Team Lead",
        //                AdditionalInformation = "Additional information about the applicant",
        //                Expectation = "Challenging work environment",
        //                Motivation = "Motivated to contribute to the team",
        //                RegionalPrefs = "Prefer working in a collaborative team"
        //            },
        //            Documents = new List<Document>
        //            {
        //                new Document
        //                {
        //                    UseAsEmployeePhoto = false,
        //                    AssignmentType = "Personal",
        //                    Category = "Resume",
        //                    SubCategory = "Professional",
        //                    Name = "ResumeDocument",
        //                    Title = "Resume",
        //                    Size = 2048,
        //                    Mimetype = "application/pdf",
        //                    Description = "Professional resume",
        //                    DocumentBytes = "Base64EncodedPDFString"
        //                }
        //            }
        //        }
        //    }
        //};
        //authService.AddApplications(mockApplication);
    }
}
