using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lable;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Button _startLevelBtn;

    private int _level;
    private bool _unlocked;
    private int _starsOpened;
    private Action<int> _onClick;

    public void Init(int level, Action<int> loadLevel)
    {
        _level = level;

        _lable.text = level.ToString();
        _unlocked = Saver.GetBool($"Unloked{_level}");
        _starsOpened = Saver.GetInt($"Stars{_level}");
        _startLevelBtn.onClick.AddListener(LoadLevel);
        if(!_unlocked && level == 1)
        {
            _unlocked = true;
            Saver.SaveBool(true,$"Unloked{_level}");
        }
        if (_unlocked)
        {
            _onClick = loadLevel;
        }
        for (int i = 0; i < _stars.Length; i++)
        {
            if(i >= _starsOpened)
            {
                _stars[i].gameObject.SetActive(false);
            }
            else
            {
                _stars[i].gameObject.SetActive(true);
            }
        }
    }
    private void LoadLevel()
    {
        _onClick?.Invoke(_level);
    }
}