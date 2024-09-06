using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private float _offset;

    public void Init(Transform target)
    {
        _target = target;
        _offset = transform.position.y - _target.position.y;
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    public void Update()
    {
        Vector3 position = new Vector3(transform.position.x,_target.position.y + _offset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
    }
}
