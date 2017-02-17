using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Http;
using System.Web.Http.SelfHost;
using System.Web.Http;

namespace GraphQLNET
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Uri _baseAddress = new Uri("http://localhost:60064/");

            
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);

            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );

            // Create server
            var server = new HttpSelfHostServer(config);
            
            // Start listening
            server.OpenAsync().Wait();
            Console.WriteLine("Web API Self hosted on " + _baseAddress + " Hit ENTER to exit...");
            Console.ReadLine();
            server.CloseAsync().Wait();


        }
    }
}
