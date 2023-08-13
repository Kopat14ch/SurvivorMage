﻿using System;
using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.Player;
using UnityEngine;

namespace Sources.Modules.Finder
{
    public class FinderCloseEnemy : MonoBehaviour
    {
        private List<EnemyUnit> _enemyUnits;
        private Mage _mage;
        private Vector3 _closePosition;
        private float _currentDistance;
        private float _tempDistance;

        public void Init(Mage mage)
        {
            _enemyUnits = new List<EnemyUnit>();
            _mage = mage;
        }
        
        public void SetEnemyList(List<EnemyUnit> enemyUnits) => _enemyUnits = enemyUnits;

        public Vector3 GetCloseEnemyPosition()
        {
            if (_enemyUnits.Count < 0)
                throw new Exception();

            foreach (var enemyUnit in _enemyUnits)
            {
                if (enemyUnit.gameObject.activeSelf)
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