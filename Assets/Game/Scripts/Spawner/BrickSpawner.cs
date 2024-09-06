using Game;
using UnityEngine;
using ActionItem = System.Action<Game.Item>;
using UseBonus = System.Action<IBonus>;
using Action = System.Action;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private Item _roof;
    [SerializeField] private Vector2 _spawnRange;
    [SerializeField] private float _offset;
    [SerializeField] private Platform _platform;
    [SerializeField] private SkeensData _brickSkeensData;
    [SerializeField] private SkeensData _roofSkeensData;

    private Camera _camera;
    private Health _health;
    private ActionItem _onInstalled;
    private Action _onReleased;
    private UseBonus _onBonusUsing;

    public void Init(Health health, Action onReleased, ActionItem onInstalled, UseBonus onBonusUsing)
    {
        _camera = Camera.main;
        _health = health;
        _onInstalled = onInstalled;
        _onReleased = onReleased;
        _onBonusUsing = onBonusUsing; 
    }
    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        float x = Random.Range(_spawnRange.x, _spawnRange.y);
        Vector3 position =  new Vector3(x, _camera.transform.position.y + _offset, 0);
        Item item = Instantiate(_item, position, Quaternion.identity);
        item.transform.position = position;
        string skeenName = Saver.GetString("Brick", "0");
        item.Init(_health, _onReleased, _onInstalled);
        item.SetSkeen(_brickSkeensData.GetSkeen(skeenName));
    }
    public void SpawnRoof()
    {
        float x = Random.Range(_spawnRange.x, _spawnRange.y);
        Vector3 position = new Vector3(x, _camera.transform.position.y + _offset, 0);
        Item item = Instantiate(_roof, position, Quaternion.identity);
        item.transform.position = position;
        string skeenName = Saver.GetString("Brick", "0");
        item.Init(_health, _onReleased, _onInstalled);
        item.SetSkeen(_roofSkeensData.GetSkeen(skeenName));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = new Vector3(transform.position.x +_spawnRange.x, transform.position.y + _offset, 0);
        Vector3 to = new Vector3(transform.position.x +_spawnRange.y, transform.position.y + _offset, 0);
        Gizmos.DrawLine(from, to);
    }
}
