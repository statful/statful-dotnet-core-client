namespace Statful.Core.Client.Utils
{
    public interface IHttpBaseAddressBuilder
    {
        IHttpBaseAddressBuilder WithHost(string host);
        IHttpBaseAddressBuilder WithPort(int port);
        IHttpBaseAddressBuilder WithPath(string path);
        IHttpBaseAddressBuilder WithSecureProtocol(bool secure);
        string Build();
    }

    public class HttpBaseAddressBuilder : IHttpBaseAddressBuilder
    {
        public const string DefaultProtocol = HTTPS;
        public const string DefaultHost = "127.0.0.1";
        public const int DefaultPort = 443;
        public const string DefaultPath = "";
        public const bool DefaultSecure = true;
        private const string HTTP = "http";
        private const string HTTPS = "https";

        private string host;
        private string path;
        private int port;
        private string protocol;

        public HttpBaseAddressBuilder()
        {
            this.host = DefaultHost;
            this.port = DefaultPort;
            this.path = DefaultPath;
            this.protocol = DefaultProtocol;
        }

        public IHttpBaseAddressBuilder WithHost(string host)
        {
            if (!string.IsNullOrWhiteSpace(host))
            {
                this.host = host;
            }
            return this;
        }

        public IHttpBaseAddressBuilder WithPort(int port)
        {
            if (port > 0)
            {
                this.port = port;
            }
            return this;
        }

        public IHttpBaseAddressBuilder WithPath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                this.path = path.StartsWith("/") ? path : "/" + path;
            }
            return this;
        }

        public IHttpBaseAddressBuilder WithSecureProtocol(bool secure)
        {
            if (secure)
            {
                this.protocol = HTTPS;
            }
            else
            {
                this.protocol = HTTP;
            }
            return this;
        }

        public string Build()
        {
            return string.Format("{0}://{1}:{2}{3}", this.protocol, this.host, this.port, this.path);
        }
    }
}