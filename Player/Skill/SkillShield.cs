using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShield : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    float skillSpeed;                   // 스킬 속도
    Rigidbody2D skillRig;               // 스킬 리지드
    SpriteRenderer player_Ren;          // 플레이어 스프라이트     
    SpriteRenderer shield_Ren;          // 드래곤 스프라이트

    void Start()
    {
        skillSpeed = 300f;
        skillRig = GetComponent<Rigidbody2D>();
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        shield_Ren = GetComponent<SpriteRenderer>();

        ShieldDir();
    }

    // ■■■■■■■ 이동방향 함수 ■■■■■■■
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

    // ■■■■■■■ 스킬의 생존 시간 함수 ■■■■■■■
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 적이 공격으로 날리는 것들 레이어로 막자
    }
}
