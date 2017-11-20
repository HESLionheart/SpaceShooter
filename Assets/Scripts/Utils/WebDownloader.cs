using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WebDownloader : MonoBehaviour
{

    public static WebDownloader instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public IEnumerator UpdateIMG(string filename, string url)
    {
        WWW www = new WWW(url);
        yield return www;
        byte[] bytes = www.texture.EncodeToPNG();
        if (!File.Exists(Application.persistentDataPath + "/" + filename))
        {
            Stream file = File.Create(Application.persistentDataPath + "/" + filename);
            file.Write(bytes, 0, bytes.Length);
        }
        else
            File.WriteAllBytes(Application.persistentDataPath + "/" + filename, bytes);
    }
}
