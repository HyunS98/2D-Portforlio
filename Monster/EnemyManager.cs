using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ¡á¡á¡á¡á¡á¡á¡á º¯¼ö ¡á¡á¡á¡á¡á¡á¡á
    public float hp = 30;    // ¸ó½ºÅÍ Ã¼·Â

    public GameObject monsterList;

    // ¡á¡á¡á¡á¡á¡á¡á ½Ì±ÛÅæ ¡á¡á¡á¡á¡á¡á¡á
    public static EnemyManager instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //monsterList.


        if (hp <= 0)
        {
            gameObject.SetActive(false);
            if(QuestManager.isQuest == true)
            {
                QuestManager.killcnt++;
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.GetComponent<PlayerAll>().isTornado == true)
    //    {
    //        hp -= 100;
    //    }
    //}

    
}
