using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.Player
{
    public interface IPlayerAbility
    {
        void Use(object sender);
        void TurnOffAbility();
        void TurnOnAbility();
        T GetAbility<T>() where T : PlayerAbilityBase;
    }
}