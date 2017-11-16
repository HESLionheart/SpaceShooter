using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Settings
    {
        public int high_score;
        public string vic_url;
        public string def_url;

        public Settings()
        {
            high_score = 0;
            vic_url = "";
            def_url = "";
        }

        public static Settings CreateSettingsFile(string filename)
        {
            Settings temp = new Settings();
            string json = JsonUtility.ToJson(temp);
            using (StreamWriter sw = File.CreateText(Application.persistentDataPath + "/" + filename))
            {
                sw.WriteLine(json);
            }
            return temp;
        }

        public static Settings LoadSettings(string filename)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + filename))
                return CreateSettingsFile(filename);
            string json = File.ReadAllText(Application.persistentDataPath + "/" + filename);
            return JsonUtility.FromJson<Settings>(json);
        }

        public static void SaveSettings(string filename,Settings settings)
        {
            File.WriteAllText(Application.persistentDataPath + "/" + filename,JsonUtility.ToJson(settings));
        }
    }
}
