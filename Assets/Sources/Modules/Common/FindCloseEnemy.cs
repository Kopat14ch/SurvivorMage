using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.Player;
using UnityEngine;

namespace Sources.Modules.Common
{
    public class FindCloseEnemy : MonoBehaviour
    {
        private IEnumerable<EnemyUnit> _enemyUnits;
        private Mage _mage;
        private Vector3 _closePosition;
        private float _currentDistance;
        private float _tempDistance;

        public void Init(IEnumerable<EnemyUnit> enemyUnits, Mage mage)
        {
            _enemyUnits = enemyUnits;
            _mage = mage;
        }

        public Vector3 GetCloseEnemyPosition()
        {
            _currentDistance = 0;
            _tempDistance = 0;
            
            foreach (var enemyUnit in _enemyUnits)
            {
                _tempDistance = Vector3.Distance(enemyUnit.transform.position, _mage.transform.position);
                
                if (_currentDistance == 0)
                {
                    SetCloseEnemy(enemyUnit);
                }
                else if (_tempDistance < _currentDistance)
                {
                    SetCloseEnemy(enemyUnit);
                }
            }
            
            return _closePosition;
        }

        private void SetCloseEnemy(EnemyUnit enemyUnit)
        {
            _currentDistance = _tempDistance;
            _closePosition = enemyUnit.transform.position;
        }
    }
}