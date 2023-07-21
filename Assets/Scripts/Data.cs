using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Data
{
    public static string CURRENT_LANGUAGE = "english";

    public static Dictionary<string, Dictionary<string, string>> LOCALIZATION =
        new Dictionary<string, Dictionary<string, string>>()
        {
            {"shild_key", new Dictionary<string, string>()
            {
                {"english","Shild" },
                {"ukrainian","Щит" },
            } 
            },
            {"paus_key", new Dictionary<string, string>()
            {
                {"english","Paus" },
                {"ukrainian","Пауза" },
            }
            },
            {"continue_key", new Dictionary<string, string>()
            {
                {"english","Continue" },
                {"ukrainian","Продовжити" },
            }
            },
            {"exit_key", new Dictionary<string, string>()
            {
                {"english","Exit" },
                {"ukrainian","Вийти" },
            }
            },
        };

    public static string[] LANGUAGES = new string[] { "english", "ukrainian" };

    private static UnityEvent onLanguageChanged;
    public static UnityEvent OnLanguageChanged
    {
        get
        {
            if(onLanguageChanged == null)
            {
                onLanguageChanged = new UnityEvent();
            }
            return onLanguageChanged;
        }
    }
}