using UniRx;

namespace Core
{
    public interface IResourceStorageService
    {
        public IReadOnlyReactiveProperty<float> ResourceCount { get; }
 
        public void AddResource(float amount);
    }
}