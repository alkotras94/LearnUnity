using UnityEngine;

namespace ObjectPool
{
    public class Bullet : MonoBehaviour, IPoolObject
    {
        private float _speed = 20f;
        private Rigidbody2D _rb;
        private ObjectPool<Bullet> _pool;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Init()
        {
            _rb.velocity = Vector3.zero;
        }
        
        public void Shoot(Vector2 direction) => _rb.AddForce(direction.normalized * _speed, ForceMode2D.Impulse);

        public void DeInit()
        {
            // можно сбросить эффекты, отключить партиклы и т.п.
        }

        public void SetPool(ObjectPool<Bullet> pool, Transform parent)
        {
            _pool = pool;
            transform.position = parent.position;
        }

        private void OnCollisionEnter2D(Collision2D other) => _pool.ReturnToPool(this);
    }
}