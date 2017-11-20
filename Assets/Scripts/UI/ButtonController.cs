using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    [SerializeField]
    GameObject parent;

    public void InitGame()
    {
        GameManager.instance.InitGame();
    }

    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public void HideParent()
    {
        parent.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
