namespace Namespace
{
    using TMPro;
    using UnityEngine;

    public class GoldText : MonoBehaviour
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
            _tmp.text = $"GOLD: {_player.Gold:N0}";
        }
    }
}