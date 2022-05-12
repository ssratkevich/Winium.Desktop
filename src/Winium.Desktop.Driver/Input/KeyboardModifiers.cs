using System.Collections.Generic;
using OpenQA.Selenium;
using WindowsInput;

namespace Winium.Desktop.Driver.Input
{
    internal class KeyboardModifiers : List<string>
    {
        #region Static Fields

        private static readonly HashSet<string> Modifiers = 
            new HashSet<string>
            {
                Keys.Control,
                Keys.LeftControl,
                Keys.Shift,
                Keys.LeftShift,
                Keys.Alt,
                Keys.LeftAlt,
            };

        private static readonly Dictionary<string, VirtualKeyCode> KeysMap =
            new Dictionary<string, VirtualKeyCode>
            {
                { Keys.Null, VirtualKeyCode.None },
                { Keys.Cancel, VirtualKeyCode.CANCEL },
                { Keys.Help, VirtualKeyCode.HELP },
                { Keys.Backspace, VirtualKeyCode.BACK },
                { Keys.Tab, VirtualKeyCode.TAB },
                { Keys.Clear, VirtualKeyCode.CLEAR },
                { Keys.Return, VirtualKeyCode.RETURN },
                { Keys.Enter, VirtualKeyCode.RETURN },
                { Keys.Shift, VirtualKeyCode.SHIFT },
                //{ Keys.LeftShift, VirtualKeyCode.LSHIFT },
                { Keys.Control, VirtualKeyCode.CONTROL },
                //{ Keys.LeftControl, VirtualKeyCode.LCONTROL },
                { Keys.Alt, VirtualKeyCode.MENU },
                //{ Keys.LeftAlt, VirtualKeyCode.LMENU },
                { Keys.Pause, VirtualKeyCode.PAUSE },
                { Keys.Escape, VirtualKeyCode.ESCAPE },
                { Keys.Space, VirtualKeyCode.SPACE },
                { Keys.PageUp, VirtualKeyCode.PRIOR },
                { Keys.PageDown, VirtualKeyCode.NEXT },
                { Keys.End, VirtualKeyCode.END },
                { Keys.Home, VirtualKeyCode.HOME },
                { Keys.Left, VirtualKeyCode.LEFT },
                //{ Keys.ArrowLeft, VirtualKeyCode.LEFT },
                { Keys.Up, VirtualKeyCode.UP },
                //{ Keys.ArrowUp, VirtualKeyCode.UP },
                { Keys.Right, VirtualKeyCode.RIGHT },
                //{ Keys.ArrowRight, VirtualKeyCode.RIGHT },
                { Keys.Down, VirtualKeyCode.DOWN },
                //{ Keys.ArrowDown, VirtualKeyCode.DOWN },
                { Keys.Insert, VirtualKeyCode.INSERT },
                { Keys.Delete, VirtualKeyCode.DELETE },
                { Keys.NumberPad0, VirtualKeyCode.NUMPAD0 },
                { Keys.NumberPad1, VirtualKeyCode.NUMPAD1 },
                { Keys.NumberPad2, VirtualKeyCode.NUMPAD2 },
                { Keys.NumberPad3, VirtualKeyCode.NUMPAD3 },
                { Keys.NumberPad4, VirtualKeyCode.NUMPAD4 },
                { Keys.NumberPad5, VirtualKeyCode.NUMPAD5 },
                { Keys.NumberPad6, VirtualKeyCode.NUMPAD6 },
                { Keys.NumberPad7, VirtualKeyCode.NUMPAD7 },
                { Keys.NumberPad8, VirtualKeyCode.NUMPAD8 },
                { Keys.NumberPad9, VirtualKeyCode.NUMPAD9 },
                { Keys.Multiply, VirtualKeyCode.MULTIPLY },
                { Keys.Add, VirtualKeyCode.ADD },
                { Keys.Separator, VirtualKeyCode.SEPARATOR },
                { Keys.Subtract, VirtualKeyCode.SUBTRACT },
                { Keys.Decimal, VirtualKeyCode.DECIMAL },
                { Keys.Divide, VirtualKeyCode.DIVIDE },
                { Keys.F1, VirtualKeyCode.F1 },
                { Keys.F2, VirtualKeyCode.F2 },
                { Keys.F3, VirtualKeyCode.F3 },
                { Keys.F4, VirtualKeyCode.F4 },
                { Keys.F5, VirtualKeyCode.F5 },
                { Keys.F6, VirtualKeyCode.F6 },
                { Keys.F7, VirtualKeyCode.F7 },
                { Keys.F8, VirtualKeyCode.F8 },
                { Keys.F9, VirtualKeyCode.F9 },
                { Keys.F10, VirtualKeyCode.F10 },
                { Keys.F11, VirtualKeyCode.F11 },
                { Keys.F12, VirtualKeyCode.F12 },
                { Keys.Meta, VirtualKeyCode.MENU },
                //{ Keys.Command, VirtualKeyCode.LWIN },
            };

        #endregion

        #region Public Methods and Operators

        public static string GetKeyFromUnicode(char key) =>
            KeysMap.ContainsKey(key.ToString()) ? key.ToString() : null;

        public static VirtualKeyCode GetVirtualKeyCode(string key) =>
            KeysMap.TryGetValue(key, out var virtualKey) ? virtualKey : default(VirtualKeyCode);
        
        public static bool IsModifier(string key) =>
            Modifiers.Contains(key);

        public static bool HasMapping(char key) =>
            KeysMap.ContainsKey(key.ToString());

        #endregion
    }
}
