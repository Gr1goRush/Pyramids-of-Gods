using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Platform : MonoBehaviour
    {
        
        [SerializeField] private Vector2 _moveRange;
        [SerializeField] private GameObject _collider;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _height;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _bonusMask;

        private PlayerInput _input;
        private List<Item> _items;
        private Camera _camera;
        private Presenter _presenter;
        public int Count => _items.Count;

        private void Awake()
        {
            _input = PlayerInput.Get();
            _items = new List<Item>();
            _camera = Camera.main;
            _presenter = FindObjectOfType<Presenter>();
        }
        public Item GetItemByIndex(int index)
        {
            return _items[index];
        }
        private void FixedUpdate()
        {
            Vector2 pointerPos = _input.GetInputAxis(AxisKey.PointerPosition);
            Vector2 nextPosition = _camera.ScreenToWorldPoint(pointerPos);
            nextPosition.x =  Mathf.Clamp(nextPosition.x, _moveRange.x, _moveRange.y);
            nextPosition.y = transform.position.y;
            Vector2 position = new Vector2(nextPosition.x,transform.position.y);
            _rb.MovePosition(position);

        }
        
        public void AddBrick(Item brick)
        {
            Vector3 position = brick.transform.position;
            if(_items.Count > 0)
            {
                position.y = _items[_items.Count - 1].GetStandHeight();
                _items[_items.Count - 1].DeleteCollider();
            }
            else
            {
                position.y = transform.position.y + _height;
                _collider.SetActive(false);
            }
            brick.transform.parent = transform;
            _items.Add(brick);
            brick.Install(position);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IBonus>(out IBonus bonus))
            {
                bonus.SetBonus(_presenter);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 from = new Vector3(transform.position.x + _moveRange.x, transform.position.y, 0);
            Vector3 to = new Vector3(transform.position.x + _moveRange.y, transform.position.y, 0);
            Gizmos.DrawLine(from, to);
        }
    }
}