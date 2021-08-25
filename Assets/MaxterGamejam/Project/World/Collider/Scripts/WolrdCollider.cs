using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.World
{
    public class WolrdCollider : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(collision.gameObject);
        }
    }
}