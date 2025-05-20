using System;
using Core;
using UniRx;
using Zenject;

namespace UI
{
    public class UIResourcesWindowPresenter : IInitializable, IDisposable
    {
        private readonly IResourceStorageService _resourceStorageService;
        private readonly UIResourcesWindowView _resourcesWindowView;
        private readonly CompositeDisposable _disposable = new();

        public UIResourcesWindowPresenter(
            IResourceStorageService resourceStorageService,
            UIResourcesWindowView resourcesWindowView
        )
        {
            _resourceStorageService = resourceStorageService;
            _resourcesWindowView = resourcesWindowView;
        }
        
        public void Initialize()
        {
            _resourceStorageService
                .ResourceCount
                .Subscribe(OnChangeResourceAmount)
                .AddTo(_disposable);
        }

        private void OnChangeResourceAmount(float amount) =>
            _resourcesWindowView.UpdateResourceAmount(amount);

        public void Dispose() =>
            _disposable?.Dispose();
    }
}