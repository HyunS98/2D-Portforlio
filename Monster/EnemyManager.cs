using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // �������� ���� ��������
    public float hp = 30;    // ���� ü��

    public GameObject monsterList;

    // �������� �̱��� ��������
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
