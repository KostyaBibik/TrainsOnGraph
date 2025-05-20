using Infrastructure;

namespace Core
{
    public interface ITrainAgentService
    {
        void Register(ITrainAgent agent);
        void Unregister(ITrainAgent agent);
    }
}