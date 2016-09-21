namespace Kevins.Examples.Common.Utils.Patterns
{
    public interface IRetryable
    {
        bool Attempt();

        void Recover();
    }
}
