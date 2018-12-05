using System;
using System.Collections.Concurrent;
using System.Timers;
using Statful.Core.Client.Server;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.Buffer
{
    internal class MessageBuffer : IMessageBuffer
    {
        private readonly int flushinterval;
        private readonly object sharedstatelock = new object();
        private readonly IStatfulGateway statfulGateway;
        private readonly ILogger logger;
        private bool isDisposed;
        private ConcurrentQueue<string> queue;
        private Timer timer;

        public MessageBuffer(IStatfulGateway statfulGateway, ILogger logger, int flushinterval) {
            this.statfulGateway = statfulGateway;
            this.logger = logger;
            this.flushinterval = flushinterval;
            this.queue = new ConcurrentQueue<string>();
            this.StartTimer();
        }

        public void Save(string message) {
            if (message == null) {
                throw new ArgumentNullException(nameof(message), "Message cannot be null");
            }
            lock (this.sharedstatelock) {
                this.queue.Enqueue(message);
            }
        }

        public void Flush() {
            lock (this.sharedstatelock) {
                try {
                    if (!this.queue.IsEmpty) {
                        var message = string.Join("\n", this.queue.ToArray());
                        this.statfulGateway.SendAsync(message);
                        this.queue = new ConcurrentQueue<string>();
                    }
                }
                catch (Exception ex)
                {
                    this.logger.Error("Error occured while flushing messages.");
                    this.logger.ErrorFormat("Exception Message: {0}", ex.Message);
                    this.logger.DebugFormat("Stack Trace: {0}", ex.StackTrace);
                }
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void TimerTick(object sender, object e) {
            this.Flush();
        }

        private void SetupTimer() {
            this.timer = new Timer {Interval = this.flushinterval};
            this.StartTimer();
        }

        private void StartTimer() {
            if (this.timer == null) {
                this.SetupTimer();
                return;
            }
            this.timer.Elapsed += this.TimerTick;
            this.timer.Start();
        }

        private void StopTimer() {
            if (this.timer != null && this.timer.Enabled) {
                this.timer.Stop();
                this.timer.Elapsed -= this.TimerTick;
            }
        }

        private void Dispose(bool disposing) {
            if (this.isDisposed) {
                return;
            }
            if (disposing) {
                this.Flush();
                if (this.timer != null) {
                    this.StopTimer();
                    this.timer.Dispose();
                }
            }

            this.isDisposed = true;
        }

        ~MessageBuffer() {
            this.Dispose(false);
        }
    }
}