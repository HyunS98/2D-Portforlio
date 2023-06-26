using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            PlayerAll.instance.hp -= 11;
            Debug.Log(PlayerAll.instance.hp);
            StartCoroutine(PlayerAll.instance.ChagePlayerColor());
        }
    }
}
