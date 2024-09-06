using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _heartPrefab;
    [SerializeField] private Transform _heartParent;
    [SerializeField] private Sprite _enableHeart;
    [SerializeField] private Sprite _disableHeart;

    private int _maxHealth;
    private Image[] _hearts;
    private Health _trackedHealth;
    public void Init(Health health)
    {
        _trackedHealth = health;
        _trackedHealth.OnHealthChanged += ShowHearts;
        _maxHealth = _trackedHealth._maxHealth;
        _hearts = new Image[_maxHealth];

        for (int i = 0; i < _maxHealth; i++)
        {
            Image heart = Instantiate(_heartPrefab, _heartParent);
            _hearts[i] = heart;
        }
        ShowHearts(_maxHealth);

    }
    public void ShowHearts(int health)
    {
        for (int i = 0; i < health; i++)
        {
            _hearts[i].sprite = _enableHeart;
        }
        for (int i = health; i < _maxHealth; i++)
        {
            _hearts[i].sprite = _disableHeart;
        }
    }
}