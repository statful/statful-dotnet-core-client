using System;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.Configuration
{
    public class ClientConfiguration : IClientConfiguration
    {
        private ClientConfiguration(string host,
                                    int port,
                                    bool secure,
                                    int timeout,
                                    string token,
                                    string app,
                                    bool dryrun,
                                    bool cacheDns,
                                    string tags,
                                    int flushInterval,
                                    int sampleRate,
                                    string ns,
                                    string path,
                                    string transport,
                                    ILogger logger)
        {
            this.host = host;
            this.port = port;
            this.secure = secure;
            this.timeout = timeout;
            this.token = token;
            this.app = app;
            this.dryrun = dryrun;
            this.cacheDns = cacheDns;
            this.tags = tags;
            this.flushInterval = flushInterval;
            this.sampleRate = sampleRate;
            this.ns = ns;
            this.path = path;
            this.transport = transport;
            this.logger = logger;
        }

        private string host;
        private int port;
        private bool secure;
        private int timeout;
        private string token;
        private string app;
        private bool dryrun;
        private bool cacheDns;
        private string tags;
        private int flushInterval;
        private int sampleRate;
        private string ns;
        private string path;
        private string transport;
        private ILogger logger;

        public string Host
        {
            get { return this.host; }
        }

        public int Port
        {
            get { return this.port; }
        }

        public string Transport
        {
            get { return this.transport; }
        }

        public bool Secure
        {
            get { return this.secure; }
        }

        public int Timeout
        {
            get { return this.timeout; }
        }

        public string Token
        {
            get { return this.token; }
        }

        public string App
        {
            get { return this.app; }
        }

        public bool Dryrun
        {
            get { return this.dryrun; }
        }

        public ILogger Logger
        {
            get { return this.logger; }
        }

        public bool CacheDns
        {
            get { return this.cacheDns; }
        }

        public string Tags
        {
            get { return this.tags; }
        }

        public int FlushInterval
        {
            get { return this.flushInterval; }
        }

        public int SampleRate
        {
            get { return this.sampleRate; }
        }

        public string Namespace
        {
            get { return this.ns; }
        }

        public string Path
        {
            get { return this.path; }
        }

        public class Builder
        {
            string host = "api.statful.com";
            int port = 443;
            bool secure = true;
            int timeout = 2000;
            string token = String.Empty;
            string app = string.Empty;
            bool dryrun = false;
            bool cacheDns = false;
            string tags = string.Empty;
            int flushInterval = 10000;
            int sampleRate = 100;
            string ns = "application";
            string path = "tel/v2.0/metrics";
            string transport = "http";
            ILogger logger = new SilentLogger();

            public Builder withHost(String host)
            {
                this.host = host;
                return this;
            }

            public Builder withPort(int port)
            {
                this.port = port;
                return this;
            }

            public Builder withSecure(bool secure)
            {
                this.secure = secure;
                return this;
            }

            public Builder withTimeout(int timeout)
            {
                this.timeout = timeout;
                return this;
            }

            public Builder withToken(String token)
            {
                this.token = token;
                return this;
            }

            public Builder withApp(String app)
            {
                this.app = app;
                return this;
            }

            public Builder withDryRun(bool dryRun)
            {
                this.dryrun = dryRun;
                return this;
            }

            public Builder withCacheDNS(bool cacheDns)
            {
                this.cacheDns = cacheDns;
                return this;
            }

            public Builder withTags(String tags)
            {
                this.tags = tags;
                return this;
            }
            public Builder withFlushInterval(int flushInterval)
            {
                this.flushInterval = flushInterval;
                return this;
            }
            public Builder withSamplerate(int sampleRate)
            {
                this.sampleRate = sampleRate;
                return this;
            }
            public Builder withNamespace(String ns)
            {
                this.ns = ns;
                return this;
            }
            public Builder withPath(String path)
            {
                this.path = path;
                return this;
            }
            public Builder withTransport(String transport)
            {
                this.transport = transport;
                return this;
            }
            public Builder withLogger(ILogger logger)
            {
                this.logger = logger;
                return this;
            }

            public ClientConfiguration Build()
            {
                return new ClientConfiguration(host, port, secure, timeout, token, app, dryrun, cacheDns, tags, flushInterval, sampleRate, ns, path, transport, logger);
            }
        }
    }
}