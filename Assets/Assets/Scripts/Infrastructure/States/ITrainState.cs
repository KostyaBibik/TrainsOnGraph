namespace Infrastructure
{
    public interface ITrainState
    {
        void Enter(ITrainContext context);
        void Exit();
        void Tick();
    }
}