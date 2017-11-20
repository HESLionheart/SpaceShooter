using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    Settings settings;
    [SerializeField]
    string filename;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    float radius;
    [SerializeField]
    Canvas canvas;
    RawImage image; 
    int score;
    List<GameObject> enemies;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        settings = Settings.LoadSettings(filename);
        InitGame();
    }

    public void InitGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + Settings.vic))
            WebDownloader.instance.StartCoroutine(WebDownloader.instance.UpdateIMG(Settings.vic,settings.vic_url));
        if (!File.Exists(Application.persistentDataPath + "/" + Settings.def))
            WebDownloader.instance.StartCoroutine(WebDownloader.instance.UpdateIMG(Settings.def, settings.def_url));
        image = canvas.GetComponentInChildren<RawImage>();
        enemies = new List<GameObject>();
        SpawnEnemies(settings.high_score + 1);
    }

    void SpawnEnemies(int count)
    {
        for(int i=0;i<360;i+=360/count)
        {
            float angle = Mathf.Deg2Rad*(i);
            GameObject temp=Instantiate(enemy,new Vector3(Mathf.Sin(angle)*radius,Mathf.Cos(angle)*radius,0),Quaternion.Euler(0,0,0));
            enemies.Add(temp);
        }
    }

    public void Kill(GameObject go)
    {
        if (go.layer == LayerMask.NameToLayer("player"))
            StartCoroutine(GameOver(false));
        else if (go.layer == LayerMask.NameToLayer("enemy"))
        {
            enemies.Remove(go);
            Destroy(go);
            if (enemies.Count <= 0)
                StartCoroutine(GameOver(true));
        }
        else
            Destroy(go);

    }

    public IEnumerator GameOver(bool win)
    {
        canvas.gameObject.SetActive(true);
        string path = "file://" + Application.persistentDataPath + "/";
        if (win)
        {
            path += Settings.vic;
            settings.high_score++;
            Settings.SaveSettings(filename,settings);
        }
        else
            path += Settings.def;
        WWW www = new WWW(path);
        yield return www;
        image.texture = www.texture;
    }
}
