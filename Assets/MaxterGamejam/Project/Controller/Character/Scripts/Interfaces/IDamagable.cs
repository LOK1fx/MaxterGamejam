using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode
{
    public interface IDamagable
    {
        void TakePointDamage(object sender, int damage, Vector3 dir, object[] info = null);
        void TakeRadialDamage(object sender, int damage, object[] info = null);
    }

}