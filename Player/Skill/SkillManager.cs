using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // �������� ���� ��������

    // �������� ��ų ���� ��������
    [Header("-- Skill --")]
    public Animator ani;                // ��ų�� ���� ĳ���� ���
    public Transform skillPos;          // ��ų ���� ��ġ
    public GameObject[] SkillPrf;       // ����Ʈ ������

    public PlayerAll player;

    bool[] coolTime = new bool[6];

    void Awake()
    {
        var obj = FindObjectsOfType<SkillManager>();
        if(obj.Length == 1)
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
        player = GameObject.Find("Player").GetComponent<PlayerAll>();
        for(int i=0; i<coolTime.Length; i++)
        {
            coolTime[i] = false;
        }
    }

    void Update()
    {
        if (NowAni())
        {
            Skill_DragonFire();
            Skill_Tornado();
            Skill_Stone();
            SubSkill_Shield();
            SubSkill_Light();
            SubSkill_Arrow();
        }
    }

    // �������� ��Ÿ��, �뽬, ��ų�� �������̸� ��ų ��� �Ұ� ��������
    bool NowAni()
    {
        if(!(ani.GetCurrentAnimatorStateInfo(0).IsName("WallSlide")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Dash")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Skill1")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Skill2")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("Skill3")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("LightCast")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("ArrowCast")
            || ani.GetCurrentAnimatorStateInfo(0).IsName("ShieldCast")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // �������� ��ų1(��) ��������
    void Skill_DragonFire()
    {
        if (Input.GetButtonDown("Skill1") && coolTime[0] == false)
        {
            ani.SetTrigger("SkillDragon");
            coolTime[0] = true;
            /* Instantiate�Ҷ� ���ڰ��� 2��(������Ʈ, ��ġ) ���ϸ� �߻簡 �̻����� */
            Instantiate(SkillPrf[0], skillPos.position, skillPos.rotation);
            StartCoroutine(CoolTimeDelete(0, 7));
        }
    }

    // �������� ��ų2(����̵�) ��������
    void Skill_Tornado()
    {
        if (Input.GetButtonDown("Skill2") && coolTime[1] == false)
        {
            ani.SetTrigger("SkillTornado");

            SkillTornado.isTornado = true;
            coolTime[1] = true;
            StartCoroutine(DeleteTime());
            StartCoroutine(CoolTimeDelete(1, 5));
        }
    }

    // �������� ��ų3(����) ��������
    void Skill_Stone()
    {
        if (player.isGround == true)
        {
            if (Input.GetButtonDown("Skill3") && coolTime[2] == false)
            {
                ani.SetTrigger("SkillStone");
                coolTime[2] = true;
                Instantiate(SkillPrf[4], skillPos.position, skillPos.rotation);
                StartCoroutine(CoolTimeDelete(2, 10));
            }
        }
    }

    // �������� ���� ��ų1(����Ʈ) ��������
    void SubSkill_Light()
    {
        if (Input.GetButtonDown("SubSkill1") && coolTime[3] == false)
        {
            ani.SetTrigger("LightCast");

            /* Instantiate�Ҷ� ���ڰ��� 2��(������Ʈ, ��ġ) ���ϸ� �߻簡 �̻����� */
            Instantiate(SkillPrf[2], skillPos.position, skillPos.rotation);
            coolTime[3] = true;
            StartCoroutine(CoolTimeDelete(3, 5));
        }
    }

    // �������� ���� ��ų2(���ο�) ��������
    void SubSkill_Arrow()
    {
        if (Input.GetButtonDown("SubSkill2") && coolTime[4] == false)
        {
            ani.SetTrigger("ArrowCast");

            Instantiate(SkillPrf[3], skillPos.position, skillPos.rotation);
            coolTime[4] = true;
            StartCoroutine(CoolTimeDelete(4, 6));
        }
    }


    // �������� ���� ��ų3(����) ��������
    void SubSkill_Shield()
    {
        if(Input.GetButtonDown("SubSkill3") && coolTime[5] == false)
        {
            ani.SetTrigger("ShieldCast");
            coolTime[5] = true;
            Instantiate(SkillPrf[1], skillPos.position, skillPos.rotation);
            StartCoroutine(CoolTimeDelete(5, 8));
        }
    }

    IEnumerator DeleteTime()
    {
        yield return new WaitForSeconds(1f);
        SkillTornado.isTornado = false;
    }

    // �������� ��� ��ų ��Ÿ�� �ڷ�ƾ ��������
    IEnumerator CoolTimeDelete(int i, int time)
    {
        yield return new WaitForSeconds(time);
        coolTime[i] = false;
    }


}
