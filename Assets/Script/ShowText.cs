using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] Physics2DExtentsion PE;
    [SerializeField] PlayerController PC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PE.holdFlag &&!PC.NowHoldItem)
        {
            text1.text = "アイテムを持ち上げる ・・・ B ボタン";
        }
        else if(PC.NowHoldItem)
        {
            text1.text = "アイテムを置く　・・・ B ボタン";
            text3.text = "アイテムを下に投げる　・・・ A ボタン";
            text4.text = "アイテムを上に投げる　・・・ Y ボタン";
        }
        else 
        {
            text1.text = "";
        }

        if(PC.NowHoldItem && !PC.originSR.flipX)
        {
            text2.text = "アイテムを右に投げる　・・・ X ボタン";
        }
        else if(PC.NowHoldItem && PC.originSR.flipX)
        {
            text2.text = "アイテムを左に投げる　・・・ X ボタン";
        }

        if(!PC.NowHoldItem)
        {
            text2.text = "";
            text3.text = "";
            text4.text = "";
        }



    }
}
