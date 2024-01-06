namespace Namespace
{
    using TMPro;
    using UnityEngine;

    public class NextLevelIncomeText : MonoBehaviour
    {
        private Player _player;
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
            _player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            _tmp.text = $"INCOME x{_player.IncomePerSecFactor:f1} => {(long)(_player.Income * _player.IncomePerSecFactor):n0}";
        }
    }
}