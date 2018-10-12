using System;
using System.Diagnostics;

namespace ReactiveExperiment
{
    public class SimulatedKeyTracker : IKeyTracker
    {
        public Subscription<KeyNotification> KeyStream { get; } = new Subscription<KeyNotification>();

        public SimulatedKeyTracker(IObservable<KeyNotification> keySource)
        {
            keySource.Subscribe(notification =>
            {
                Debug.WriteLine(notification);
                KeyStream.Broadcast(notification);
            });
        }
    }
}
