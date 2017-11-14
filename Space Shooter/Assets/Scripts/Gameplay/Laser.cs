using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float timeout;
    public float speed;

    private void Start()
    {
        Destroy(gameObject, timeout);
    }

    void Update () {
        Move();
	}

    void Move()
    {
        float y = speed * Time.deltaTime;
        Vector3 velocity = new Vector3(0, y, 0);
        transform.position += transform.rotation * velocity;
    }
}
