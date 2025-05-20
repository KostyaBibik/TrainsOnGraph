using DataBase;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    [CreateAssetMenu(fileName = nameof(SettingsInstaller), menuName = "Installers/" + nameof(SettingsInstaller))]
    public class SettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private TrainSettings _trainSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameSettings).AsSingle();
            Container.BindInstance(_trainSettings).AsSingle();
        }
    }
}