namespace Namespace
{
    using TMPro;
    using UnityEngine;

    public class TimeText : MonoBehaviour
    {
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            int min = (int)Time.timeSinceLevelLoad / 60;
            int sec = (int)Time.timeSinceLevelLoad - min * 60;
            _tmp.text = $"{min:D2}:{sec:D2}";
        }
    }
}