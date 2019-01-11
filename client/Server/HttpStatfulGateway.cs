using System;
using System.Net.Http;
using System.Threading.Tasks;
using Statful.Core.Client.Configuration;
using Statful.Core.Client.Utils;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.Server
{
    public class HttpStatfulGateway : IStatfulGateway
    {
        private readonly ILogger logger;
        private HttpClient client;
        private bool isDisposed;

        public HttpStatfulGateway(IClientConfiguration config, HttpClient client = null)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config", "Configuration cannot be null object!");
            }
            if (client == null && string.IsNullOrWhiteSpace(config.Token))
            {
                throw new ArgumentException("Telemetron API token not defined!");
            }

            this.logger = config.Logger;
            this.InitHttpClient(config, client);
        }

        public void SendAsync(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message", string.Format("Cannot send null message to {0}.", this.client.BaseAddress.AbsoluteUri));
            }

            this.logger.InfoFormat("sending to {0} at {1} message: {2}", this.client.BaseAddress.AbsoluteUri, DateTime.Now.ToLongTimeString(), message);

            Task.Run(async () =>
            {
                try
                {
                    var result = await this.client.PutAsync("", new StringContent(message)).ConfigureAwait(false);
                    this.logger.DebugFormat("Request: {0}", result.RequestMessage.ToString());
                    this.logger.DebugFormat("Response: {0}", result.ToString());
                }
                catch (Exception ex)
                {
                    this.logger.Error("Error occured while sending messages through http/https.");
                    this.logger.ErrorFormat("Exception Message: {0}", ex.Message);
                    this.logger.DebugFormat("Stack Trace: {0}", ex.StackTrace);
                }
            });
        }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void InitHttpClient(IClientConfiguration config, HttpClient client)
    {
        if (client == null)
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(this.BuildUri(config));
            this.client.DefaultRequestHeaders.Add("User-Agent", "Telemetry-client-csharp");
            this.client.DefaultRequestHeaders.Add("M-Api-Token", config.Token);
            this.client.Timeout = TimeSpan.FromMilliseconds(config.Timeout);
        }
        else
        {
            this.client = client;
        }
    }

    private string BuildUri(IClientConfiguration config)
    {
        return new HttpBaseAddressBuilder()
            .WithHost(config.Host)
            .WithPath(config.Path)
            .WithSecureProtocol(config.Secure)
            .WithPort(config.Port)
            .Build();
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }
        if (disposing)
        {
            this.client.CancelPendingRequests();
            this.client.Dispose();
            this.client = null;
        }

        this.isDisposed = true;
    }

    ~HttpStatfulGateway()
    {
        this.Dispose(false);
    }

    public class Builder
    {
        IClientConfiguration configuration;
        HttpClient client;

        public Builder withConfiguration(IClientConfiguration configuration)
        {
            this.configuration = configuration;
            return this;
        }
        public Builder withHttpClient(HttpClient client)
        {
            this.client = client;
            return this;
        }

        public HttpStatfulGateway build()
        {
            return new HttpStatfulGateway(configuration, client);
        }
    }
}
}