using Game;
using UnityEngine;
using UseBonus = System.Action<IBonus>;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnRange;
    [SerializeField] private Vector2 _spawnTimeRange;
    [SerializeField] private float _offset;
    [SerializeField] private Healer _bonus;

    private Camera _camera;

    public void Init()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        float time = Random.Range(_spawnTimeRange.x, _spawnTimeRange.y);
        Timer.Start(time).OnComplete(Spawn);
    }
    public void Spawn()
    {
        float x = Random.Range(_spawnRange.x, _spawnRange.y);
        Vector3 position = new Vector3(x, _camera.transform.position.y + _offset, 0);
        Healer item = Instantiate(_bonus, position, Quaternion.identity);
        item.transform.position = position;
        float time = Random.Range(_spawnTimeRange.x, _spawnTimeRange.y);
        Timer.Start(time).OnComplete(Spawn);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = new Vector3(transform.position.x + _spawnRange.x, transform.position.y + _offset, 0);
        Vector3 to = new Vector3(transform.position.x + _spawnRange.y, transform.position.y + _offset, 0);
        Gizmos.DrawLine(from, to);
    }
}
