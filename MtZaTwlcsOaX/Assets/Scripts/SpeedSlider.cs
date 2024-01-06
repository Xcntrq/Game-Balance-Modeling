namespace Namespace
{
    using UnityEngine;
    using UnityEngine.UI;

    public class SpeedSlider : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 1;
            GetComponent<Slider>().value = 1;
            GetComponent<Slider>().onValueChanged.AddListener((float value) =>
            {
                Time.timeScale = value;
            });
        }
    }
}