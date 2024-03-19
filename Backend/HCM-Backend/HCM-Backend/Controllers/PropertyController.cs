using HCMBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HCMBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        ApplicationService _applicationService;
        CreateService _createService;
        public PropertyController(ApplicationService service, CreateService createService)
        {
            _applicationService = service;
            _createService = createService;
        }

        [HttpGet]
        public Dictionary<string, List<string>> GetClassDictionary()
        {
            return _applicationService.GetClassProperties();
        }

        [HttpPost]
        public bool CreateProperties(Dictionary<string, Dictionary<string,int>> classPropertyDictionary, int amount)
        {
            Console.WriteLine("Received dictionary: ");

            foreach (var entry in classPropertyDictionary)
            {
                Console.WriteLine($"Class: {entry.Key}");


                    foreach (var property in entry.Value)
                    {
                        Console.WriteLine($"  Property: {property.Key}, Value: {property.Value}");
                    }
                
            }
            return _createService.CreateApplicationsXML(classPropertyDictionary, amount);
        }
    }
}
