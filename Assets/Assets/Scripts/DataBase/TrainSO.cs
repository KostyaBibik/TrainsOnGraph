using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = nameof(TrainSO), menuName = "Settings/" + nameof(TrainSO))]
    public class TrainSO : ScriptableObject
    {
        [SerializeField] private float _speedMoving;
        [SerializeField] private float _timeMining;

        public float SpeedMoving => _speedMoving;
        public float TimeMining => _timeMining;
    }
}