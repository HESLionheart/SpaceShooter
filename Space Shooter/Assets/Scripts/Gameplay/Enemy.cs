using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    #region moving_attrs
    [SerializeField]
    float mov_speed;
    [SerializeField]
    float max_mov_speed;
    [SerializeField]
    float rot_speed;
    [SerializeField]
    float max_rot_speed;
    [SerializeField]
    float mov_radius;
    #endregion
    #region shooting_attrs
    [SerializeField]
    Sprite laser_sprite;
    [SerializeField]
    GameObject laser;
    [SerializeField]
    float cooldown;
    [SerializeField]
    float fire_offset;
    [SerializeField]
    float range;
    #endregion
    static GameObject chaser;
    float timer;
    state stt;

    enum state
    {
        idle,
        moving,
        shooting,
        ramming,
        rotateting,
    }

    private state CalcState()
    {
        if(!(player=GameObject.Find("player")))
            return state.idle;
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if ((GetComponent<CollisionHandler>().health <= 1 && chaser == null) || chaser==gameObject)
        {
            chaser = gameObject;
            return state.ramming;
        }
        else if (dist <= range * Utils.GetScreenWidth())
        {
            if (timer <= 0)
            {
                timer = cooldown;
                return state.shooting;
            }
            else
            {
                timer -= Time.deltaTime;
                return state.rotateting;
            }

        }
        else
            return state.moving;
    }

    void Update()
    {
        stt=CalcState();
        if (stt == state.moving)
        {
            Rotate(rot_speed);
            Move(mov_speed);
        }
        else if (stt == state.rotateting)
            Rotate(rot_speed);
        else if (stt == state.shooting)
        {
            Shoot();
        }
        else if (stt == state.ramming)
        {
            Rotate(max_rot_speed);
            Move(max_mov_speed);
        }
        else
            return;
    }

    void Rotate(float speed)
    {
        float angle = Utils.GetAngle2Point(player.transform.position,transform.position);
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, speed * Time.deltaTime);
    }

    void Move(float speed)
    {
        float y = speed * Time.deltaTime;
        Vector3 velocity = new Vector3(0, y, 0);
        transform.position += transform.rotation * velocity;
    }

    void RestrictMove()
    {
        Vector2 point = new Vector2(transform.position.x,transform.position.y);
        Collider2D[] colls = Physics2D.OverlapCircleAll(point,mov_radius);
        foreach(Collider2D col in colls)
        {
            GameObject go = col.gameObject;
            if(go.CompareTag("Enemy"))
            {

            }
        }
    }

    void Shoot()
    {
        Vector3 offset = transform.rotation * new Vector3(0, fire_offset, 0);
        GameObject go = Instantiate(laser,this.transform.position,this.transform.rotation);
        go.GetComponent<SpriteRenderer>().sprite = laser_sprite;
        go.layer = gameObject.layer;
    } 
}
