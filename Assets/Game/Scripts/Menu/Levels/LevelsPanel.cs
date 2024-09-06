using UnityEngine;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] private Loading _loading;
    [SerializeField] private MenuLevelPresenter _levelPrefab;
    [SerializeField] private int _itemCount;
    [SerializeField] private Transform _context;
    
    private MenuLevelPresenter[] _levels;

    private void Awake()
    {
        _levels = new MenuLevelPresenter[_itemCount];
        for (int i = 0; i < _itemCount; i++)
        {
            MenuLevelPresenter level = Instantiate(_levelPrefab, _context);
            level.Init(i + 1, LoadLevel);
            _levels[i] = level;
        }
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    private void LoadLevel(int level)
    {
        Saver.SaveInt( level, "Level");
        _loading.LoadGame();
    }
}