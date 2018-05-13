using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Origins.API.Controllers;
using Origins.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Origins.API.Tests
{
    public class ProducerTest
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public ProducerTest()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public void AddInformation()
        {
            
        }

    }
}
