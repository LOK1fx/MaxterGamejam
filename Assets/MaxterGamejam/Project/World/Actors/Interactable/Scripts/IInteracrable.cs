namespace com.LOK1game.recode
{
    public interface IInteractable
    {
        void Use(object sender);

        /// <summary>
        /// Выполняется при наведении на объект
        /// </summary>
        void OnHover(object sender);
    }
}