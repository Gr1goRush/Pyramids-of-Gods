using System;
using UnityEngine;

namespace Game
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private float _height;
        [SerializeField] private GameObject _collider;
        [SerializeField] private Transform _model;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private LayerMask _platformMask;

        private Platform _platform;
        private Action _onReleased;
        private Action<Item> _onInstalled;
        private bool _enabled = true;
        private Health _health;

        public bool Enabled => _enabled;
        public void Init(Health health, Action onReleased = null, Action<Item> onInstalled = null)
        {
            _collider.SetActive(false);
            _onReleased = onReleased;
            _onInstalled = onInstalled;
            _health = health;
        }
        public void SetSkeen( Sprite skeen)
        {
            _renderer.sprite = skeen;
        }
        public float GetStandHeight()
        {
            float height = _height;
            return transform.position.y + height;
        }

        public void Install(Vector3 position)
        {
            _collider.SetActive(true);
            transform.rotation = Quaternion.identity;
            Destroy(_rb);
            transform.position = position;
            _enabled = false;
            _onReleased?.Invoke();
        }
        public void DeleteCollider()
        {
            _collider.SetActive(false);
        }
        public void DestroyItem()
        {
            _onReleased?.Invoke();
            Destroy(gameObject);
        }
        
        private void OnCollisionStay2D(Collision2D collision)
        {
            Vector3 scale = _model.lossyScale / 2 + _scale;
            Vector3 position = transform.position;
            position.x += scale.x / 2;
            position.y -= _model.lossyScale.y / 4;
            if ((_platformMask & (1 << collision.gameObject.layer)) == 0)
            {
                if (_enabled && Physics2D.OverlapBox(position, scale, 0, layerMask: _platformMask))
                {
                    position.x = transform.position.x - scale.x / 2;
                    if (Physics2D.OverlapBox(position, scale, 0, layerMask: _platformMask))
                    {
                        _onInstalled?.Invoke(this);
                        return;
                    }

                }
                _rb.freezeRotation = false;
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_enabled && collision.gameObject.tag == "Destroyer")
            {
                Destroy();
            }
        }
        protected virtual void Destroy()
        {
            _health.GetDamage(1);
            DestroyItem();
        }
        private void OnDrawGizmos()
        {
            Vector3 scale = _model.lossyScale/2 + _scale;
            Vector3 position = transform.position;
            position.x += scale.x / 2; 
            position.y -= _model.lossyScale.y / 4;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(position, scale);
            position.x = transform.position.x - scale.x/2;
            Gizmos.DrawWireCube(position, scale);
        }
    }
}