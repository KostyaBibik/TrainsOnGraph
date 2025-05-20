using UniRx;

namespace Core
{
    public class ResourceStorageService : IResourceStorageService
    {
        private readonly ReactiveProperty<float> _resourceCount = new(0);

        public IReadOnlyReactiveProperty<float> ResourceCount => _resourceCount;

        public void AddResource(float amount)
        {
            _resourceCount.Value += amount;
        }
    }
}