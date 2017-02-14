using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string userInput = "";
            TokenResponse tokenResponse ;
            while (userInput != "x") {
                Console.Clear();
                tokenResponse = null;
                Console.WriteLine("1: ClientCredentials");
                Console.WriteLine("2: ResourceOwnerPassword");
                Console.WriteLine("x: Exit");
                Console.Write("Please enter your choice: ");
                userInput = Console.ReadLine();
                switch (userInput) {
                    case "1":
                        tokenResponse = RequestTokenWithClientCredentialsToIdentityServer().Result;
                        break;
                    case "2":
                        tokenResponse = RequestTokenWithResourceOwnerPasswordToIdentityServer().Result;
                        break;
                }
                if (tokenResponse != null)
                {
                    UseTokenToTalkToApi(tokenResponse);
                    Console.ReadLine();
                }
            }
            

            //Console.ReadLine();

        }

        public static async Task<TokenResponse> RequestTokenWithResourceOwnerPasswordToIdentityServer()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("bob@gmail.com", "Pa$$w0rd", "api1");

            if (tokenResponse.IsError) {
                Console.WriteLine(tokenResponse.Error);
            }else {
                Console.WriteLine(tokenResponse.Json);
            }


            var tokenResponseProfile = await tokenClient.RequestResourceOwnerPasswordAsync("bob@gmail.com", "Pa$$w0rd", "profile");
            if (tokenResponseProfile.IsError)
            {
                Console.WriteLine("tokenResponseProfile error: " + tokenResponseProfile.Error);
            }
            else
            {
                Console.WriteLine("tokenResponseProfile: " + tokenResponseProfile.Json);
            }
            return tokenResponse;
        }


        public static async Task<TokenResponse> RequestTokenWithClientCredentialsToIdentityServer() {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                //return;
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            return tokenResponse;
        }

        public static async void UseTokenToTalkToApi(TokenResponse tokenResponse) {
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
