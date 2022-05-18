using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Winium.Cruciatus;
using Winium.Cruciatus.Settings;

namespace Winium.Desktop.Driver.Input
{
    internal class WiniumKeyboard
    {
        #region Fields

        private readonly KeyboardModifiers modifiers = new();
        private readonly KeyboardModifiers pressedKeys = new();

        #endregion

        #region Constructors and Destructors

        public WiniumKeyboard(KeyboardSimulatorType keyboardSimulatorType)
        {
            CruciatusFactory.Settings.KeyboardSimulatorType = keyboardSimulatorType;
        }

        #endregion

        #region Public Methods and Operators

        public void KeyDown(string keyToPress, bool isModifier)
        {
            var key = KeyboardModifiers.GetVirtualKeyCode(keyToPress);
            this.pressedKeys.Add(keyToPress);
            if (isModifier)
            {
                this.modifiers.Add(keyToPress);
            }
            CruciatusFactory.Keyboard.KeyDown(key);
        }

        public void KeyUp(string keyToRelease, bool isModifier)
        {
            var key = KeyboardModifiers.GetVirtualKeyCode(keyToRelease);
            if (isModifier)
            {
                this.modifiers.Remove(keyToRelease);
            }
            this.pressedKeys.Remove(keyToRelease);
            CruciatusFactory.Keyboard.KeyUp(key);
        }

        public void KeyPress(string keyToPress)
        {
            var key = KeyboardModifiers.GetVirtualKeyCode(keyToPress);
            CruciatusFactory.Keyboard.KeyDown(key);
            CruciatusFactory.Keyboard.KeyUp(key);
        }

        public void SendKeys(char[] keysToSend)
        {
            var builder = keysToSend.Select(key => new KeyEvent(key)).ToList();

            this.SendKeys(builder);
        }

        #endregion

        #region Methods

        protected void ReleaseModifiers()
        {
            this.modifiers.Clear();
            var tmp = this.pressedKeys.ToList();

            foreach (var key in tmp)
            {
                this.KeyUp(key, false);
            }
        }

        private void PressOrReleaseModifier(string modifier)
        {
            if (this.modifiers.Contains(modifier))
            {
                this.KeyUp(modifier, true);
            }
            else
            {
                this.KeyDown(modifier, true);
            }
        }

        private void SendKeys(IEnumerable<KeyEvent> events)
        {
            foreach (var keyEvent in events)
            {
                if (keyEvent.IsNewLine())
                {
                    CruciatusFactory.Keyboard.SendEnter();
                }
                else if (keyEvent.IsModifierRelease())
                {
                    this.ReleaseModifiers();
                }
                else if (keyEvent.IsModifier())
                {
                    this.PressOrReleaseModifier(keyEvent.GetKey());
                }
                else if (keyEvent.HasMapping())
                {
                    if (this.modifiers.Count > 0)
                    {
                        this.KeyDown(keyEvent.GetKey(), false);
                    }
                    else
                    {
                        this.KeyPress(keyEvent.GetKey());
                    }
                }
                else
                {
                    this.Type(keyEvent.GetCharacter());
                }
            }
        }

        private void Type(char key)
        {
            // First try.
            if (this.modifiers.Count > 0 && KeyboardModifiers.TryGetVirtualKeyCode(key, out var virtualKey))
            {
                CruciatusFactory.Keyboard.KeyDown(virtualKey);
                CruciatusFactory.Keyboard.KeyUp(virtualKey);
                return;
            }

            string str = Convert.ToString(key);

            if (this.modifiers.Contains(Keys.LeftShift) || this.modifiers.Contains(Keys.Shift))
            {
                str = str.ToUpper();
            }

            CruciatusFactory.Keyboard.SendText(str);
        }

        #endregion
    }
}
