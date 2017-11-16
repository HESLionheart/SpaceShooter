﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    Settings settings;
    [SerializeField]
    string filename;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    float radius;
    int score;
    List<GameObject> enemies;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        settings = Settings.LoadSettings(filename);
        enemies = new List<GameObject>();
        SpawnEnemies(settings.high_score+1);
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
            GameOver(false);
        else if(go.layer == LayerMask.NameToLayer("enemy"))
        {
            enemies.Remove(go);
            Destroy(go);
            if (enemies.Count <= 0)
                GameOver(true);
        }

    }

    public void GameOver(bool win)
    {

    }
}