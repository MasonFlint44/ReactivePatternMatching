using System.Windows.Input;

namespace ReactiveExperiment
{
    public interface IKeyTracker
    {
        Subscription<KeyNotification> KeyStream { get; }
    }
}