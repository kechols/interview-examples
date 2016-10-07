namespace Kevins.HSoftware.UsingSealed
{
    public abstract class AbstractSealed : ISealed
    {
        private string value = null;

        public abstract bool Sealed { get; }

        public string Value { get { return value; } }

        protected AbstractSealed()
        {
            this.value = value;
        }

        protected AbstractSealed(string value)
        {
            this.value = value;
        }
    }
}
