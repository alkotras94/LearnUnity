using UnityEngine;

namespace ObjectPool
{
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int startCount = 10;

        private ObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>();
            _bulletPool.Init(bulletPrefab, startCount, transform);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Bullet bullet = _bulletPool.Get();
                //bullet.transform.position = transform.position;
                //bullet.transform.forward = transform.forward;
                bullet.Shoot(transform.right);
                bullet.SetPool(_bulletPool,transform);
            }
        }
    }
}