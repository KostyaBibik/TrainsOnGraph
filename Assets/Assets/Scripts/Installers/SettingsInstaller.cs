using DataBase;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    [CreateAssetMenu(fileName = nameof(SettingsInstaller), menuName = "Installers/" + nameof(SettingsInstaller))]
    public class SettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GraphViewSettings _graphViewSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_graphViewSettings).AsSingle();
        }
    }
}