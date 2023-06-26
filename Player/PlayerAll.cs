using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAll : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    Rigidbody2D rigid;
    Animator ani;
    public static SpriteRenderer spriteRender;

    // 플레이어 전체적인 움직임 제어
    static public bool All_PlayerMove = true;

    // 움직임 관련 변수
    [Header ("-- Move --")]
    public float MoveSpeed = 5f;
    public float JumpPower = 6.5f;
    public float dir;

    // 점프 상태
    int JumpCnt = 2;

    // 벽타기 관련 변수(벽)
    [Header ("-- Wall --")]
    public LayerMask w_Layer;
    public Transform wallCheck;
    public float wallchkDistance;
    public bool isWall;
    float slidingSpeed = 0.5f;

    // 땅 관련 변수
    [Header("-- Ground --")]
    public LayerMask g_Layer;
    public Transform groundCheck;
    public float groundchkDistance;
    public bool isGround = false;

    // 공격 관련 변수
    public GameObject AutoBoxPrf;
    //DamageBox Dam_Box;
    float attackSpeed = 2f;
    public Transform AutoBoxPos;
    bool isAttack = false;

    // 대쉬 관련 변수
    [Header("-- AutoAttack --")]
    float dashPower = 15f;
    bool isDash = false;
    float dashTime;

    // 먼지
    [Header("-- Smoke --")]
    public GameObject dustPrf;

    // 플레이어 체력
    [Header("-- HP --")]
    public int hp;
    bool dieEnd = false;


    // ■■■■■■■ 싱글톤 ■■■■■■■
    public static PlayerAll instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        var obj = FindObjectsOfType<PlayerAll>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        //Dam_Box = GameObject.Find("AutoAttack").GetComponent<DamageBox>();

        StartCoroutine(AutoHp());
    }

    void Update()
    {
        Check();
        if (All_PlayerMove == true)
        {
            ani.StopPlayback();

            dir = Input.GetAxis("Horizontal");

            Jump();
            Attack();
            Dash();

            Dash_SkillTime();         // 모든 스킬 쿨타임 조절 함수
            Die();
        }
    }

    void FixedUpdate()
    {
        WallSlide();
        Move();
    }

    // ■■■■■■■ 움직임 ■■■■■■■
    void Move()
    {
        /* FixedUpdate 에서 구현하게 함으로써 벽 충돌을 보완가능
           리지드바디로 사용하면 미끄러짐이 발생하여 Translate로 구현 */
        if (dir != 0)
        {
            transform.Translate(Vector2.right * dir * MoveSpeed * Time.deltaTime);
            ani.SetBool("isRun", true);

            if (dir > 0)
            {
                spriteRender.flipX = false;
            }
            if (dir < 0)
            {
                spriteRender.flipX = true;
            }
        }
        else
        {
            ani.SetBool("isRun", false);
        }
    }

    // ■■■■■■■ 점프 ■■■■■■■
    void Jump()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && JumpCnt > 0 && !isWall)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            JumpCnt--;
            ani.SetBool("isGround", false);
            ani.SetBool("isSlide", false);
            ani.SetBool("isRun", false);
        }

        Exceoption();
    }

    // ■■■■■■■ 벽 타기 ■■■■■■■
    void WallSlide()
    {
        // 슬라이딩 조건 (벽에 붙어있으면서 땅과 근접해 있지 않을때 슬라이딩)
        if (isWall && !isGround)
        {
            ani.SetBool("isSlide", true);
            ani.SetBool("isGround", false);
            ani.SetBool("isJump", false);
            ani.SetBool("isDoubleJump", false);
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
            ani.SetBool("isRun", false);
            if (JumpCnt < 2)
            {
                JumpCnt = 2;
            }
        }
    }

    // ■■■■■■■ 캐릭터 공격 ■■■■■■■
    void Attack()
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            /* 다중 클릭으로 모션이 겹치는걸 방지, 공격모션 1번에 공격 1번 적용 */
            if (Input.GetButtonDown("Attack") && (isAttack == false))
            {
                isAttack = true;
                ani.SetBool("isAttack", true);
                //Instantiate(SkillPrf[1], skillPos.position, skillPos.rotation);
                Instantiate(AutoBoxPrf, transform.position, transform.rotation);
                MoveSpeed = attackSpeed;
                StartCoroutine(AttackDelay());
            }
        }
        else
        {
            return;
        }
    }

    // ■■■■■■■ 대쉬 ■■■■■■■
    void Dash()
    {
        if (!(ani.GetCurrentAnimatorStateInfo(0).IsName("WallSlide")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Attack")))
        {
            if (Input.GetButtonDown("Dash") && !isDash && (dashTime <= 0))
            {
                
                isDash = true;
                ani.SetTrigger("Dash");
            }

            if (isDash)
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(Vector2.right * dir * dashPower, ForceMode2D.Impulse);
                dashTime = 8f;
                StartCoroutine(DashStop());
            }
        }
    }

    // ■■■■■■■ 대쉬 쿨타임 ■■■■■■■
    void Dash_SkillTime()
    {
        if(dashTime >= 0)
        {
            dashTime -= Time.deltaTime;
            isDash = false;
        }
    }

    // ■■■■■■■ 벽 또는 땅 체크 함수 ■■■■■■■
    void Check()
    {
        // 벽을 타기위한 벽 확인 변수
        if (Physics2D.Raycast(wallCheck.position, Vector2.right * dir, wallchkDistance, w_Layer))
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }

        if (Physics2D.Raycast(new Vector2(groundCheck.position.x - 0.22f, groundCheck.position.y), Vector2.down, groundchkDistance, g_Layer)
           || Physics2D.Raycast(new Vector2(groundCheck.position.x + 0.22f, groundCheck.position.y), Vector2.down, groundchkDistance, g_Layer))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    // ■■■■■■■ 모든 움직임의 예외경우(ex.낙하) ■■■■■■■
    void Exceoption()
    {
        if (isGround == false)
        {
            ani.SetBool("isRun", false);
            ani.SetBool("isGround", false);
            //ani.SetBool("isJump", true);

            if (JumpCnt >= 1)
            {
                ani.SetBool("isJump", true);
            }
            else
            {
                ani.SetBool("isJump", false);
                ani.SetBool("isDoubleJump", true);
            }
        }
    }

    // ■■■■■■■ 플레이어 죽음 ■■■■■■■
    void Die()
    {
        if(hp <= 0 && dieEnd == false)
        {
            ani.SetTrigger("Die");
            dieEnd = true;
        }
    }

    // ■■■■■■■ 바닥 착지 확인 콜리션 ■■■■■■■
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            if(isGround)
            {
                ani.SetBool("isGround", true);   // Idle 상태로 변환
                ani.SetBool("isSlide", false);   // 슬라이딩상태가 아님
                ani.SetBool("isJump", false);
                ani.SetBool("isDoubleJump", false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            JumpCnt = 2;

            // 새로운 오브젝트를 선언해서 생성하지 않으면 프리팹이 삭제가 안됨
            GameObject dust = Instantiate(dustPrf);

            if (spriteRender.flipX == false)
            {
                dust.transform.position = new Vector2(other.otherCollider.transform.position.x - 1f, other.otherCollider.transform.position.y + 0.32f);
            }
            if (spriteRender.flipX == true)
            {
                SpriteRenderer dust_flip;    // 먼지 좌우방향 전환 변수
                dust_flip = dust.GetComponent<SpriteRenderer>();
                dust_flip.flipX = true;
                dust.transform.position = new Vector2(other.otherCollider.transform.position.x + 1f, other.otherCollider.transform.position.y + 0.32f);
            }

            Destroy(dust, 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Knife")
        {
            hp -= 5;
        }
    }

    // ■■■■■■■ 대쉬 정지 ■■■■■■■
    IEnumerator DashStop()
    {
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector2.zero;
    }

    // ■■■■■■■ 공격 후딜 ■■■■■■■
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.2f);
        ani.SetBool("isAttack", false);
        yield return new WaitForSeconds(0.4f);
        MoveSpeed = 5f;
        isAttack = false;
    }

    // ■■■■■■■ 자동 체력 회복 ■■■■■■■
    IEnumerator AutoHp()
    {
        yield return new WaitForSeconds(5f);
        if(hp < 100 && hp>0)
        {
            hp += 1;
        }
        StartCoroutine(AutoHp());
    }

    public IEnumerator ChagePlayerColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    // ■■■■■■■ 기즈모 ■■■■■■■
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallCheck.position, Vector2.right * 0.3f * dir);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(groundCheck.position.x - 0.22f, groundCheck.position.y), Vector2.down * groundchkDistance);
        Gizmos.DrawRay(new Vector2(groundCheck.position.x + 0.22f, groundCheck.position.y), Vector2.down * groundchkDistance);
    }

}
