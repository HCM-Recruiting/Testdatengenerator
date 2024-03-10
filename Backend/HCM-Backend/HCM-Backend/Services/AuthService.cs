using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

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
            signature = CreateHash(requestURL, signature, algorithm);
            signature = CreateHash(requestMethod, signature, algorithm);
            signature = CreateHash(content, signature, algorithm);
            signature = CreateHash(nonce, signature, algorithm);
            signature = CreateHash(timestamp, signature, algorithm);

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
            using (var hmac = new HMACSHA256(keyBytes)) // or HMACSHA1, HMACSHA512, etc. based on the algorithm
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = hmac.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }


        public void GetApplications(int pageSize)
        {
            string baseRequestURL = "http://intplay.test.infoniqa.io/ei/services/restProxy/standard/application";
            //string baseReq = "intplay.test.infoniqa.io/ei/services/restProxy/standard/";

            string requestURL = $"{baseRequestURL}?pageSize={pageSize}";

            string content = "";  // No request content for GET
            string nonce = "J4OCFopV1ykXssVsNqJoEw==";
            //string timestamp = DateTimeOffset.UtcNow.ToString("yyyyMMddTHHmmssZ");
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            string authorizationHeader = CreateSignature(baseRequestURL, "GET", content, nonce, timestamp);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("hmac",authorizationHeader);
            //client.DefaultRequestHeaders.Accept.Add(new ("application/json"));
            //client.DefaultRequestHeaders.Add("Accept", "application/json");

            HttpResponseMessage response = client.GetAsync(requestURL).Result;

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

    }
}

