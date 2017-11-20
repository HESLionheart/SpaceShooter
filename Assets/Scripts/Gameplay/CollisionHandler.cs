using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public int health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        health--;
        Debug.Log(gameObject.name+":"+health);
    }

    private void LateUpdate()
    {
        if (health <= 0)
        {
            GameManager.instance.Kill(gameObject);
        }
    }
}
