using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = nameof(TrainSettings), menuName = "Settings/" + nameof(TrainSettings))]
    public class TrainSettings : ScriptableObject
    {
        [SerializeField] private TrainSO[] _trains;

        public TrainSO[] Trains => _trains;
    }
}