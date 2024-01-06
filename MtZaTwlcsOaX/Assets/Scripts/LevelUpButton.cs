namespace Namespace
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class LevelUpButton : MonoBehaviour
    {
        private Player _player;
        private Button _button;
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
            _player = FindObjectOfType<Player>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_player.LevelUp);
        }

        private void Update()
        {
            _button.interactable = _player.Gold >= _player.LevelUpCost;
            _tmp.text = _player.LevelUpCost.ToString("N0");
        }
    }
}