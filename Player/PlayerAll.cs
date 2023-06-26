using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAll : MonoBehaviour
{
    // �������� ���� ��������
    Rigidbody2D rigid;
    Animator ani;
    public static SpriteRenderer spriteRender;

    // �÷��̾� ��ü���� ������ ����
    static public bool All_PlayerMove = true;

    // ������ ���� ����
    [Header ("-- Move --")]
    public float MoveSpeed = 5f;
    public float JumpPower = 6.5f;
    public float dir;

    // ���� ����
    int JumpCnt = 2;

    // ��Ÿ�� ���� ����(��)
    [Header ("-- Wall --")]
    public LayerMask w_Layer;
    public Transform wallCheck;
    public float wallchkDistance;
    public bool isWall;
    float slidingSpeed = 0.5f;

    // �� ���� ����
    [Header("-- Ground --")]
    public LayerMask g_Layer;
    public Transform groundCheck;
    public float groundchkDistance;
    public bool isGround = false;

    // ���� ���� ����
    public GameObject AutoBoxPrf;
    //DamageBox Dam_Box;
    float attackSpeed = 2f;
    public Transform AutoBoxPos;
    bool isAttack = false;

    // �뽬 ���� ����
    [Header("-- AutoAttack --")]
    float dashPower = 15f;
    bool isDash = false;
    float dashTime;

    // ����
    [Header("-- Smoke --")]
    public GameObject dustPrf;

    // �÷��̾� ü��
    [Header("-- HP --")]
    public int hp;
    bool dieEnd = false;


    // �������� �̱��� ��������
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

            Dash_SkillTime();         // ��� ��ų ��Ÿ�� ���� �Լ�
            Die();
        }
    }

    void FixedUpdate()
    {
        WallSlide();
        Move();
    }

    // �������� ������ ��������
    void Move()
    {
        /* FixedUpdate ���� �����ϰ� �����ν� �� �浹�� ���ϰ���
           ������ٵ�� ����ϸ� �̲������� �߻��Ͽ� Translate�� ���� */
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

    // �������� ���� ��������
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

    // �������� �� Ÿ�� ��������
    void WallSlide()
    {
        // �����̵� ���� (���� �پ������鼭 ���� ������ ���� ������ �����̵�)
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

    // �������� ĳ���� ���� ��������
    void Attack()
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            /* ���� Ŭ������ ����� ��ġ�°� ����, ���ݸ�� 1���� ���� 1�� ���� */
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

    // �������� �뽬 ��������
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

    // �������� �뽬 ��Ÿ�� ��������
    void Dash_SkillTime()
    {
        if(dashTime >= 0)
        {
            dashTime -= Time.deltaTime;
            isDash = false;
        }
    }

    // �������� �� �Ǵ� �� üũ �Լ� ��������
    void Check()
    {
        // ���� Ÿ������ �� Ȯ�� ����
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

    // �������� ��� �������� ���ܰ��(ex.����) ��������
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

    // �������� �÷��̾� ���� ��������
    void Die()
    {
        if(hp <= 0 && dieEnd == false)
        {
            ani.SetTrigger("Die");
            dieEnd = true;
        }
    }

    // �������� �ٴ� ���� Ȯ�� �ݸ��� ��������
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            if(isGround)
            {
                ani.SetBool("isGround", true);   // Idle ���·� ��ȯ
                ani.SetBool("isSlide", false);   // �����̵����°� �ƴ�
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

            // ���ο� ������Ʈ�� �����ؼ� �������� ������ �������� ������ �ȵ�
            GameObject dust = Instantiate(dustPrf);

            if (spriteRender.flipX == false)
            {
                dust.transform.position = new Vector2(other.otherCollider.transform.position.x - 1f, other.otherCollider.transform.position.y + 0.32f);
            }
            if (spriteRender.flipX == true)
            {
                SpriteRenderer dust_flip;    // ���� �¿���� ��ȯ ����
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

    // �������� �뽬 ���� ��������
    IEnumerator DashStop()
    {
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector2.zero;
    }

    // �������� ���� �ĵ� ��������
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.2f);
        ani.SetBool("isAttack", false);
        yield return new WaitForSeconds(0.4f);
        MoveSpeed = 5f;
        isAttack = false;
    }

    // �������� �ڵ� ü�� ȸ�� ��������
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

    // �������� ����� ��������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallCheck.position, Vector2.right * 0.3f * dir);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(groundCheck.position.x - 0.22f, groundCheck.position.y), Vector2.down * groundchkDistance);
        Gizmos.DrawRay(new Vector2(groundCheck.position.x + 0.22f, groundCheck.position.y), Vector2.down * groundchkDistance);
    }

}
