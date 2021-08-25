using UnityEngine;
using com.LOK1game.recode;

namespace com.LOK1game.MaxterGamejam
{
    public class GamejamGameOptions : GameOptions
    {
        public bool EnableBunnyhopping { get; private set; }

        public void SetBunnyhopping(bool active, object sender)
        {
            Debug.Log("Bunny");

            if(!(GameControllerBase)sender) { return; }

            EnableBunnyhopping = active;

            Debug.Log("Bunnny Active");
        }
    }
}
