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
    [SerializeField]
    GameObject parent;
    public bool done;

    private void Start()
    {
        done = false;
    }

    public void SaveSettings()
    {
        Settings temp = new Settings();
        if (!string.IsNullOrEmpty(vic_input.text) && temp.vic_url != vic_input.text)
        {
            temp.vic_url = vic_input.text;
            UpdateIMG(Settings.vic, temp.vic_url);
        }
        if (!string.IsNullOrEmpty(def_input.text) && temp.def_url != def_input.text)
        {
            temp.def_url = def_input.text;
            UpdateIMG(Settings.def, temp.def_url);
        }
        Settings.SaveSettings(filename,temp);
        SceneManager.LoadScene(0);
        parent.SetActive(false);
    }

    void UpdateIMG(string filename, string url)
    {
        Debug.Log("url");
        WWW www = new WWW(url);
        while(!www.isDone)
        {
            //Debug.Log(www.progress);
        }
        byte[] bytes = www.texture.EncodeToPNG();
        if (!File.Exists(Application.persistentDataPath + "/" + filename))
        {
            Stream file = File.Create(Application.persistentDataPath + "/" + filename);
            file.Write(bytes, 0, bytes.Length);
        }
        else
            File.WriteAllBytes(Application.persistentDataPath + "/" + filename, bytes);
        done = true;
    }
}
