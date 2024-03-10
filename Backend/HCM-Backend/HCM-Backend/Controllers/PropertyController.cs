using HCMBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HCMBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        ApplicationService _service;
        public PropertyController(ApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public Dictionary<string, List<string>> GetClassDictionary()
        {
            return _service.GetClassProperties();
        }

        [HttpPost]
        public bool CreateProperties(Dictionary<string, List<Dictionary<string,int>>> classPropertyDictionary)
        {
            Console.WriteLine("Received dictionary: ");

            foreach (var entry in classPropertyDictionary)
            {
                Console.WriteLine($"Class: {entry.Key}");

                foreach (var propertyList in entry.Value)
                {
                    foreach (var property in propertyList)
                    {
                        Console.WriteLine($"  Property: {property.Key}, Value: {property.Value}");
                    }
                }
            }

            return true;
        }
    }
}
