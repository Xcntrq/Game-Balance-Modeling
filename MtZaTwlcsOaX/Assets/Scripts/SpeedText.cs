namespace Namespace
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class SpeedText : MonoBehaviour
    {
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            FindObjectOfType<Slider>().onValueChanged.AddListener((float value) =>
            {
                _tmp.text = $"GAME SPEED: X{value:f0}";
            });
        }
    }
}