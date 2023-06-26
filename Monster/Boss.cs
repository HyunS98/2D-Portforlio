using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    enum State { Idle, Run, AttackDelay, Attack1, Attack2, AttackDash, Stun }
    State state;

    // ■■■■■■■ 변수 ■■■■■■■
    public CapsuleCollider2D childCap; // 자식 콜라이더
    public BossDashAttack dash_Attack; // 대쉬(공격) 스크립트 가져오기
    public BossRange boss_range;       // 플레이어 인식 범위 변수
    public float hp = 150;

    GameObject player;               // 플레이어 찾아오기
    Animator ani;                    
    Rigidbody2D rigid;               
    CapsuleCollider2D boss_col;      // 보스 콜라이더
    SpriteRenderer sprite;           // 보스 sprite
    RaycastHit2D hit;                // 대쉬진행간 벽 충돌 확인 변수
    Collider2D[] colls;                // 공격 범위 박스 오브젝트

    // ■■■■■■■ 보스 움직임 ■■■■■■■
    float speed = 2f;
    int ranMove;

    // ■■■■■■■ 보스 공격 ■■■■■■■
    int ranAttack;
    bool isAttack = false;

    // ■■■■■■■ 보스와 플레이어의 거리 판단 ■■■■■■■
    float playerAndBossDistance = 2f;

    // ■■■■■■■ 보스 죽음 ■■■■■■■
    bool boss_die = false;

    // ■■■■■■■ 싱글톤 ■■■■■■■
    public static Boss instance;

    void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), childCap.GetComponentInChildren<CapsuleCollider2D>());
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    void Start()
    {
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        boss_col = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        state = State.Idle;

        StartCoroutine(IdleMove());
    }

    void Update()
    {
        if (boss_die == false)
        {
            player = GameObject.Find("Player");
            Die();

            if (sprite.flipX == true)
            {
                boss_col.offset = new Vector2(0.4f, boss_col.offset.y);
            }
            else
            {
                boss_col.offset = new Vector2(-0.4f, boss_col.offset.y);
            }
            childCap.offset = boss_col.offset;

            switch (state)
            {
                case State.Idle:
                    //Debug.Log("idle");

                    if (ranMove == 0)
                    {
                        ani.SetTrigger("Idle");
                    }
                    else if (ranMove == 1)
                    {
                        transform.Translate(Vector2.right * speed * Time.deltaTime);
                        ani.SetTrigger("Run");
                        sprite.flipX = false;
                    }
                    else if (ranMove == 2)
                    {
                        transform.Translate(Vector2.left * speed * Time.deltaTime);
                        ani.SetTrigger("Run");
                        sprite.flipX = true;
                    }

                    if (boss_range.Range == true)
                    {
                        state = State.Run;
                        ranMove = 0;
                        StopAllCoroutines();
                    }

                    break;

                case State.Run:
                    //Debug.Log("run");
                    speed = 4f;

                    ani.SetTrigger("Run");
                    if (Vector2.Distance(transform.position, player.transform.position) > playerAndBossDistance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + 1.6f), speed * Time.deltaTime);
                    }

                    if (transform.position.x > player.transform.position.x)
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }

                    if (Vector2.Distance(transform.position, player.transform.position) <= playerAndBossDistance)
                    {
                        speed = 0;
                        state = State.AttackDelay;
                        ani.SetTrigger("AttackDelay");
                        ani.ResetTrigger("Run");
                    }

                    if (boss_range.Range == false)
                    {
                        state = State.Idle;
                        speed = 2f;
                        StartCoroutine(IdleMove());
                    }
                    break;

                case State.AttackDelay:
                    //Debug.Log("공격 딜레이");

                    StartCoroutine(AttackRan());

                    if (Vector2.Distance(transform.position, player.transform.position) <= 2.2 && ranAttack != 0 && boss_range.Range == true)
                    {
                        if (ranAttack == 1)
                        {
                            state = State.Attack1;
                        }
                        else if (ranAttack == 2)
                        {
                            state = State.Attack2;
                        }
                        else if (ranAttack == 3)
                        {
                            state = State.AttackDash;
                        }
                    }
                    else if (Vector2.Distance(transform.position, player.transform.position) > 2.2f)
                    {
                        isAttack = false;
                        ranAttack = 0;
                        ani.ResetTrigger("AttackDelay");
                        StartCoroutine(MoveDelay());
                    }
                    break;

                case State.Attack1:
                    //Debug.Log("공격1");

                    isAttack = true;
                    ani.SetBool("Attack1", true);

                    if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.77f)
                    {
                        StopAllCoroutines();
                        state = State.AttackDelay;
                        ani.SetBool("Attack1", false);
                        ranAttack = 0;
                    }

                    break;


                case State.Attack2:
                    //Debug.Log("공격2");

                    ani.SetBool("Attack2", true);

                    if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.77f)
                    {
                        StopAllCoroutines();
                        state = State.AttackDelay;
                        ani.SetBool("Attack2", false);
                        ranAttack = 0;
                    }

                    break;

                case State.AttackDash:
                    //Debug.Log("공격대쉬");
                    speed = 8;
                    ani.SetBool("AttackDash", true);

                    childCap.isTrigger = true;
                    boss_col.isTrigger = true;

                    if (sprite.flipX == false)
                    {
                        rigid.velocity = Vector2.right * speed;
                        hit = Physics2D.Raycast(transform.position, Vector2.right, 0.7f, LayerMask.GetMask("Wall"));
                    }
                    else
                    {
                        rigid.velocity = Vector2.left * speed;
                        hit = Physics2D.Raycast(transform.position, Vector2.left, 0.7f, LayerMask.GetMask("Wall"));
                    }

                    if (hit)
                    {
                        StopAllCoroutines();
                        ani.SetBool("AttackDash", false);
                        state = State.AttackDelay;
                        ranAttack = 0;
                        childCap.isTrigger = false;
                        boss_col.isTrigger = false;
                    }

                    break;
                case State.Stun:
                    {
                        ani.SetBool("Stun", true);
                        StartCoroutine(StunEnd());
                    }
                    break;
            }
            //if(Input.GetMouseButtonDown(0))
            //{
            //    Debug.Log(Vector2.Distance(transform.position, player.transform.position));
            //}
        }
    }

    // ■■■■■■■ 공격1,2의 플레이어에게 데미지 부여 하는 함수 ■■■■■■■
    void Damage(int x)
    {
        //Debug.Log("대미지");
        if (transform.position.x < player.transform.position.x)
        {
            colls = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 1.6f, transform.position.y - 0.5f), new Vector2(2, 3), 0);
        }
        else
        {
            colls = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 1.6f, transform.position.y - 0.5f), new Vector2(2, 3), 0);
        }

        if (colls != null)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].gameObject.layer == 3)
                {
                    PlayerAll.instance.hp -= x;
                    Debug.Log(PlayerAll.instance.hp);
                    StartCoroutine(PlayerAll.instance.ChagePlayerColor());
                }
            }
        }
    }

    // ■■■■■■■ 보스 죽음 ■■■■■■■
    void Die()
    {
        if(hp <= 0 && !boss_die)
        {
            ani.SetTrigger("Die");
            boss_die = true;
        }
    }

    // ■■■■■ 스킬 또는 기본공격에 따른 피해를 받음 ■■■■■
    void OnTriggerEnter2D(Collider2D other)
    {
        // ■■■ 플레이어 스킬 ■■■
        if (other.gameObject.layer == 9)
        {
            hp -= 10;
            //Debug.Log(gameObject.name + " " + hp);
            StartCoroutine(ChageBossColor());
            if(other.gameObject.tag == "Shield")
            {
                state = State.Stun;
                speed = 0;
            }
        }

        // ■■■ 플레이어 기본공격 ■■■
        if (other.gameObject.layer == 15)
        {
            Debug.Log("보스, 플레이어 기본 공격 당함");
            hp -= 3;
            Debug.Log(gameObject.name + " " + hp);
            StartCoroutine(ChageBossColor());
        }
    }

    // ■■■■■■■ idle 방향 고르기 ■■■■■■■
    IEnumerator IdleMove()
    {
        yield return new WaitForSeconds(1.0f);
        if (ranMove == 0)
        {
            ranMove = Random.Range(0, 3);
        }
        yield return new WaitForSeconds(2.0f);
        ranMove = 0;
        ani.ResetTrigger("Run");

        StartCoroutine(IdleMove());
    }

    // ■■■■■■■ 공격 방법 랜덤 뽑기 ■■■■■■■
    IEnumerator AttackRan()
    {
        yield return new WaitForSeconds(1.0f);
        if (ranAttack == 0)
        {
            ranAttack = Random.Range(1, 4);
        }
    }

    // ■■■■■■■ 다음 동작 딜레이 ■■■■■■■
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(0.3f);
        ani.SetBool("Stun", false);
        state = State.Run;
        StopAllCoroutines();
    }

    // ■■■■■■■ 스턴 적용 후 공격 대기모션 ■■■■■■■
    IEnumerator StunEnd()
    {
        yield return new WaitForSeconds(0.5f);
        state = State.AttackDelay;
    }

    public IEnumerator ChageBossColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(255, 255, 255, 255);
    }
}
