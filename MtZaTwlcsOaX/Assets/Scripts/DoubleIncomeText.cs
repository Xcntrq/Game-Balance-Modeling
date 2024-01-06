namespace Namespace
{
    using TMPro;
    using UnityEngine;

    public class DoubleIncomeText : MonoBehaviour
    {
        private Player _player;
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _tmp.text = _player.IsIncomeDoubled ? "ALREADY PURCHASED" : "ONCE PER GAME";
        }
    }
}