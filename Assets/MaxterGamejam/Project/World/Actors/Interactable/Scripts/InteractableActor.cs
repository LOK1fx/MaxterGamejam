using UnityEngine;

namespace com.LOK1game.recode
{
    public abstract class InteractableActor : MonoBehaviour, IInteractable
    {
        protected bool isHovered;

        public abstract void Use(object sender);
        public abstract void OnHover(object sender);
    }
}