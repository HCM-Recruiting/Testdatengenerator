using NuGet.Packaging.Signing;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace HCMBackend.Services
{
    public class AuthenticationService
    {
        string clientID = "htlgr";
        string apiSharedKey = "D92CE36654826C4B21C39C77AA226A==";
        string requestURL = "intplay.test.infoniqa.io/ei/services/restProxy/standard/";
        string requestMethod = "";
        string content = "{\"key\": \"value\"}"; // Your POST request body
        string nonce = "J4OCFopV1ykXssVsNqJoEw==";
        string timestamp = DateTimeOffset.UtcNow.ToString("yyyyMMddTHHmmssZ");

        string authorizationHeader;
        string apiUrl;

        HttpClient client;

        public AuthenticationService()
        {
            string signature = CreateSignature(clientID, apiSharedKey, requestURL, requestMethod, content, nonce, timestamp);
            authorizationHeader = $"hmac {clientID}:{signature}";
            apiUrl = $"https://{requestURL}";

            client = new HttpClient()
            {
                DefaultRequestHeaders = { { "Authorization", authorizationHeader }, { "Content-Type", "application/json" } }
            };

            //using (HttpClient client = new HttpClient())
            //{
            //    // Set Authorization header
            //    client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);

            //    // Set Content-Type header for a JSON request
            //    client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            //    // Create POST request
            //    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(content, Encoding.UTF8, "application/json"));

            //    // Handle the response
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string responseBody = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine(responseBody);
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            //    }
            //}
        }

        static string CreateSignature(string clientID, string apiSharedKey, string requestURL, string requestMethod, string content, string nonce, string timestamp)
        {
            string algorithm = "HmacSHA1";

            string signature = CreateHash(clientID, apiSharedKey, algorithm);
            signature = CreateHash(requestURL, signature, algorithm);
            signature = CreateHash(requestMethod, signature, algorithm);
            signature = CreateHash(content, signature, algorithm);
            signature = CreateHash(nonce, signature, algorithm);
            signature = CreateHash(timestamp, signature, algorithm);

            return signature;
        }

        static string CreateHash(string message, string key, string algorithm)
        {
            string hashResult = key;
            if (!string.IsNullOrEmpty(message))
            {
                hashResult = GenerateHash(message, hashResult, algorithm);
            }
            return hashResult;
        }

        static string GenerateHash(string message, string key, string algorithm)
        {
            using (HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}
