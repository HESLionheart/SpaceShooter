using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {
    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
