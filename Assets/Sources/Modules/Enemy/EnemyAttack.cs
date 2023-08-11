using Sources.Modules.Player;
using UnityEngine;

namespace Sources.Modules.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _cooldown;

        private float _passedTime;
        
        private void Update()
        {
            CountDown();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Mage mage) && _passedTime >= _cooldown)
            {
                _passedTime = 0;
                Attack(mage);
            }
        }

        private void CountDown()
        {
            if (_passedTime < _cooldown)
                _passedTime += Time.deltaTime;
        }
        
        private void Attack(Mage mage)
        {
            Debug.Log("Attack!");
        }
    }
}
