using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************
    speed의 경우 State.run에서 조절하면 절벽에서도 앞으로 나아감
*****************************/

public class Skeleton : MonoBehaviour
{
    // ■■■■■ 변수 ■■■■■
    enum State { idle, run, attackDelay, attack1, attack2, back, Damage, die }          // 몬스터의 상태
    State state = State.idle;

    public Animator ani;             // 몬스터의 애니메이션
    public CapsuleCollider2D childCap;      // 플레이어와 부딪칠때 밀리지 않게하기 위해 만든 자식오브젝트
    public GameObject GroundCheck;

    int ranMove;                     // idle 상태에서의 움직임, 랜덤값
    float speed = 2f;                // 몬스터의 움직임 속도
    int ran;                         // 공격 유형, 랜덤값
    bool isAttack = false;           // 공격 상태 확인 변수
    public float hp = 10;            // hp
    bool AllState = true;            // 몬스터의 전체 상태 통제 변수

    Transform player;                // 목적(플레이어)
    Vector2 standardPos;             // 초기 위치 
    SpriteRenderer sprite;           // 몬스터의 바라보는 방향 전환 오브젝트
    Collider2D[] colls;                // 공격 범위 박스 오브젝트
    RaycastHit2D hit;

    void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), childCap.GetComponentInChildren<CapsuleCollider2D>());
    }

    void Start()
    {
        standardPos = transform.position;          //GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        
        StartCoroutine(MoveRandom());
    }

    void Update()
    {
        player = GameObject.Find("Player").transform;

        //Debug.Log(GroundCheck.transform.position.x);

        // ■■■■■ idle 상태가 아닐때는 플레이어가 위치한 방향으로 바라봄 ■■■■■
        if (!(state == State.idle || state == State.back))
        {
            // ■■■■■ 플레이어가 있는 방향으로 몬스터 flipX값이 바뀜 ■■■■■
            if (transform.position.x < player.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        if (hp <= 0)
        {
            Debug.Log("죽음");
            state = State.die;
        }

        if(AllState == true)
        {
            switch (state)
            {
                // ■■■■■ idle ■■■■■
                case State.idle:
                    //Debug.Log("idle");

                    // ■■■■■ 몬스터 AI 모드로 스스로 움직임 ■■■■■
                    if (ranMove == 1)
                    {
                        //Debug.Log("움직이는중1");
                        transform.Translate(Vector2.right * speed * Time.deltaTime);
                        ani.SetTrigger("Run");
                        sprite.flipX = false;
                    }
                    else if (ranMove == 2)
                    {
                        //Debug.Log("움직이는중2");
                        transform.Translate(Vector2.left * speed * Time.deltaTime);
                        ani.SetTrigger("Run");
                        sprite.flipX = true;
                    }

                    // ■■■■■ 플레이어와의 거리가 5f 미만일때 Run 으로 변환 ■■■■■
                    if (Vector2.Distance(player.position, transform.position) < 5f)
                    {
                        state = State.run;
                        speed = 2;
                        StopAllCoroutines();
                    }

                    break;

                // ■■■■■ Run ■■■■■
                case State.run:
                    //Debug.Log("Run");
                    ani.SetTrigger("Run");

                    // ■■■■■ 플레이어한테 이동함 ■■■■■
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                    // ■■■■■ 플레이어가 있는 방향으로 몬스터 flipX값이 바뀜 ■■■■■
                    if (Vector2.Distance(player.position, transform.position) <= 0.6f)
                    {
                        ani.SetTrigger("AttackDelay");
                        state = State.attackDelay;
                    }
                    else if (Vector2.Distance(player.position, transform.position) >= 5f)
                    {
                        state = State.back;
                    }
                    break;

                // ■■■■■ AttackDelay ■■■■■
                case State.attackDelay:
                    //Debug.Log("AttackDelay");

                    StartCoroutine(StopAttack());

                    // ■■■■■ 공격 유형이 정해지면 상태전환 ■■■■■
                    if (Vector2.Distance(player.position, transform.position) <= 0.8f && isAttack == false && ran != 0)
                    {
                        if (ran == 1)
                        {
                            state = State.attack1;
                        }
                        else if (ran == 2)
                        {
                            state = State.attack2;
                        }
                    }
                    // ■■■■■ 공격 대기 중에 사거리를 벗어나면 run상태로 전환 ■■■■■
                    else if (Vector2.Distance(player.position, transform.position) > 0.8f)
                    {
                        state = State.run;
                        speed = 2;
                        isAttack = false;
                        ran = 0;
                    }
                    break;

                // ■■■■■ Attack1 ■■■■■
                case State.attack1:
                    //Debug.Log("Attack1");

                    // ■■■■■ 공격 시작과 동시에 초기화 ■■■■■
                    isAttack = true;
                    speed = 0;
                    ran = 0;

                    // ■■■■■ 공격 사정거리안에 들어오면 공격 시작 ■■■■■
                    if (Vector2.Distance(player.position, transform.position) <= 0.8f && isAttack == true)
                    {
                        ani.SetBool("Attack1", true);

                        // ■■■■■ 공격 애니메이션의 진행률이 72%이상 진행되면 대기상태로 전환 ■■■■■
                        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.77f)
                        {
                            isAttack = false;
                            state = State.attackDelay;
                            ani.SetBool("Attack1", false);
                            StopAllCoroutines();
                        }
                    }
                    // ■■■■■ 공격 사정거리 밖으로 이동시 ■■■■■
                    else if (Vector2.Distance(player.position, transform.position) > 0.8f)
                    {
                        // ■■■■■ 진행중인 공격 애니메이션을 끝낸 후 run상태로 이동 ■■■■■
                        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.77f)
                        {
                            state = State.run;
                            speed = 2;
                            isAttack = false;
                            ani.SetBool("Attack1", false);
                            StopAllCoroutines();
                        }
                    }
                    break;

                case State.attack2:

                    // ■■■■■ 공격 시작과 동시에 초기화 ■■■■■
                    isAttack = true;
                    speed = 0;
                    ran = 0;

                    // ■■■■■ 공격 사정거리안에 들어오면 공격 시작 ■■■■■
                    if (Vector2.Distance(player.position, transform.position) <= 0.8f && isAttack == true)
                    {
                        ani.SetBool("Attack2", true);
                        // ■■■■■ 공격 애니메이션의 진행률이 72%이상 진행되면 대기상태로 전환 ■■■■■
                        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack2") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.72f)
                        {
                            isAttack = false;
                            state = State.attackDelay;
                            ani.SetBool("Attack2", false);
                            StopAllCoroutines();
                        }
                    }
                    // ■■■■■ 공격 사정거리 밖으로 이동시 ■■■■■
                    else if (Vector2.Distance(player.position, transform.position) > 0.8f)
                    {
                        // ■■■■■ 진행중인 공격 애니메이션을 끝낸 후 run상태로 이동 ■■■■■
                        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack2") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.72f)
                        {
                            state = State.run;
                            speed = 2;
                            isAttack = false;
                            ani.SetBool("Attack2", false);
                            StopAllCoroutines();
                        }
                    }

                    break;

                // ■■■■■ 복귀 상태 // ■■■■■
                case State.back:
                    //Debug.Log("복귀");

                    // ■■■■■ 플레이어와 일정거리 이상 멀어지면 기준 위치로 돌아옴 ■■■■■
                    transform.position = Vector2.MoveTowards(transform.position, standardPos, speed * Time.deltaTime);

                    if(transform.position.x < standardPos.x)
                    {
                        sprite.flipX = false;
                    }
                    else
                    {
                        sprite.flipX = true;
                    }

                    if (Vector2.Distance(transform.position, standardPos) < 0.2f)
                    {
                        transform.position = standardPos;
                        ani.ResetTrigger("Run");
                        ani.SetTrigger("Idle");
                        state = State.idle;
                        StopAllCoroutines();
                        StartCoroutine(MoveRandom());
                        //Debug.Log("복귀완료");
                    }
                    break;

                case State.Damage:
                    //Damage();

                    break;

                case State.die:
                    if (QuestManager.isQuest == true && ScrollViewManager.point == 1)
                    {
                        QuestManager.killcnt++;
                    }
                    
                    StartCoroutine(DieDestroy());
                    break;
            }

            Danger();
        }
    }

    // ■■■■■ 낭떠러지 확인 함수 ■■■■■
    void Danger()
    {
        Debug.DrawRay(GroundCheck.transform.position, Vector3.down * 0.5f, Color.blue);

        if (sprite.flipX == true)
        {
            GroundCheck.transform.position = new Vector2(transform.position.x - 0.35f, GroundCheck.transform.position.y);
        }
        else if (sprite.flipX == false)
        {
            GroundCheck.transform.position = new Vector2(transform.position.x + 0.35f, GroundCheck.transform.position.y);
        }

        RaycastHit2D hit = Physics2D.Raycast(GroundCheck.transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
        {
            speed = 0;
        }
        else
        {
            speed = 2;
        }
    }

    // ■■■■■ 애니메이션에 맞춰 플레이어에게 대미지를 줌 ■■■■■ (공격별 Animation 부분에 들어가있음)
    void Damage(int x)
    {
        if (transform.position.x < player.position.x)
        {
            colls = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 1, transform.position.y), new Vector2(1, 1), 0);
        }
        else
        {
            colls = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 1, transform.position.y), new Vector2(1, 1), 0);
        }

        if(colls != null)
        {
            for(int i=0; i<colls.Length; i++)
            {
                if (colls[i].gameObject.layer == 3)
                {
                    PlayerAll.instance.hp -= x;
                    //Debug.Log(PlayerAll.instance.hp);
                    StartCoroutine(PlayerAll.instance.ChagePlayerColor());
                }
            }
        }
    }

    // ■■■■■ 스킬 또는 기본공격에 따른 피해를 받음 ■■■■■
    void OnTriggerEnter2D(Collider2D other)
    {
        // ■■■ 플레이어 스킬 ■■■
        if (other.gameObject.layer == 9)
        {
            hp -= 10;
            Debug.Log(gameObject.name + " " + hp);
            StartCoroutine(ChangeColor());
        }

        // ■■■ 플레이어 기본공격 ■■■
        if (other.gameObject.tag == "DamageBox")
        {
            hp -= 3;
            Debug.Log(gameObject.name + " " + hp);
            StartCoroutine(ChangeColor());
        }
    }

    // ■■■■■ idle 상태 움직임 반복 ■■■■■
    IEnumerator MoveRandom()
    {
        yield return new WaitForSeconds(1.5f);
        ranMove = Random.Range(1, 3);
        yield return new WaitForSeconds(1.5f);
        ranMove = 0;
        ani.ResetTrigger("Run");            // Run 애니메이션 취소
        ani.SetTrigger("Idle");
        StartCoroutine(MoveRandom());       // 움직임 반복재생(재귀)
    }

    // ■■■■■ 공격대기 동안 랜덤값 ■■■■■
    IEnumerator StopAttack()
    {
        ani.SetTrigger("AttackDelay");
        yield return new WaitForSeconds(0.7f);  // 공격 딜레이 시간
        if (ran == 0)
        {
            ran = Random.Range(1, 3);
        }
    }

    // ■■■■■ 피해를 입었을때 일정시간 빨강색으로 변함 ■■■■■
    IEnumerator ChangeColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(255, 255, 255, 255);
    }

    // ■■■■■ 죽었을때 모든 움직임, 오브젝트를 멈춤 ■■■■■
    IEnumerator DieDestroy()
    {
        ani.SetTrigger("Die");
        AllState = false;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(new Vector2(transform.position.x + 1, transform.position.y), new Vector3(1, 1, 0));
    //}
}
