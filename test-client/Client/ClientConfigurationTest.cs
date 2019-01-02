using NUnit.Framework;
using Moq;
using Statful.Core.Client.Buffer;
using Statful.Core.Client.Client;
using Statful.Core.Client.Configuration;
using Statful.Core.Client;
using System;
using System.IO;

namespace test_client.Client
{
    public class ClientConfigurationTest
    {
        [Test]

        public void Test_client_configuration_from_file(){
            IStatfulClient client = StatfulClientFactory.CreateStatfulClient("/home/francisco/IdeaProjects/statful-dotnet-core-client/test-client", "appsettings.json");
            
            Assert.NotNull(client);
        }
    }
}