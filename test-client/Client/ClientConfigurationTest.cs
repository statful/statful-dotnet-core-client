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
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "appsettings.json";

            IStatfulClient client = StatfulClientFactory.CreateStatfulClient(filePath, fileName);
            
            Assert.NotNull(client);
        }
    }
}