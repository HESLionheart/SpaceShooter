using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
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
        Settings temp=Settings.LoadSettings(filename);
        if (!string.IsNullOrEmpty(vic_input.text))
            temp.vic_url = vic_input.text;
        if (!string.IsNullOrEmpty(def_input.text))
            temp.def_url = def_input.text;
        Settings.SaveSettings(filename,temp);
        SceneManager.LoadScene(0);
    }
}
