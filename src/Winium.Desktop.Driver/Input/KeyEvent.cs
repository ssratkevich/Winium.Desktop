using OpenQA.Selenium;

namespace Winium.Desktop.Driver.Input
{
    internal class KeyEvent
    {
        #region Fields

        private readonly char character;

        private readonly string unicodeKey;

        #endregion

        #region Constructors and Destructors

        public KeyEvent(char ch)
        {
            this.character = ch;
            this.unicodeKey = KeyboardModifiers.GetKeyFromUnicode(this.character);
        }

        #endregion

        #region Public Methods and Operators

        public char GetCharacter()
        {
            return this.character;
        }

        public string GetKey()
        {
            return this.unicodeKey;
        }

        public bool IsModifier()
        {
            return KeyboardModifiers.IsModifier(this.unicodeKey);
        }

        public bool IsModifierRelease()
        {
            return this.GetKey() == Keys.Null;
        }

        public bool HasMapping() =>
            KeyboardModifiers.HasMapping(this.character);

        public bool IsNewLine()
        {
            return this.GetCharacter() == '\n' || 
                this.GetKey() == Keys.Enter || 
                this.GetKey() == Keys.Return;
        }

        #endregion
    }
}
