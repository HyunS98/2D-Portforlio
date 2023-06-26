using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    public Vector2 min;
    public Vector2 max;
    Vector2 target_Pos;
    int speed;

    // ■■■■■■■ 처음에 움직여야 작동 ■■■■■■■
    void Start()
    {
        target_Pos = min;
        speed = 2;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target_Pos, speed * Time.deltaTime);

        if (new Vector2(transform.position.x, transform.position.y) == min)
        {
            target_Pos = max;
        }
        if (new Vector2(transform.position.x, transform.position.y) == max)
        {
            target_Pos = min;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.transform.SetParent(null);

            DontDestroyOnLoad(other.gameObject);
        }
    }

}
