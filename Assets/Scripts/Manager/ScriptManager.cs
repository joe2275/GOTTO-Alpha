using System.Collections.Generic;
using CSV;
using UnityEngine;

namespace Manager
{
    public static class ScriptManager
    {
        private static Dictionary<string, Dictionary<string, string>> _scriptDict;
        public static Language Language { get; private set; }

        public static void Load()
        {
            _scriptDict = CsvReader.Read(Resources.Load<TextAsset>("Script").text);
            var enumerator = _scriptDict.GetEnumerator();
            if (enumerator.MoveNext())
            {
                int index = 0;
                string[] languages = new string[enumerator.Current.Value.Keys.Count];

                var languageEnumerator = enumerator.Current.Value.Keys.GetEnumerator();
                while (languageEnumerator.MoveNext())
                {
                    languages[index++] = languageEnumerator.Current;
                }

                Language = new Language(languages);
                
                languageEnumerator.Dispose();
            }
            
            enumerator.Dispose();
        }

        public static bool HasKey(string key)
        {
            return key != null && _scriptDict.ContainsKey(key);
        }
        
        public static string GetScript(string key)
        {
            return _scriptDict[key][Language.Current];
        }

        public static string GetScript(string tag, params object[] parameters)
        {
            return string.Format(_scriptDict[tag][Language.Current], parameters);
        }

    }
    
    public class Language
    {
        private int mIndex;
        private readonly string[] languages;

        public string Current
        {
            get => languages[mIndex];
            set
            {
                for (int i = 0; i < languages.Length; i++)
                {
                    if (languages[i].Equals(value))
                    {
                        mIndex = i;
                        return;
                    }
                }
            }
        }

        public int Index
        {
            get => mIndex;
            set
            {
                if (value >= 0 && value < languages.Length)
                {
                    mIndex = value;
                }
            }
        }
        public int Count => languages.Length;
        public string this[int index] => languages[index];


        public Language(string[] languages)
        {
            this.languages = languages;
        }
    }
}
