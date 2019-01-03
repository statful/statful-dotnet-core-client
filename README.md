# statful-dotnet-core-client
Statful client for .NET Core applications. This client is intended to gather metrics and send them to Statful.

## Table of Contents

* [Supported Versions of .NET Core](#supported-versions-of-.net-core)
* [Installation](#installation)
* [Quick Start](#quick-start)
* [Defaults Configuration Per Method](#defaults-configuration-per-method)
* [Client Configuration](#client-configuration)
* [Reference](#reference)
* [Authors](#authors)
* [License](#license)

## Supported Versions of .NET Core

| Statful client Version | Tested .NET Core versions  |
|:---|:---|
| 1.x.x | `2.1.500`|

## Quick start

Install the StatfulDotnetCoreClient nuget package and you are ready to use it. 

You can configure the client programmatically or by using a configuration file.

## Programmatically
Create an instance of `ClientConfiguration` to set all desired parameters like the following sample:
```c#
IClientConfiguration configuration = new ClientConfiguration.Builder()
                                                            .withHost("host")
                                                            .withPort(443)
                                                            .withToken("token")
                                                                .
                                                                .
                                                                .
                                                            Build();
```
## Configuration File
The configuration file is a json document that follows the following structure:

```c#
{
    "StatfulClient":{
        "Settings":{
            "host": "api.statful.com",
            "port": 443,
            "secure": true,
            "timeout": 1000,
            "token": "token",
            "app": "foo",
            "dryrun": false,
            "tags": ["tag1","value1"],
            "sampleRate": 100,
            "ns": "application",
            "flushInterval": 10000,
            "maxBufferSize": 5000,
            "path": "tel/v2.0/metrics",
            "transport": "http"
        }
    }
}

```

## Initialize client

Then create an instance of `IStatfulClient` by using `StatfulClientFactory`:

```c# 
IStatfulClient client = StatfulClientFactory.CreateStatfulClient("pathToFile", "filename"); // Creates configured client from a file

OR

IStatfulClient client = StatfulClientFactory.CreateStatfulClient(configuration); // Creates configured client programmatically
```
Finally, you can send a metric just like this:

```c#
client.Time("name", 10, "tag1=sample", null);
```

### Defaults Configuration Per Method

`IStatfulClient` has 4 methods to send metrics:

    Inc -> appends a prefix `counter` to the metric name and, if none are specified, sets `sum` and `count` aggregations as default;
    
    Time -> appends a prefix `timer` to the metric name and, if none are specified, sets `avg`, `p90` and `count` aggregations as default;
    
    Gauge -> appends a prefix `gauge` to the metric name and, if none are specified, sets `last` aggregation as default;
    
    Put -> sends the metric as is;

## Client Configuration

General configuration for a Statful client.

    * host [optional] [default: 'api.statful.com']
    * port [optional] [default: 443]
    * secure [optional] [default: true] - enable or disable https
    * timeout [optional] [default: 2000ms] - timeout for http transport
    * token - An authentication token to send to Statful
    * app [optional] - if specified set a tag ‘app=foo’
    * dryrun [optional] [default: false] - debug log metrics when flushing the buffer
    * tags [optional] - global list of tags to set, these are merged with custom tags set on method calls with priority to custom tags
    * sampleRate [optional] [default: 100] [between: 1-100] - global rate sampling
    * ns [optional] [default: 'application'] - default namespace
    * flushInterval [optional] [default: 10000] - defines an interval to periodically flush the buffer based on time
    * maxBufferSize [optional] [default: 5000] - defines how many metrics at max are kept in the buffer between forced flushes
    * path [optional] [default: 'tel/v2.0/metrics'] - defines the endpoint to send metrics to
    * transport [optional] [default: 'http'] - type of transport to be used when sending metrics to statful (UDP/HTTP)
    * logger [optional] [default: SilentLogger] - defines logger library

## Authors

[Mindera - Software Craft](https://github.com/Mindera)

## License

Statful .NET Core Client is available under the MIT license. See the [LICENSE](https://raw.githubusercontent.com/statful/statful-dotnet-core-client/master/LICENSE) file for more information.