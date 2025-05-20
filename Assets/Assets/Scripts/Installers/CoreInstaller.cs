using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Systems;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.Factories.Impl;
using Core;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private SceneHandler _sceneHandler;
        
        public override void InstallBindings()
        {
            BindHelpers();
            BindFactories();
            BindServices();
            BindSystems();
        }

        private void BindHelpers()
        {
            Container.Bind<SceneHandler>().FromInstance(_sceneHandler).AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IGraphObjectFactory>().To<GraphObjectFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<IGraphService>().To<GraphService>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<GraphInitializer>().AsSingle().NonLazy();
        }
    }
}