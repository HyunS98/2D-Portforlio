using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDragon : MonoBehaviour
{
    // �������� ���� ��������
    float skillSpeed;                   // ��ų �ӵ�
    Rigidbody2D skillRig;               // ��ų ������
    SpriteRenderer player_Ren;          // �÷��̾� ��������Ʈ     
    SpriteRenderer dragon_Ren;          // �巡�� ��������Ʈ
    
    void Start()
    {
        skillSpeed = 100f;
        skillRig = GetComponent<Rigidbody2D>();
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        dragon_Ren = GetComponent<SpriteRenderer>();

        DragonDir();
    }

    // �������� �̵����� �Լ� ��������
    void DragonDir()
    {
        if (player_Ren.flipX == false)
        {
            skillRig.AddForce(Vector2.right * skillSpeed, ForceMode2D.Force);
        }
        else
        {
            skillRig.AddForce(Vector2.left * skillSpeed, ForceMode2D.Force);
            dragon_Ren.flipX = true;
        }
        StartCoroutine(TimeDelay());
    }

    // �������� ��ų�� ���� �ð� �Լ� ��������
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.gameObject.layer == 8)
    //    {
    //        other.GetComponent<EnemyManager>().hp -= 10;
    //        Debug.Log(gameObject.name + " : " + other.GetComponent<EnemyManager>().hp);
    //    }
    //}
}
