using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShield : MonoBehaviour
{
    // �������� ���� ��������
    float skillSpeed;                   // ��ų �ӵ�
    Rigidbody2D skillRig;               // ��ų ������
    SpriteRenderer player_Ren;          // �÷��̾� ��������Ʈ     
    SpriteRenderer shield_Ren;          // �巡�� ��������Ʈ

    void Start()
    {
        skillSpeed = 300f;
        skillRig = GetComponent<Rigidbody2D>();
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        shield_Ren = GetComponent<SpriteRenderer>();

        ShieldDir();
    }

    // �������� �̵����� �Լ� ��������
    void ShieldDir()
    {
        if (player_Ren.flipX == false)
        {
            skillRig.AddForce(Vector2.right * skillSpeed, ForceMode2D.Force);
        }
        else
        {
            skillRig.AddForce(Vector2.left * skillSpeed, ForceMode2D.Force);
            shield_Ren.flipX = true;
        }
        StartCoroutine(TimeDelay());
    }

    // �������� ��ų�� ���� �ð� �Լ� ��������
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �������� ������ �͵� ���̾�� ����
    }
}
