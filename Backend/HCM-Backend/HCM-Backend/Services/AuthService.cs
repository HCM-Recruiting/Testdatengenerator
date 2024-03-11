using ApplicationLib;
using Newtonsoft.Json;
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
        private string clientID = "htlgr";
        private string apiSharedKey = "D92CE36654826C4B21C39C77AA226A==";
        private HttpClient client;

        public AuthService()
        {
            client = new HttpClient();
        }

        private string CreateSignature(string requestURL, string requestMethod, string content, string nonce, string timestamp)
        {
            string algorithm = "HmacSHA1";

            string signature = CreateHash(clientID, apiSharedKey, algorithm);
            Console.WriteLine("HashResult after clientId: " + clientID + " = "+signature);
            signature = CreateHash(requestURL, signature, algorithm);
            Console.WriteLine("HashResult after requestUrl: " + requestURL +" = " +signature);
            signature = CreateHash(requestMethod, signature, algorithm);
            Console.WriteLine("HashResult after requestMethod: " + requestMethod + " = " + signature);
            signature = CreateHash(content, signature, algorithm);
            Console.WriteLine("HashResult after content: " + content + " = " + signature);
            signature = CreateHash(nonce, signature, algorithm);
            Console.WriteLine("HashResult after nonce: " + nonce + " = " + signature);
            signature = CreateHash(timestamp, signature, algorithm);
            Console.WriteLine("HashResult after timestamp: " + timestamp + " = " + signature);

            Console.WriteLine("hmac " + clientID + ":" + algorithm + ":" + timestamp + ":" + nonce + ":" + signature);
            return "hmac " + clientID + ":" + algorithm + ":" + timestamp + ":" + nonce + ":" + signature;
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
            using (var hmac = new HMACSHA1(keyBytes)) // or HMACSHA1, HMACSHA512, etc. based on the algorithm
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = hmac.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }


        public void PostApplicationXML()
        {
            string baseRequestURL = "https://intplay.test.infoniqa.io/ei/services/restProxy/standard/application";
            //string baseReq = "intplay.test.infoniqa.io/ei/services/restProxy/standard/";

            string requestURL = $"{baseRequestURL}";

            Application testApplication = new Application()
            {
                JobOffer = new JobOffer
                {
                    Identifier = "TOP-2023-000006",
                    Id = "8a0bc0a88adab669018adacb06730022"
                },
                IncomingDate = DateTime.Parse("2024-02-19"),
                Applicant = new Applicant
                {
                    Gender = "4",
                    Address = new Address
                    {
                        FirstName = "Emi",
                        LastName = "Lio",
                    }
                }
            };

            //new StringContent(jsonString, Encoding.UTF8, "application/json")

            //Console.WriteLine("serialized object: " + SerializeObjectToXml(testApplication));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("applicant.xml");

            // Access XML elements using XPath or other methods
            string xmlContent = File.ReadAllText("applicant.xml");
            string content = xmlContent.ToString();
            string nonce = "18C31CEBF5CB69D2BFD920E792FEF7FF";
            //string timestamp = DateTimeOffset.UtcNow.ToString("yyyyMMddTHHmmssZ");
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            string authorizationHeader = CreateSignature(baseRequestURL, "POST", content, nonce, timestamp);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            client.DefaultRequestHeaders.Add("Accept", "application/xml;charset=UTF-8");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("hmac",authorizationHeader);
            //client.DefaultRequestHeaders.Accept.Add("application/xml;charset=UTF-8");
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            //    try
            //    {
            //        using (HttpClient httpClient = new HttpClient())
            //        {
            //            httpClient.DefaultRequestHeaders.Add("Accept", "application/xml;charset=UTF-8");
            //            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            //            var httpContent = new StringContent(content.XDocument.ToString(), Encoding.UTF8, "application/xml");
            //            using (HttpResponseMessage HttpResponseMessage = await httpClient.GetAsync(string.Concat(_RootUrl, ""), HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            //            {
            //                if (HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            //                    using (HttpContent HttpContent = HttpResponseMessage.Content)
            //                    {
            //                        string MyContent = await HttpContent.ReadAsStringAsync();
            //                        var root = JsonConvert.DeserializeObject<RootObject>(MyContent);
            //                        ReturnContinentModels = new AsyncObservableCollection<ContentModel>(root.products);
            //                    }
            //            }
            //        }
            //    }
            //    catch (Exception ex) { Console.WriteLine(ex.Message); }
            //    return ReturnContinentModels;
            //}

            HttpResponseMessage response = client.PostAsync(requestURL,new StringContent(xmlContent, Encoding.UTF8, "application/xml")).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public void PostApplicationsJson()
        {
            string baseRequestURL = "https://intplay.test.infoniqa.io/ei/services/restProxy/standard/application";
            //string baseReq = "intplay.test.infoniqa.io/ei/services/restProxy/standard/";

            string requestURL = $"{baseRequestURL}";

            Application testApplication = new Application()
            {
                JobOffer = new JobOffer
                {
                    Identifier = "TOP-2023-000006",
                    Id = "8a0bc0a88adab669018adacb06730022"
                },
                IncomingDate = DateTime.Parse("2024-02-19"),
                Applicant = new Applicant
                {
                    Gender = "4",
                    Address = new Address
                    {
                        FirstName = "Emi",
                        LastName = "Lio",
                    }
                }
            };

            //new StringContent(jsonString, Encoding.UTF8, "application/json")

            //Console.WriteLine("serialized object: " + SerializeObjectToXml(testApplication));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("applicant.xml");

            // Access XML elements using XPath or other methods
            string xmlContent = File.ReadAllText("applicant.xml");
            string content = xmlContent.ToString();
            string nonce = "18C31CEBF5CB69D2BFD920E792FEF7FF";
            //string timestamp = DateTimeOffset.UtcNow.ToString("yyyyMMddTHHmmssZ");
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            string authorizationHeader = CreateSignature(baseRequestURL, "POST", content, nonce, timestamp);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            client.DefaultRequestHeaders.Add("Accept", "application/xml;charset=UTF-8");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("hmac",authorizationHeader);
            //client.DefaultRequestHeaders.Accept.Add("application/xml;charset=UTF-8");
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            //    try
            //    {
            //        using (HttpClient httpClient = new HttpClient())
            //        {
            //            httpClient.DefaultRequestHeaders.Add("Accept", "application/xml;charset=UTF-8");
            //            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            //            var httpContent = new StringContent(content.XDocument.ToString(), Encoding.UTF8, "application/xml");
            //            using (HttpResponseMessage HttpResponseMessage = await httpClient.GetAsync(string.Concat(_RootUrl, ""), HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            //            {
            //                if (HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            //                    using (HttpContent HttpContent = HttpResponseMessage.Content)
            //                    {
            //                        string MyContent = await HttpContent.ReadAsStringAsync();
            //                        var root = JsonConvert.DeserializeObject<RootObject>(MyContent);
            //                        ReturnContinentModels = new AsyncObservableCollection<ContentModel>(root.products);
            //                    }
            //            }
            //        }
            //    }
            //    catch (Exception ex) { Console.WriteLine(ex.Message); }
            //    return ReturnContinentModels;
            //}

            HttpResponseMessage response = client.PostAsync(requestURL, new StringContent(xmlContent, Encoding.UTF8, "application/xml")).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public static string SerializeObjectToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

    }
}

