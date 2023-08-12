using UnityEngine;

namespace Sources.Modules.Enemy
{
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private int _maxHp;

        private int _currentHp;

        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
            TryDie();
        }

        private void TryDie()
        {
            if (_currentHp == 0)
            {
                
            }
        }
    }
}
