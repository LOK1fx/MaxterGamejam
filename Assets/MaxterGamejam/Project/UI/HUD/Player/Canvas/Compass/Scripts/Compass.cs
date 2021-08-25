using UnityEngine.UI;
using UnityEngine;
using com.LOK1game.recode.Player;

namespace com.LOK1game.recode.UI
{
    public class Compass : MonoBehaviour
    {
        public RawImage CompassImage;

        private void Update()
        {
            if (Player.Player.LocalPlayerInstance == null) { return; }

            CompassImage.uvRect = new Rect(Player.MoveCamera.Instance.transform.localEulerAngles.y / 360, 0, 1, 1);
        }
    }
}