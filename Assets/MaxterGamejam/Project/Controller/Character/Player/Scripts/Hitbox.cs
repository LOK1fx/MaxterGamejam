using UnityEngine;
using System.Collections;
using System;

namespace com.LOK1game.recode
{
    public class Hitbox : MonoBehaviour, IHitbox
    {
        public event Action<object, int, object[]> OnHit;

        public enum Part
        {
            Head,
            Neck,
            UpperBody,
            LowerBody,
            Arms,
            Legs
        }

        public Part part;

        public void TakePointDamage(object sender, int damage, Vector3 dir, object[] info = null)
        {
            info = new object[2]
            {
                dir,
                part
            };

            OnHit?.Invoke(sender, GetDamage(part, damage), info);
        }

        public void TakeRadialDamage(object sender, int damage, object[] info = null)
        {
            OnHit?.Invoke(sender, damage, info);
        }

        private int GetDamage(Part part, int damage)
        {
            switch (part)
            {
                case Part.Head:
                    return GetRoundedDamage(damage * 100f);
                case Part.Neck:
                    return GetRoundedDamage(damage * 1.5f);
                case Part.UpperBody:
                    return GetRoundedDamage(damage * 1f);
                case Part.LowerBody:
                    return GetRoundedDamage(damage * 1f);
                case Part.Arms:
                    return GetRoundedDamage(damage * 1.2f);
                case Part.Legs:
                    return GetRoundedDamage(damage * 1f);
                default:
                    return damage;
            }
        }

        private int GetRoundedDamage(float damage)
        {
            return Mathf.RoundToInt(damage);
        }

        public Part GetBodyPart()
        {
            return part;
        }
    }
}