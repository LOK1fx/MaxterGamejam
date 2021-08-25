using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LOK1game.Tools;

namespace com.LOK1game.recode
{
    public class Subtitles : MonoBehaviour
    {
        public static event Action<int> OnSubtitleAdded;

        private static List<Subtitle> _subtitles = new List<Subtitle>();

        private static void DeleteSubtitle(int index)
        {
            Destroy(_subtitles[index].gameObject);
        }

        public static void Subtitle(string key, string speakerKey = "", float time = 1f)
        {
            var newSub = Instantiate(Resources.Load("Prefabs/Subtitle") as GameObject, GameObject.Find("SubtitlesCanvas").transform);

            _subtitles.Add(newSub.GetComponent<Subtitle>());

            _subtitles[_subtitles.Count - 1].SetText(key, speakerKey);

            Coroutines.StartRoutine(HideSubtitle(_subtitles.Count - 1, time));

            OnSubtitleAdded?.Invoke(_subtitles.Count - 1);
        }

        private static IEnumerator HideSubtitle(int index, float time)
        {
            yield return new WaitForSeconds(time);

            DeleteSubtitle(index);
        }
    }
}