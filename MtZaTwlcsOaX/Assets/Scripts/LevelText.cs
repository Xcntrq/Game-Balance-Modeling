namespace Namespace
{
    using TMPro;
    using UnityEngine;

    public class LevelText : MonoBehaviour
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
            _tmp.text = $"LEVEL: {_player.Level}";
        }
    }
}