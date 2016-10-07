namespace Kevins.HSoftware.UsingSealed
{
    public sealed class ClazzPreventsPartiallyInitializedObjectUsingSealed : AbstractSealed
    {
        public override bool Sealed => true;

        public ClazzPreventsPartiallyInitializedObjectUsingSealed(string value) : base(value) { }
    }
}
