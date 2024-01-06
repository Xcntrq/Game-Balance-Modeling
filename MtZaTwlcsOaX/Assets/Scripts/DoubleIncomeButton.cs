namespace Namespace
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class DoubleIncomeButton : MonoBehaviour
    {
        private Player _player;
        private Button _button;
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
            _player = FindObjectOfType<Player>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_player.DoubleIncome);
        }

        private void Update()
        {
            _button.interactable = (!_player.IsIncomeDoubled) && (_player.Gold >= _player.DoubleIncomeCost);
            _tmp.text = _player.DoubleIncomeCost.ToString("N0");
        }
    }
}