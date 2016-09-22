namespace Kevins.Examples.Common.Utils.Strings
{
    public class MessagesProvider : AbstractStringsProvider<MessagesProvider>
    {
        public override IStringLoader GetStringLoader()
        {
            return new MessagesLoader();
        }

        public override string GetIdentifier()
        {
            return "message";
        }
    }
}
