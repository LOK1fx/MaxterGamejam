using UnityEngine;
using System.Collections;

namespace com.LOK1game.recode
{
    public interface IHitbox : IDamagable
    {
        Hitbox.Part GetBodyPart();
    }
}