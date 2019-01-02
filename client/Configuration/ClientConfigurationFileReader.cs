using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Statful.Core.Client.Utils;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.Configuration
{
    public class ClientConfigurationFileReader : System.Configuration.ConfigurationSection, IClientConfiguration
    {
        public const string SectionName = "StatfulClient";
        private const string SETTINGS = "Settings";
        private ILogger logger;

        public ClientConfigurationFileReader(string path, string filename)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(filename, optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            settings = configuration.GetSection(SectionName).GetSection(SETTINGS).Get<StatfulSettingsElement>();
            logger = new Log4NetLogger();
        }

        [ConfigurationProperty(SETTINGS)]
        private StatfulSettingsElement settings
        {
            get { return (StatfulSettingsElement)base[SETTINGS]; }
            set { base[SETTINGS] = value; }
        }

        public string Host
        {
            get { return settings.Host; }
        }

        public int Port
        {
            get { return settings.Port; }
        }

        public string App
        {
            get { return settings.App; }
        }

        public bool Dryrun
        {
            get { return settings.Dryrun; }
        }

        public ILogger Logger
        {
            get { return logger; }
        }

        public bool CacheDns
        {
            get { return settings.CacheDns; }
        }

        public string Tags
        {
            get { return settings.Tags; }
        }

        public string Transport
        {
            get { return settings.Transport; }
        }

        public bool Secure
        {
            get { return settings.Secure; }
        }

        public int FlushInterval
        {
            get { return settings.FlushInterval; }
        }

        public int SampleRate
        {
            get { return settings.SampleRate; }
        }

        public string Namespace
        {
            get { return settings.Namespace; }
        }

        public string Token
        {
            get { return settings.Token; }
        }

        public string Path
        {
            get { return settings.Path; }
        }

        public int Timeout
        {
            get { return settings.Timeout; }
        }
    }
}