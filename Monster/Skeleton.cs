using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************
    speed�� ��� State.run���� �����ϸ� ���������� ������ ���ư�
*****************************/

public class Skeleton : MonoBehaviour
{
    // ������ ���� ������
    enum State { idle, run, attackDelay, attack1, attack2, back, Damage, die }          // ������ ����
    State state = State.idle;

    public Animator ani;             // ������ �ִϸ��̼�
    public CapsuleCollider2D childCap;      // �÷��̾�� �ε�ĥ�� �и��� �ʰ��ϱ� ���� ���� �ڽĿ�����Ʈ
    public GameObject GroundCheck;

    int ranMove;                     // idle ���¿����� ������, ������
    float speed = 2f;                // ������ ������ �ӵ�
    int ran;                         // ���� ����, ������
    bool isAttack = false;           // ���� ���� Ȯ�� ����
    public float hp = 10;            // hp
    bool AllState = true;            // ������ ��ü ���� ���� ����

    Transform player;                // ����(�÷��̾�)
    Vector2 standardPos;             // �ʱ� ��ġ 
    SpriteRenderer sprite;           // ������ �ٶ󺸴� ���� ��ȯ ������Ʈ
    Collider2D[] colls;                // ���� ���� �ڽ� ������Ʈ
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

        // ������ idle ���°� �ƴҶ��� �÷��̾ ��ġ�� �������� �ٶ� ������
        if (!(state == State.idle || state == State.back))
        {
            // ������ �÷��̾ �ִ� �������� ���� flipX���� �ٲ� ������
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
            Debug.Log("����");
            state = State.die;
        }

        if(AllState == true)
        {
            switch (state)
            {
                // ������ idle ������
                case State.idle:
                    //Debug.Log("idle");

                    // ������ ���� AI ���� ������ ������ ������
                    if (ranMove == 1)
                    {
                        //Debug.Log("�����̴���1");
                        transform.Translate(Vector2.right * speed * Time.deltaTime);
                        ani.SetTrigger("Run");
                        sprite.flipX = false;
                    }
                    else if (ranMove == 2)
                    {
                        //Debug.Log("�����̴���2");
                        transform.Translate(Vector2.left * speed * Time.deltaTime);
                        ani.SetTrigger("Run");
                        sprite.flipX = true;
                    }

                    // ������ �÷��̾���� �Ÿ��� 5f �̸��϶� Run ���� ��ȯ ������
                    if (Vector2.Distance(player.position, transform.position) < 5f)
                    {
                        state = State.run;
                        speed = 2;
                        StopAllCoroutines();
                    }

                    break;

                // ������ Run ������
                case State.run:
                    //Debug.Log("Run");
                    ani.SetTrigger("Run");

                    // ������ �÷��̾����� �̵��� ������
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                    // ������ �÷��̾ �ִ� �������� ���� flipX���� �ٲ� ������
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

                // ������ AttackDelay ������
                case State.attackDelay:
                    //Debug.Log("AttackDelay");

                    StartCoroutine(StopAttack());

                    // ������ ���� ������ �������� ������ȯ ������
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
                    // ������ ���� ��� �߿� ��Ÿ��� ����� run���·� ��ȯ ������
                    else if (Vector2.Distance(player.position, transform.position) > 0.8f)
                    {
                        state = State.run;
                        speed = 2;
                        isAttack = false;
                        ran = 0;
                    }
                    break;

                // ������ Attack1 ������
                case State.attack1:
                    //Debug.Log("Attack1");

                    // ������ ���� ���۰� ���ÿ� �ʱ�ȭ ������
                    isAttack = true;
                    speed = 0;
                    ran = 0;

                    // ������ ���� �����Ÿ��ȿ� ������ ���� ���� ������
                    if (Vector2.Distance(player.position, transform.position) <= 0.8f && isAttack == true)
                    {
                        ani.SetBool("Attack1", true);

                        // ������ ���� �ִϸ��̼��� ������� 72%�̻� ����Ǹ� �����·� ��ȯ ������
                        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.77f)
                        {
                            isAttack = false;
                            state = State.attackDelay;
                            ani.SetBool("Attack1", false);
                            StopAllCoroutines();
                        }
                    }
                    // ������ ���� �����Ÿ� ������ �̵��� ������
                    else if (Vector2.Distance(player.position, transform.position) > 0.8f)
                    {
                        // ������ �������� ���� �ִϸ��̼��� ���� �� run���·� �̵� ������
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

                    // ������ ���� ���۰� ���ÿ� �ʱ�ȭ ������
                    isAttack = true;
                    speed = 0;
                    ran = 0;

                    // ������ ���� �����Ÿ��ȿ� ������ ���� ���� ������
                    if (Vector2.Distance(player.position, transform.position) <= 0.8f && isAttack == true)
                    {
                        ani.SetBool("Attack2", true);
                        // ������ ���� �ִϸ��̼��� ������� 72%�̻� ����Ǹ� �����·� ��ȯ ������
                        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack2") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.72f)
                        {
                            isAttack = false;
                            state = State.attackDelay;
                            ani.SetBool("Attack2", false);
                            StopAllCoroutines();
                        }
                    }
                    // ������ ���� �����Ÿ� ������ �̵��� ������
                    else if (Vector2.Distance(player.position, transform.position) > 0.8f)
                    {
                        // ������ �������� ���� �ִϸ��̼��� ���� �� run���·� �̵� ������
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

                // ������ ���� ���� // ������
                case State.back:
                    //Debug.Log("����");

                    // ������ �÷��̾�� �����Ÿ� �̻� �־����� ���� ��ġ�� ���ƿ� ������
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
                        //Debug.Log("���ͿϷ�");
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

    // ������ �������� Ȯ�� �Լ� ������
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

    // ������ �ִϸ��̼ǿ� ���� �÷��̾�� ������� �� ������ (���ݺ� Animation �κп� ������)
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

    // ������ ��ų �Ǵ� �⺻���ݿ� ���� ���ظ� ���� ������
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �÷��̾� ��ų ����
        if (other.gameObject.layer == 9)
        {
            hp -= 10;
            Debug.Log(gameObject.name + " " + hp);
            StartCoroutine(ChangeColor());
        }

        // ���� �÷��̾� �⺻���� ����
        if (other.gameObject.tag == "DamageBox")
        {
            hp -= 3;
            Debug.Log(gameObject.name + " " + hp);
            StartCoroutine(ChangeColor());
        }
    }

    // ������ idle ���� ������ �ݺ� ������
    IEnumerator MoveRandom()
    {
        yield return new WaitForSeconds(1.5f);
        ranMove = Random.Range(1, 3);
        yield return new WaitForSeconds(1.5f);
        ranMove = 0;
        ani.ResetTrigger("Run");            // Run �ִϸ��̼� ���
        ani.SetTrigger("Idle");
        StartCoroutine(MoveRandom());       // ������ �ݺ����(���)
    }

    // ������ ���ݴ�� ���� ������ ������
    IEnumerator StopAttack()
    {
        ani.SetTrigger("AttackDelay");
        yield return new WaitForSeconds(0.7f);  // ���� ������ �ð�
        if (ran == 0)
        {
            ran = Random.Range(1, 3);
        }
    }

    // ������ ���ظ� �Ծ����� �����ð� ���������� ���� ������
    IEnumerator ChangeColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(255, 255, 255, 255);
    }

    // ������ �׾����� ��� ������, ������Ʈ�� ���� ������
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
