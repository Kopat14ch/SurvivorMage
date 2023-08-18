using Sources.Modules.Player.Scripts;
using UnityEngine;

namespace Sources.Modules.Enemy
{
    internal class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;

        private const int AddDamage = 10;
        private float _passedTime;
        
        private void Update()
        {
            CountDown();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Mage mage) && _passedTime >= _cooldown)
            {
                _passedTime = 0;
                Attack(mage);
            }
        }

        public void AddLevel(int wave, int currentLevel)
        {
            while (currentLevel < wave)
            {
                _damage += AddDamage;
                Debug.Log(_damage);
                currentLevel++;
            }
        }

        private void CountDown()
        {
            if (_passedTime < _cooldown)
                _passedTime += Time.deltaTime;
        }
        
        private void Attack(Mage mage)
        {
            mage.TryTakeDamage(_damage);
        }
    }
}
