using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region moving_attrs
    [SerializeField]
    float mov_speed;
    [SerializeField]
    float rot_speed;
    [SerializeField]
    float radius;
    #endregion
    #region shooting_attrs
    [SerializeField]
    float fire_delay;
    [SerializeField]
    float fire_offset;
    [SerializeField]
    float cooldown;
    [SerializeField]
    GameObject laser;
    [SerializeField]
    Sprite laser_sprite;
    #endregion

    void Start()
    {

    }


    void Update()
    {
        //Move();
        MoveByMouse();
        if (Input.GetButton("Fire1"))
            Shoot();

    }

    private void Move()
    {
        float y = Input.GetAxis("Vertical") * mov_speed * Time.deltaTime;
        float z = transform.rotation.eulerAngles.z - Input.GetAxis("Horizontal") * rot_speed * Time.deltaTime;
        Vector3 velocity = new Vector3(0, y, 0);
        Quaternion rot = Quaternion.Euler(0, 0, z);
        transform.rotation = rot;
        transform.position += rot * velocity;
        RestrictMove(transform.position);
    }

    private void MoveByMouse()
    {
        float angle = Utils.GetAngle2Point(Utils.GetMousePos2D(),transform.position);
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        float y = Input.GetAxis("Vertical") * mov_speed * Time.deltaTime;
        Vector3 velocity = new Vector3(0, y, 0);
        transform.rotation = rot;
        transform.position += transform.rotation * velocity;
        RestrictMove(transform.position);
    }

    private void RestrictMove(Vector3 pos)
    {
        if (pos.y + radius > Camera.main.orthographicSize)
            pos.y = Camera.main.orthographicSize - radius;
        else if (pos.y - radius < -Camera.main.orthographicSize)
            pos.y = -Camera.main.orthographicSize + radius;
        float width = Utils.GetScreenWidth();
        if (pos.x + radius > width)
            pos.x = width - radius;
        else if (pos.x - radius < -width)
            pos.x = -width + radius;
        transform.position = pos;
    }

    private void Shoot()
    {
        if (cooldown <= 0)
        {
            cooldown = fire_delay;
            Vector3 offset = transform.rotation * new Vector3(0,fire_offset,0);
            GameObject go=Instantiate(laser,transform.position+offset,transform.rotation);
            go.GetComponent<SpriteRenderer>().sprite = laser_sprite;
            go.layer = gameObject.layer;
        }
        else
            cooldown -= Time.deltaTime;
    }
}
