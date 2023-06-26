using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// ■■■■■■ 애니메이션 부분 스크립트임 무시하삼 ■■■■■■

public class First_Talk : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public CanvasGroup talkGroup;
    public CanvasGroup playerNameBox;
    public CanvasGroup sisterNameBox;

    public string[] message;

    int cnt;
    public int gameStart;

    bool confirm = false;

    Color alpha;
    public Image blackBG;

    void Start()
    {
        cnt = message.Length;
    }

    void Update()
    {
        if (gameStart == 3)
        {
            StartCoroutine(Test());
        }
        else
        {
            alpha = blackBG.color;

            alpha.a -= Time.deltaTime;

            blackBG.color = alpha;
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    if (collision.gameObject.layer == 3)
    //    {
    //        if (confirm == false)
    //        {
    //            gameStart++;
    //            confirm = true;
    //            StartCoroutine(Delay(gameStart));
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(gameStart);

        

        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "Sister")
        {
            if (confirm == false)
            {
                confirm = true;
                //PlayerAll.All_PlayerMove = false;
                StartCoroutine(Delay());
            }
            else
            {
                return;
            }
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(2f);

        alpha = blackBG.color;

        alpha.a += Time.deltaTime;

        blackBG.color = alpha;
    }

    IEnumerator Delay()
    {
        Debug.Log(gameStart);
        
        if(gameStart == 1 || gameStart == 3)
        {
            playerNameBox.alpha = 1;
            sisterNameBox.alpha = 0;
            yield return new WaitForSeconds(2f);
            //gameStart++;
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }

        if(gameStart == 2)
        {
            playerNameBox.alpha = 0;
            sisterNameBox.alpha = 1;
            yield return new WaitForSeconds(2f);
            gameStart++;
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }
        


        talkGroup.alpha = 1;



        for(int i=0; i < message.Length; i++)
        {
            textUI.text = "";
            foreach (char cnt in message[i].ToCharArray())
            {
                textUI.text += cnt;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);

            if(cnt == message.Length)
            {
                if (textUI.text.Equals(message[cnt-1]))
                {
                    yield return new WaitForSeconds(2f);
                    talkGroup.alpha = 0;
                    //PlayerAll.All_PlayerMove = true;
                }
            }
        }

        
    }
}
