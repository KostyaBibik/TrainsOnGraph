using UnityEngine;

namespace Core
{
    public class TrainView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Initialize(TrainModel model)
        {
            name = $"Train_{model.Id}";
        }
    }
}