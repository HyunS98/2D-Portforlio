using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLight : MonoBehaviour
{
    // �������� ���� ��������
    Animator ani;                     // ��ü �ִϸ��̼� ��������
    float skillSpeed;                 // ��ų �ӵ�
    Rigidbody2D skillRig;             // ��ų ������
    SpriteRenderer player_Ren;        // �÷��̾� ��������Ʈ
    SpriteRenderer light_Ren;         // ����Ʈ ��������Ʈ  

    void Start()
    {
        skillSpeed = 7;
        skillRig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        light_Ren = GetComponent<SpriteRenderer>();

        Light_Dir();
    }

    void Update()
    {
        StartCoroutine(TimeDelay());
    }

    // �������� �̵����� �Լ� ��������
    void Light_Dir()
    {
        if (player_Ren.flipX == false)
        {
            skillRig.velocity = new Vector2(skillSpeed, 0);
        }
        else
        {
            skillRig.velocity = new Vector2(-skillSpeed, 0);
            light_Ren.flipX = true;
        }
    }

    // �������� �ִϸ��̼� �� ���� �Լ� ��������
    IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // �������� ��ų�� ���� �ð� �Լ� ��������
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    // �������� �� or ��ü �ε�ġ�� ����� ��������
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 6 || other.gameObject.layer == 8 || other.gameObject.layer == 13)
        {
            skillRig.velocity = Vector2.zero;
            ani.SetTrigger("End");
            StartCoroutine(DeleteDelay());
        }

        //if(other.gameObject.layer == 13)
        //{
        //    Boss.instance.hp -= 10;
        //    skillRig.velocity = Vector2.zero;
        //    ani.SetTrigger("End");
        //    StartCoroutine(Boss.instance.ChageBossColor());
        //}
    }
}
