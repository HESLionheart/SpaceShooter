using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Submit : MonoBehaviour {

    [SerializeField]
    InputField vic_input, def_input;
    [SerializeField]
    string filename;

    public void SaveSettings()
    {
        Settings temp = new Settings();
        if (!string.IsNullOrEmpty(vic_input.text) && temp.vic_url != vic_input.text)
        {
            temp.vic_url = vic_input.text;
            WebDownloader.instance.StartCoroutine(WebDownloader.instance.UpdateIMG(Settings.vic, temp.vic_url));
        }
        if (!string.IsNullOrEmpty(def_input.text) && temp.def_url != def_input.text)
        {
            temp.def_url = def_input.text;
            WebDownloader.instance.StartCoroutine(WebDownloader.instance.UpdateIMG(Settings.def, temp.def_url));
        }
        Settings.SaveSettings(filename,temp);
    }

    
}
