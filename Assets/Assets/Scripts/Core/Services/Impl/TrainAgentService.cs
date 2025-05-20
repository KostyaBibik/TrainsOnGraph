using System.Collections.Generic;
using Infrastructure;
using Zenject;

namespace Core
{
    public class TrainAgentService : ITrainAgentService, ITickable
    {
        private readonly List<ITrainAgent> _agents = new();

        public void Register(ITrainAgent agent)
        {
            if (!_agents.Contains(agent))
            {
                _agents.Add(agent);
            }
        }

        public void Unregister(ITrainAgent agent) 
        {
            if(_agents.Contains(agent))
            {
                _agents.Remove(agent);
            }
        }
        
        public void Tick()
        {
            foreach (var agent in _agents)
            {
                agent.Tick();
            }
        }
    }
}