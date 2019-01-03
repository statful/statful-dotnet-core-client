using NUnit.Framework;
using Moq;
using Statful.Core.Client.Buffer;
using Statful.Core.Client.Client;
using Statful.Core.Client.Configuration;
using Statful.Core.Client.Utils;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.UnitTests.Client
{
    [TestFixture]
    public class StatfulClientUnitTests
    {
        private IClientConfiguration configurationGateway;
        private Mock<ILuckyCoin> luckyCoin;
        private Mock<ITimestamp> timestamp;

        [SetUp]
        public virtual void SetUp()
        {
            this.luckyCoin = new Mock<ILuckyCoin>();
            this.timestamp = new Mock<ITimestamp>();

            this.configurationGateway = new ConfigurationFake
            {
                Prefix = "prefix",
                Namespace = "domain",
                App = "appname",
                Tags = "tag1=value1",
                SampleRate = 50
            };
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.luckyCoin = null;
            this.configurationGateway = null;
            this.timestamp = null;
        }

        [TestFixture]
        public class IncTests : StatfulClientUnitTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
            }

            [TearDown]
            public override void TearDown()
            {
                base.TearDown();
            }

            [Test]
            public void Should_save_counter_message_when_tags_are_not_defined()
            {
                var messageBuffer = new Mock<IMessageBuffer>();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer.Object, this.timestamp.Object, this.luckyCoin.Object);

                client.Inc("inc", 10, null, "sum,count", 10, 50);

                messageBuffer.Verify((mb) => mb.Save(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void Should_save_counter_message_when_aggregations_are_not_defined()
            {
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Inc("inc", 10, "tag1=value1", null, 10, 50);

                var expected = "domain.counter.inc,app=appname,tag1=value1 10 1000 sum,count,10 50";

                Assert.AreEqual(expected, messageBuffer.message);
            }

            [Test]
            public void Should_save_counter_message_when_aggregations_are_defined()
            {
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Inc("inc", 10, "tag1=value1", "avg", 10, 50);

                var expected = "domain.counter.inc,app=appname,tag1=value1 10 1000 avg,10 50";

                Assert.AreEqual(expected, messageBuffer.message);
            }

            [Test]
            public void Should_not_save_counter_message_When_message_is_out_sample_rate()
            {
                var messageBuffer = new Mock<IMessageBuffer>();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(false);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer.Object, this.timestamp.Object, this.luckyCoin.Object);

                client.Inc("inc", 10, "tag1=value1", "sum,count", 10, 50);

                messageBuffer.Verify((mb) => mb.Save(It.IsAny<string>()), Times.Never);
            }

            [Test]
            public void Should_save_counter_message_When_message_is_within_sample_rate()
            {
                var messageBuffer = new MessageBufferFake();
                var expected = "domain.counter.inc,app=appname,tag1=value1 10 1000 sum,count,10 50";
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Inc("inc", 10, "tag1=value1", "sum,count", 10, 50);

                Assert.AreEqual(expected, messageBuffer.message);
            }
        }

        [TestFixture]
        public class GaugeTests : StatfulClientUnitTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
            }

            [TearDown]
            public override void TearDown()
            {
                base.TearDown();
            }

            [Test]
            public void Should_save_gauge_message_when_tags_are_not_defined()
            {
                var messageBuffer = new Mock<IMessageBuffer>();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer.Object, this.timestamp.Object, this.luckyCoin.Object);

                client.Gauge("gauge", 10, null, "sum,count", 10, 50);

                messageBuffer.Verify((mb) => mb.Save(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void Should_save_gauge_message_when_aggregations_are_not_defined()
            {
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Gauge("name", 10, "tag1=value1", null, 10, 50);

                var expected = "domain.gauge.name,app=appname,tag1=value1 10 1000 last,10 50";

                Assert.AreEqual(expected, messageBuffer.message);
            }

            [Test]
            public void Should_save_gauge_message_when_aggregations_are_defined()
            {
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Gauge("name", 10, "tag1=value1", "avg", 10, 50);

                var expected = "domain.gauge.name,app=appname,tag1=value1 10 1000 avg,10 50";

                Assert.AreEqual(expected, messageBuffer.message);
            }

            [Test]
            public void Should_not_save_gauge_message_When_is_out_sample_rate()
            {
                var messageBuffer = new Mock<IMessageBuffer>();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(false);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer.Object, this.timestamp.Object, this.luckyCoin.Object);

                client.Gauge("name", 10, "tag1=value1", "sum,count", 10, 50);

                messageBuffer.Verify((mb) => mb.Save(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void Should_save_gauge_message_When_is_within_sample_rate()
            {
                var expected = "domain.gauge.name,app=appname,tag1=value1 10 1000 sum,count,10 50";
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Gauge("name", 10, "tag1=value1", "sum,count", 10, 50);

                Assert.AreEqual(expected, messageBuffer.message);
            }
        }

        [TestFixture]
        public class TimerTests : StatfulClientUnitTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
            }

            [TearDown]
            public override void TearDown()
            {
                base.TearDown();
            }

            [Test]
            public void Should_save_time_message_when_tags_are_not_defined()
            {
                var messageBuffer = new Mock<IMessageBuffer>();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer.Object, this.timestamp.Object, this.luckyCoin.Object);

                client.Time("time", 10, null, "sum,count", 10, 50);

                messageBuffer.Verify((mb) => mb.Save(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void Should_save_time_message_when_aggregations_are_not_defined()
            {
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Time("name", 10, "tag1=value1", null, 10, 50);

                var expected = "domain.timer.name,app=appname,tag1=value1 10 1000 avg,p90,count,10 50";

                Assert.AreEqual(expected, messageBuffer.message);
            }

            [Test]
            public void Should_save_time_message_when_aggregations_are_defined()
            {
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Time("name", 10, "tag1=value1", "avg", 10, 50);

                var expected = "domain.timer.name,app=appname,tag1=value1 10 1000 avg,10 50";

                Assert.AreEqual(expected, messageBuffer.message);
            }

            [Test]
            public void Should_not_save_time_message_When_message_is_out_sample_rate()
            {
                var messageBuffer = new Mock<IMessageBuffer>();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(false);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer.Object, this.timestamp.Object, this.luckyCoin.Object);

                client.Time("name", 10, "tag1=value1", "sum,count", 10, 50);

                messageBuffer.Verify((mb) => mb.Save(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void Should_save_time_message_When_message_is_within_sample_rate()
            {
                var expected = "domain.timer.time,app=appname,tag1=value1 10 1000 sum,count,10 50";
                var messageBuffer = new MessageBufferFake();
                this.luckyCoin.Setup((lc) => lc.Throw(It.IsAny<int>())).Returns(true);
                this.timestamp.Setup((ts) => ts.Now()).Returns(1000);
                var client = new StatfulClient(this.configurationGateway, messageBuffer, this.timestamp.Object, this.luckyCoin.Object);

                client.Time("time", 10, "tag1=value1", "sum,count", 10, 50);

                Assert.AreEqual(expected, messageBuffer.message);
            }
        }
    }

    internal class MessageBufferFake : IMessageBuffer
    {
        public string message = string.Empty;

        public void Dispose() { }

        public void Save(string message)
        {
            this.message = message;
        }

        public void Flush() { }
    }

    internal class ConfigurationFake : IClientConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Prefix { get; set; }
        public string Transport { get; set; }
        public bool Secure { get; set; }
        public int Timeout { get; set; }
        public string Token { get; set; }
        public string App { get; set; }
        public bool Dryrun { get; set; }

        public ILogger Logger
        {
            get { return new Mock<ILogger>().Object; }
        }

        public bool CacheDns { get; set; }
        public string Tags { get; set; }
        public int FlushInterval { get; set; }
        public int SampleRate { get; set; }
        public string Namespace { get; set; }
        public string Path { get; set; }
    }
}