using System.Windows.Input;

namespace ReactiveExperiment
{
    public class KeyNotification
    {
        public Key Key { get; }
        public KeyEvent KeyEvent { get; }

        public KeyNotification(Key key, KeyEvent keyEvent)
        {
            Key = key;
            KeyEvent = keyEvent;
        }

        public override string ToString()
        {
            return $"Key: {Key}, KeyEvent: {KeyEvent}";
        }
    }
}
