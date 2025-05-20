using UI;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindResourceWindow();
        }

        private void BindResourceWindow()
        {
            Container.BindInterfacesAndSelfTo<UIResourcesWindowView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UIResourcesWindowPresenter>().AsSingle();
        }
    }
}