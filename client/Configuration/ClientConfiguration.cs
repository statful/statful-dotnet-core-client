using System;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.Configuration
{
    public class ClientConfiguration : IClientConfiguration
    {
        public ClientConfiguration(string prefix, string transport) {
            if (string.IsNullOrWhiteSpace(prefix)) {
                throw new ArgumentException("prefix cannot be null or empty string!","prefix");
            }
            if (string.IsNullOrWhiteSpace(transport)) {
                throw new ArgumentException("transport cannot be null or empty string!","transport");
            }
            this.Prefix = prefix;
            this.Transport = transport;
        }

        private string host = "127.0.0.1";
        private int port = 80;
        private bool secure = true;
        private int timeout = 2000;
        private string token = String.Empty;
        private string app = string.Empty;
        private bool dryrun = false;
        private bool cacheDns = false;
        private string tags = string.Empty;
        private int flushInterval = 10000;
        private int sampleRate = 100;
        private string ns = "application";
        private string path = string.Empty;
        private string transport = String.Empty;
        private string prefix = String.Empty;
        private ILogger logger = new SilentLogger();
        
        public string Host
        {
            get { return this.host; }
            set { this.host = value; }
        }

        public int Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        public string Prefix
        {
            get { return this.prefix; }
            set { this.prefix = value; }
        }

        public string Transport
        {
            get { return this.transport; }
            set { this.transport = value; }
        }

        public bool Secure
        {
            get { return this.secure; }
            set { this.secure = value; }
        }

        public int Timeout
        {
            get { return this.timeout; }
            set { this.timeout = value; }
        }

        public string Token
        {
            get { return this.token; }
            set { this.token = value; }
        }

        public string App
        {
            get { return this.app; }
            set { this.app = value; }
        }

        public bool Dryrun
        {
            get { return this.dryrun; }
            set { this.dryrun = value; }
        }

        public ILogger Logger
        {
            get { return this.logger; }
            set
            {
                if (value == null) {
                    return;
                }
                this.logger = value;
            }
        }

        public bool CacheDns
        {
            get { return this.cacheDns; }
            set { this.cacheDns = value; }
        }

        public string Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public int FlushInterval
        {
            get { return this.flushInterval; }
            set { this.flushInterval = value; }
        }

        public int SampleRate
        {
            get { return this.sampleRate; }
            set { this.sampleRate = value; }
        }

        public string Namespace
        {
            get { return this.ns; }
            set { this.ns = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
    }
}