using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows.Input;

namespace ReactiveExperiment
{
    public class Hotkey : IHotkey
    {
        private readonly HashSet<Key> _keysPressed = new HashSet<Key>();
        private IKeyTracker _keyTracker;

        private ModifierKeys _modifiers;
        public ModifierKeys Modifiers
        {
            get => _modifiers;
            set
            {
                _modifiers = value;

                var alt = _keyTracker.KeyStream.Where(key => key.Key == Key.LeftAlt || key.Key == Key.RightAlt);
                var ctrl = _keyTracker.KeyStream.Where(key => key.Key == Key.LeftCtrl || key.Key == Key.RightCtrl);
                var shift = _keyTracker.KeyStream.Where(key => key.Key == Key.LeftShift || key.Key == Key.RightShift);
                var win = _keyTracker.KeyStream.Where(key => key.Key == Key.LWin || key.Key == Key.RWin);
            }
        }

        public IList<Key> Keys { get; set; } = new List<Key>();
        public IObservable<HotkeyPressedNotification> Pressed { get; } = new Subscription<HotkeyPressedNotification>();

        public Hotkey(IKeyTracker keyTracker)
        {
            _keyTracker = keyTracker;

            var pattern =
                new PatternMatcher<Key>(keyTracker.KeyStream.Where(x => x.KeyEvent == KeyEvent.Down).Select(x => x.Key))
                    .Or(Key.LeftAlt)
                    .Or(Key.RightAlt)
                    .And(new PatternMatcher<Key>(keyTracker.KeyStream.Where(x => x.KeyEvent == KeyEvent.Down).Select(x => x.Key))
                        .Or(Key.LeftCtrl)
                        .Or(Key.RightCtrl));

            pattern.Stream.Subscribe(tuple =>
            {
                Console.WriteLine($"Key 1: {tuple.Item1}, Key 2: {tuple.Item2}");
                Console.ReadLine();
            });

            //var leftAlt = _keyTracker.KeyStream.Where(key => key.Key == Key.LeftAlt);
            //var rightAlt = _keyTracker.KeyStream.Where(key => key.Key == Key.RightAlt);
            //var latestAlt = leftAlt.Merge(rightAlt);
            //var alt = Observable.CombineLatest(leftAlt, rightAlt, async (left, right) =>
            //{
            //    if (left.KeyEvent == KeyEvent.Down) { return left; }
            //    if (right.KeyEvent == KeyEvent.Down) { return right; }

            //    return await latestAlt.LastAsync();
            //});
            //var alt = _keyTracker.KeyStream.Where(key => key.Key == Key.LeftAlt || key.Key == Key.RightAlt);

            //var altPressed = _keyTracker.Pressed.Where(x => x == Key.LeftAlt || x == Key.RightAlt)
            //    .Select(x => ModifierKeys.Alt).StartWith(ModifierKeys.None);
            //var ctrlPressed = _keyTracker.Pressed.Where(x => x == Key.LeftCtrl || x == Key.RightCtrl)
            //    .Select(x => ModifierKeys.Control).StartWith(ModifierKeys.None);
            //var shiftPressed = _keyTracker.Pressed.Where(x => x == Key.LeftShift || x == Key.RightShift)
            //    .Select(x => ModifierKeys.Shift).StartWith(ModifierKeys.None);
            //var winPressed = _keyTracker.Pressed.Where(x => x == Key.LWin || x == Key.RWin)
            //    .Select(x => ModifierKeys.Windows).StartWith(ModifierKeys.None);

            //var altUnpressed = _keyTracker.Unpressed.Where(x => x == Key.LeftAlt || x == Key.RightAlt)
            //    .Select(x => ModifierKeys.Alt).StartWith(ModifierKeys.None);
            //var ctrlUnpressed = _keyTracker.Unpressed.Where(x => x == Key.LeftCtrl || x == Key.RightCtrl)
            //    .Select(x => ModifierKeys.Control).StartWith(ModifierKeys.None);
            //var shiftUnpressed = _keyTracker.Unpressed.Where(x => x == Key.LeftShift || x == Key.RightShift)
            //    .Select(x => ModifierKeys.Shift).StartWith(ModifierKeys.None);
            //var winUnpressed = _keyTracker.Unpressed.Where(x => x == Key.LWin || x == Key.RWin)
            //    .Select(x => ModifierKeys.Windows).StartWith(ModifierKeys.None);

            //var modPressed = Observable.CombineLatest(altPressed, ctrlPressed, shiftPressed, winPressed,
            //    (alt, ctrl, shift, win) => alt | ctrl | shift | win);
            ////var modUnpressed = Observable.CombineLatest(altUnpressed, ctrlUnpressed, shiftUnpressed, winUnpressed,
            ////    (alt, ctrl, shift, win) => alt | ctrl | shift | win);

            //var modStream = modPressed;
            //modStream = modStream.Zip(altUnpressed, (m, a) => m & ~a);
            //modStream = modStream.Zip(ctrlUnpressed, (m, c) => m & ~c);
            //modStream = modStream.Zip(shiftUnpressed, (m, s) => m & ~s);
            //modStream = modStream.Zip(winUnpressed, (m, w) => m & ~w);

            //modPressed.Subscribe(x => Debug.WriteLine($"Modifiers pressed: {x}"));
            //modStream.Subscribe(x => Debug.WriteLine($"Current modifiers: {x}"));
            ////modUnpressed.Subscribe(x => Debug.WriteLine($"Modifiers Unpressed: {x}"));
        }

        public Hotkey SetModifiers(ModifierKeys modifiers)
        {
            Modifiers = modifiers;
            return this;
        }

        public Hotkey AddKey(Key key)
        {
            Keys.Add(key);
            return this;
        }
    }

    public interface IHotkey
    {
        ModifierKeys Modifiers { get; set; }
        IList<Key> Keys { get; set; }
        IObservable<HotkeyPressedNotification> Pressed { get; }
    }
}
