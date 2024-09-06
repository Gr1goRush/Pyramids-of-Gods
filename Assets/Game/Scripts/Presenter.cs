using Game;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField] private int _maxLevel;
    [SerializeField] private BrickSpawner _brickSpawner;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private Game.Platform _platform;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private HeightPresenter _heightPresenter;
    [SerializeField] private CameraFollower _cameraFollower;
    private int _moveCameraBricksCount = 5;

    private Health _health;
    private int _level;
    private int _height;
    private int _coins;
    private bool _vibro;

    private void Awake()
    {
        Init();
        _heightPresenter.Init(_height);
        _losePanel.Close();
        _winPanel.Close();
    }

    public void Init()
    {
        _vibro = Saver.GetBool("Vibro", true);
        _level = Saver.GetInt("Level", 1);
        _height = 2 + _level;
        _health = new Health(3);
        _healthView.Init(_health);
        _brickSpawner.Init(_health, TrySpawn, InstallBrick, UseBonus);
        _bonusSpawner.Init();
        _cameraFollower.Init(_platform.transform);
    }
    private void InstallBrick(Item brick)
    {
        _coins += 1;
        if (_vibro) Handheld.Vibrate();
        _platform.AddBrick(brick);
    }
    private void UseBonus(IBonus bonus)
    {
        bonus.SetBonus(this);
    }
    public void Heal(int health)
    {
        _health.Heal(health);
    }
    private void TrySpawn()
    {
        _heightPresenter.UpdateHeight(_platform.Count + 1);
        if(_platform.Count > _moveCameraBricksCount)
        {
            _cameraFollower.SetTarget(_platform.GetItemByIndex(_platform.Count - 2).transform);
        }
        if(_platform.Count >= _height)
        {
            int next = _level + 1;
            if(next >= _maxLevel)
            {
                next = 1;
            }
            _winPanel.Open(_level, next, _height, _coins);
            Saver.SaveBool(true, $"Unloked{next}");
            int stars = Saver.GetInt( $"Stars{_level}");
            if(stars < _health.HP)
            {
                Saver.SaveInt(_health.HP, $"Stars{_level}");
            }

            GlobalWallet.AddCoins(_coins, "Coins");

        }
        else if(_health.HP <= 0)
        {
            GlobalWallet.AddCoins(_coins, "Coins");
            _losePanel.Open(_height, _platform.Count, _level, _coins);
        }
        else
        {
            if(_platform.Count == _height - 1)
            {
                _brickSpawner.SpawnRoof();
            }
            else
            {
                _brickSpawner.Spawn();
            }

        }
    }
}
