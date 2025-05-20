using Core;

namespace Infrastructure
{
    public interface ITrainFactory
    {
        public TrainView Create(GraphNodeModel spawnNode, float speedMoving, float timeMining);
    }
}