using ApplicationLib;
using System.Reflection;

namespace HCMBackend.Services
{
    public class ApplicationService
    {
        public ApplicationService()
        {
        }

        public Dictionary<string, List<string>> GetClassProperties()
        {
            Dictionary<string, List<string>> classPropertyDictionary = new Dictionary<string, List<string>>();

            classPropertyDictionary[typeof(Address).Name] = GetProperties(typeof(Address));
            classPropertyDictionary[typeof(Applicant).Name] = GetProperties(typeof(Applicant));
            classPropertyDictionary[typeof(Application).Name] = GetProperties(typeof(Application));
            classPropertyDictionary[typeof(Contact).Name] = GetProperties(typeof(Contact));
            classPropertyDictionary[typeof(SocioDemographicData).Name] = GetProperties(typeof(SocioDemographicData));
            classPropertyDictionary[typeof(SpecificData).Name] = GetProperties(typeof(SpecificData));

            return classPropertyDictionary;
        }

        private List<string> GetProperties(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            List<string> propertyNames = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                if (!property.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), true).Any()
                    && (property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(int)))
                {
                    propertyNames.Add(property.Name);
                }
            }

            return propertyNames;
        }
    }
}
