namespace Sunrise.Radiology.Messenger.Common.Utils.Strings
{
    public class DefaultsProvider : AbstractStringsProvider<DefaultsProvider>
    {
        public override IStringLoader GetStringLoader()
        {
            return new DefaultsLoader();
        }

        public override string GetIdentifier()
        {
            return "default";
        }
    }
}
