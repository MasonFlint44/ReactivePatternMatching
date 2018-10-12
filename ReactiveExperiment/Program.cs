using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReactiveExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            var keySimulator = new KeySimulator();
            var keyTracker = new SimulatedKeyTracker(keySimulator.KeyStream);
            var hotkey = new Hotkey(keyTracker);

            keySimulator.Down(Key.A)
                .Up(Key.A)
                .Down(Key.B)
                .Up(Key.B)
                .Down(Key.RightCtrl)
                .Down(Key.LeftAlt)
                .Down(Key.U)
                .Down(Key.P)
                .Up(Key.RightCtrl)
                .Up(Key.LeftAlt);
        }
    }
}
