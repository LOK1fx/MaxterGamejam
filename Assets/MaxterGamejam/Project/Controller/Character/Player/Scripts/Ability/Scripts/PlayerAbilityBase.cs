using UnityEngine;

namespace com.LOK1game.recode.Player
{
    public abstract class PlayerAbilityBase : MonoBehaviour
    {
        public virtual void Use(object sender)
        {
            
        }

        public virtual void TurnOffAbility()
        {
            enabled = false;
        }

        public virtual void TurnOnAbility()
        {
            enabled = true;
        }

        public T GetAbility<T>() where T : PlayerAbilityBase
        {
            return (T)this;
        }
    }
}