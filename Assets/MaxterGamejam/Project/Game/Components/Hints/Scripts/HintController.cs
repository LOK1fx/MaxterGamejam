using UnityEngine;
using com.LOK1game.recode;

namespace com.LOK1game.MaxterGamejam
{
    public class HintController : MonoBehaviour
    {
        private static readonly string _hintPrefabPath = "Prefabs/Hint";

        public static void ShowHint(Hint hint)
        {
            var hudTransform = GameController.GetGameMode<GamejamGameMode>().initializedHud.GetComponent<HUD>().GetHintsTransform();
            var newHintPanel = Instantiate(Resources.Load<HintPanel>(_hintPrefabPath), hudTransform);

            newHintPanel.SetHintInfo(hint);
        }
    }
}