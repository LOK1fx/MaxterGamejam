using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    [System.Serializable]
    public struct Hint
    {
        public string Title;
        [TextArea] public string Description;
        public float ShowTime;

        public Hint(string description)
        {
            Title = "Подсказка!";
            Description = description;

            ShowTime = 2f;
        }

        public Hint(string title, string description)
        {
            Title = title;
            Description = description;

            ShowTime = 2f;
        }

        public Hint(string title, string description, float time)
        {
            Title = title;
            Description = description;

            ShowTime = time;
        }
    }
}