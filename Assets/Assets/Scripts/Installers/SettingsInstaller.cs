using DataBase;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Assets.Scripts.Installers
{
    [CreateAssetMenu(fileName = nameof(SettingsInstaller), menuName = "Installers/" + nameof(SettingsInstaller))]
    public class SettingsInstaller : ScriptableObjectInstaller
    {
        [FormerlySerializedAs("_graphViewSettings")] [SerializeField] private GameSettings gameSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();
        }
    }
}