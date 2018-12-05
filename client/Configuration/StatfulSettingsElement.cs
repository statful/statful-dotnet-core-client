using System.Configuration;

namespace Statful.Core.Client.Configuration
{
    public class StatfulSettingsElement : ConfigurationElement
    {
        private const string HOST = "host";
        private const string PORT = "port";
        private const string PREFIX = "prefix";
        private const string TRANSPORT = "transport";
        private const string SECURE = "secure";
        private const string TIMEOUT = "timeout";
        private const string TOKEN = "token";
        private const string APP = "app";
        private const string DRYRUN = "dryrun";
        private const string CACHE_DNS = "cache_dns";
        private const string TAGS = "tags";
        private const string SAMPLE_RATE = "sample_rate";
        private const string FLUSH_INTERVAL = "flush_interval";
        private const string DOMAIN = "domain";
        private const string PATH = "path";
        private const string PROTOCOL = "protocol";

        [ConfigurationProperty(HOST, IsRequired = false, DefaultValue = "127.0.0.1")]
        public string Host
        {
            get { return (string) this[HOST]; }
            set { this[HOST] = value; }
        }

        [ConfigurationProperty(PORT, IsRequired = false, DefaultValue = 2013)]
        public int Port
        {
            get { return (int) this[PORT]; }
            set { this[PORT] = value; }
        }

        [ConfigurationProperty(PREFIX, IsRequired = true)]
        public string Prefix
        {
            get { return (string) this[PREFIX]; }
            set { this[PREFIX] = value; }
        }

        [ConfigurationProperty(TRANSPORT, IsRequired = true)]
        public string Transport
        {
            get { return (string) this[TRANSPORT]; }
            set { this[TRANSPORT] = value; }
        }

        [ConfigurationProperty(SECURE, IsRequired = false, DefaultValue = true)]
        public bool Secure
        {
            get { return (bool) this[SECURE]; }
            set { this[SECURE] = value; }
        }

        [ConfigurationProperty(TIMEOUT, IsRequired = false, DefaultValue = 2000)]
        public int Timeout
        {
            get { return (int) this[TIMEOUT]; }
            set { this[TIMEOUT] = value; }
        }

        [ConfigurationProperty(TOKEN, IsRequired = false, DefaultValue = "")]
        public string Token
        {
            get { return (string) this[TOKEN]; }
            set { this[TOKEN] = value; }
        }

        [ConfigurationProperty(APP, IsRequired = false, DefaultValue = "")]
        public string App
        {
            get { return (string) this[APP]; }
            set { this[APP] = value; }
        }

        [ConfigurationProperty(DRYRUN, IsRequired = false, DefaultValue = false)]
        public bool Dryrun
        {
            get { return (bool) this[DRYRUN]; }
            set { this[DRYRUN] = value; }
        }

        [ConfigurationProperty(CACHE_DNS, IsRequired = false, DefaultValue = false)]
        public bool CacheDns
        {
            get { return (bool) this[CACHE_DNS]; }
            set { this[CACHE_DNS] = value; }
        }

        [ConfigurationProperty(TAGS, IsRequired = false, DefaultValue = "")]
        public string Tags
        {
            get { return (string) this[TAGS]; }
            set { this[TAGS] = value; }
        }

        [ConfigurationProperty(SAMPLE_RATE, IsRequired = false, DefaultValue = 100)]
        public int SampleRate
        {
            get { return (int) this[SAMPLE_RATE]; }
            set { this[SAMPLE_RATE] = value; }
        }

        [ConfigurationProperty(FLUSH_INTERVAL, IsRequired = false, DefaultValue = 10000)]
        public int FlushInterval
        {
            get { return (int) this[FLUSH_INTERVAL]; }
            set { this[FLUSH_INTERVAL] = value; }
        }

        [ConfigurationProperty(DOMAIN, IsRequired = false, DefaultValue = "application")]
        public string Namespace
        {
            get { return (string) this[DOMAIN]; }
            set { this[DOMAIN] = value; }
        }

        [ConfigurationProperty(PATH, IsRequired = false, DefaultValue = "")]
        public string Path
        {
            get { return (string) this[PATH]; }
            set { this[PATH] = value; }
        }

        [ConfigurationProperty(PROTOCOL, IsRequired = false, DefaultValue = "")]
        public string Protocol
        {
            get { return (string) this[PROTOCOL]; }
            set { this[PROTOCOL] = value; }
        }
    }
}