namespace Namespace
{
    using TMPro;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject _winCanvas;
        [SerializeField] private bool _isIncomeDoubled;
        [SerializeField] private float _time;
        [SerializeField] private long _level;
        [SerializeField] private long _levelUpCost;
        [SerializeField] private long _incomePerSec;
        [SerializeField] private long _doubleIncomeCost;
        [SerializeField] private float _levelUpCostFactor;
        [SerializeField] private float _incomePerSecFactor;
        [SerializeField] private long _gold;

        public bool IsIncomeDoubled => _isIncomeDoubled;
        public long Level => _level;
        public long LevelUpCost => _levelUpCost;
        public long Income => _incomePerSec;
        public long DoubleIncomeCost => _doubleIncomeCost;
        public float IncomePerSecFactor => _incomePerSecFactor;
        public long Gold => _gold;

        public void LevelUp()
        {
            if (_gold >= _levelUpCost)
            {
                _level++;
                _gold -= _levelUpCost;
                _levelUpCost = (long)(_levelUpCost * _levelUpCostFactor);
                _incomePerSec = (long)(_incomePerSec * _incomePerSecFactor);
            }

            if (_level >= 10)
            {
                Time.timeScale = 0;
                int min = (int)Time.timeSinceLevelLoad / 60;
                int sec = (int)Time.timeSinceLevelLoad - min * 60;
                _winCanvas.GetComponentInChildren<TextMeshProUGUI>().text = $"YOU WON IN<br>{min:D2}:{sec:D2}";
                _winCanvas.SetActive(true);
            }
        }

        public void DoubleIncome()
        {
            if (_gold >= _doubleIncomeCost)
            {
                _isIncomeDoubled = true;
                _gold -= _doubleIncomeCost;
                _incomePerSec *= 2;
            }
        }

        private void Update()
        {
            _time += Time.deltaTime;

            while (_time > 1)
            {
                _time -= 1;
                _gold += _incomePerSec;
            }
        }
    }
}