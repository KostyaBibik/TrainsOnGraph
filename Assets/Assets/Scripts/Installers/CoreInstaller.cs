using Core;
using Infrastructure;
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
            Container.Bind<ITrainFactory>().To<TrainFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<IGraphService>().To<GraphService>().AsSingle();
            Container.BindInterfacesTo<TrainAgentService>().AsSingle();
            Container.BindInterfacesTo<PathfindingService>().AsSingle();
            Container.BindInterfacesTo<ResourceStorageService>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<GraphInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<TrainInitializer>().AsSingle();
        }
    }
}