using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image hpBar;
    public Text curHpText;
    float hp;

    void Update()
    {
        hp = PlayerAll.instance.hp;

        hpBar.fillAmount = hp / (float)100;

        if(hp >=0)
        {
            curHpText.text = hp + " / 100";
        }
        else
        {
            curHpText.text = "0 / 100";
        }
        
    }
}
