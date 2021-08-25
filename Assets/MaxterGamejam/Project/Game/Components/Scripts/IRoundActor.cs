using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode
{
    public interface IRoundActor
    {
        void OnRoundStart();
        void OnRoundEnd();
        void OnFreezeTimeEnd();
    }
}