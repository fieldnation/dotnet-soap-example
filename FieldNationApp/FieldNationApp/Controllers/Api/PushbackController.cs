using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FieldNationApp.Controllers.Api
{
    public class PushbackController : ApiController
    {
        // POST api/pushback
        public async Task<HttpResponseMessage> Post(HttpRequestMessage req)
        {
            using (MD5 md5 = MD5.Create())
            {
                var json = await req.Content.ReadAsStringAsync();

                // Validate the FN-HASH
                var appSettings = System.Configuration.ConfigurationManager.AppSettings;
                var key = appSettings.Get("FN_HASH_SECRET");
                var source = key + json;
                var fnHash = req.Headers.GetValues("fn-hash").DefaultIfEmpty(string.Empty).First();

                if (!VerifyMd5Hash(md5, source, fnHash))
                {
                    // Failed to validate!
                    return new HttpResponseMessage(HttpStatusCode.Forbidden);
                }

                // Do something with the pushback JSON
                // You'll need to add handlers to handle the various pushbacks you want to listen to.
                // It is also a good idea to serialize the JSON into a POCO.
                // The pushback JSON schema 

                // Check if something failed when managing the pushback data
                // If failed respond with a internal server error
                // Else respond with 'OK'
                var hasFailed = false;
                if (hasFailed)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
        }


        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
