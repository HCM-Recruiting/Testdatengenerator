using ApplicationLib;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HCMBackend.Services
{
    public class AuthService
    {
        public IConfiguration _configuration { get; set; }
        private string _clientID = "";
        private string _apiSharedKey = "";
        private string _baseRequestUrl = "";
        private HttpClient client;

        public AuthService(IConfiguration iConfig)
        {
            _configuration = iConfig;
            client = new HttpClient();
            _clientID = _configuration.GetValue<string>("MyAuthData:ClientId");
            _apiSharedKey = _configuration.GetValue<string>("MyAuthData:ApiSharedKey");
            _baseRequestUrl = _configuration.GetValue<string>("MyAuthData:BaseRequestURL");
        }

        #region CreateHash

        private void ShowHashResult(string propName, object propValue, string hashValue)
        {
            Console.WriteLine($"HashResult after {propName}: {propValue} = {hashValue}");
        }

        private string CreateSignature(string requestURL, string requestMethod, string content, string nonce, string timestamp)
        {
            string algorithm = "HmacSHA1";

            string signature = CreateHash(_clientID, _apiSharedKey, algorithm);
            ShowHashResult(nameof(_clientID), _clientID, signature);
            signature = CreateHash(requestURL, signature, algorithm);
            ShowHashResult(nameof(requestURL), requestURL, signature);
            signature = CreateHash(requestMethod, signature, algorithm);
            ShowHashResult(nameof(requestMethod), requestMethod, signature);
            signature = CreateHash(content, signature, algorithm);
            ShowHashResult(nameof(content), content, signature);
            signature = CreateHash(nonce, signature, algorithm);
            ShowHashResult(nameof(nonce), nonce, signature);
            signature = CreateHash(timestamp, signature, algorithm);
            ShowHashResult(nameof(timestamp), timestamp, signature);


            Console.WriteLine($"hmac {_clientID}:{algorithm}:{timestamp}:{nonce}:{signature}");
            return "hmac " + _clientID + ":" + algorithm + ":" + timestamp + ":" + nonce + ":" + signature;
        }

        static string CreateHash(string message, string key, string algorithm)
        {
            string hashResult = key;

            if (!string.IsNullOrWhiteSpace(message))
            {
                hashResult = GenerateHash(message, hashResult, algorithm);
            }

            return hashResult;
        }

        static string GenerateHash(string message, string key, string algorithm)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmac = new HMACSHA1(keyBytes))
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = hmac.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        #endregion


        public bool PostApplicationXML(string fileName)
        {

            string xmlContent = File.ReadAllText("applicant.xml");
            string content = xmlContent.ToString();
            string nonce = "18C31CEBF5CB69D2BFD920E792FEF7FF";
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            string authorizationHeader = CreateSignature(_baseRequestUrl, "POST", content, nonce, timestamp);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            client.DefaultRequestHeaders.Add("Accept", "application/xml;charset=UTF-8");

            HttpResponseMessage response = client.PostAsync(_baseRequestUrl, new StringContent(xmlContent, Encoding.UTF8, "application/xml")).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
                return true;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
        }

        public void PostApplicationsJson()
        {
            string baseRequestURL = "https://intplay.test.infoniqa.io/ei/services/restProxy/standard/application";
            string requestURL = $"{baseRequestURL}";


            Application testApplication = new Application()
            {
                JobOffer = new JobOffer
                {
                    Identifier = "TOP-2023-000006",
                    Id = "8a0bc0a88adab669018adacb06730022"
                },
                IncomingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Applicant = new Applicant
                {
                    Gender = "4",
                    Address = new Address
                    {
                        FirstName = "Johan",
                        LastName = "Liebert",
                    }
                }
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };


            string jsonContent = JsonConvert.SerializeObject(testApplication, jsonSerializerSettings);
            string nonce = "18C31CEBF5CB69D2BFD920E792FEF7FF";
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            string authorizationHeader = CreateSignature(baseRequestURL, "POST", jsonContent, nonce, timestamp);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            client.DefaultRequestHeaders.Add("Accept", "application/json;charset=utf-8");

            HttpResponseMessage response = client.PostAsync(requestURL, new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(response.StatusCode + responseBody);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public List<JobOffer> GetAvailableJobOffers()
        {
            return null;
        }

    }
}

