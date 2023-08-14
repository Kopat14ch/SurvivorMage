using System;
using UnityEngine;

namespace Sources.Modules.Player.MVP
{
    public class PlayerView : MonoBehaviour
    {
        public event Action ButtonAddMaxHealthPressed;
        public event Action ButtonAddDamageScalerPressed;
        public event Action ButtonAddSpeedPressed;

        public void ChangeMaxHealthText(float maxHealth)
        {
            
        }
        
        public void ChangeDamageScalerText(float damageScaler)
        {
            
        }
        
        public void ChangeSpeedText(float speed)
        {
            
        }
    }
}