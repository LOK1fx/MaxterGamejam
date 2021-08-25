using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem : MonoBehaviour
{
    public enum Language
    {
        English,
        Russian,
        Chinese
    }

    public static Language language = Language.Russian;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedRU;
    private static Dictionary<string, string> localisedZH;

    public static bool isInit;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        //if(Application.systemLanguage == SystemLanguage.Russian)
        //{
        //    language = Language.Russian;
        //}
        //else
        //{
        //    language = Language.English;
        //}

        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedRU = csvLoader.GetDictionaryValues("ru");
        localisedZH = csvLoader.GetDictionaryValues("zh");

        isInit = true;
    }

    public static string GetLocalisedValue(string key)
    {
        if(!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Russian:
                localisedRU.TryGetValue(key, out value);
                break;
            case Language.Chinese:
                localisedZH.TryGetValue(key, out value);
                break;
            default:
                localisedEN.TryGetValue(key, out value);
                break;
        }

        return value;
    }
}
