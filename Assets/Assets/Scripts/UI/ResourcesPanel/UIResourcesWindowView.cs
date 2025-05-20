using TMPro;
using UnityEngine;

namespace UI
{
    public class UIResourcesWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;

        public void UpdateResourceAmount(float amount) =>
            _amountText.text = $"Resources amount: {amount}";
    }
}