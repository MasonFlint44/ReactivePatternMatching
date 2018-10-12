using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;

namespace ReactiveExperiment
{
    public class KeySimulator
    {
        public Subscription<KeyNotification> KeyStream { get; } = new Subscription<KeyNotification>();

        public KeySimulator Down(Key key)
        {
            KeyStream.Broadcast(new KeyNotification(key, KeyEvent.Down));
            return this;
        }

        public KeySimulator Up(Key key)
        {
            KeyStream.Broadcast(new KeyNotification(key, KeyEvent.Up));
            return this;
        }

        public KeySimulator Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return this;
        }
    }
}
