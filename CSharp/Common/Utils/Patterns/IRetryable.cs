namespace Sunrise.Radiology.Messenger.Common.Utils.Patterns
{
    public interface IRetryable
    {
        bool Attempt();

        void Recover();
    }
}
