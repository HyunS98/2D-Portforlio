using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillArrow : MonoBehaviour
{
    // �������� ���� ��������
    Animator ani;                     // ��ü �ִϸ��̼� ��������
    float skillSpeed;                 // ��ų �ӵ�
    Rigidbody2D skillRig;             // ��ų ������
    SpriteRenderer player_Ren;        // �÷��̾� ��������Ʈ
    SpriteRenderer arrow_Ren;         // ����Ʈ ��������Ʈ  

    void Start()
    {
        skillSpeed = 10;
        skillRig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        arrow_Ren = GetComponent<SpriteRenderer>();

        Arrow_Dir();
    }

    void Update()
    {
        StartCoroutine(TimeDelay());
    }

    // �������� �̵����� �Լ� ��������
    void Arrow_Dir()
    {
        if (player_Ren.flipX == false)
        {
            skillRig.velocity = new Vector2(skillSpeed, 0);
        }
        else
        {
            skillRig.velocity = new Vector2(-skillSpeed, 0);
            arrow_Ren.flipX = true;
        }
    }

    // �������� ��ų�� ���� �ð� �Լ� ��������
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    // �������� �ִϸ��̼� �� ���� �Լ� ��������
    IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // �������� �� or ��ü �ε�ġ�� ����� ��������
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 7 || other.gameObject.layer == 6 || other.gameObject.layer == 13)
        {
            skillRig.velocity = Vector2.zero;
            ani.SetTrigger("End");
            StartCoroutine(DeleteDelay());
        }
    }
}
