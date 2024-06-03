using UnityEngine;
using TMPro;
public class ShowText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] TextMeshProUGUI text5;
    [SerializeField] TextMeshProUGUI text6;
    [SerializeField] TextMeshProUGUI text7;
    [SerializeField] TextMeshProUGUI text8;
    [SerializeField] TextMeshProUGUI text9;
    private Physics2DExtentsion PE;
    [SerializeField] PlayerController PC;

    //フラグに応じてテキスト表示
    private void Update()
    {
        Expression_Text();
    }
    private void Awake()
    {
        PE = FindObjectOfType<Physics2DExtentsion>();
    }
    private void Expression_Text()
    {
        bool isholdFlag = PE.Duplicate_Hold_Flag;
        bool ispickaxe = PE.Duplicate_Pickaxe_Hold_Flag;
        bool istorch = PE.Duplicate_Torch_Hold_Flag;
        bool isbow = PE.Duplicate_Bow_Hold_Flag;

        if(isholdFlag && !PC.NowHoldItem)
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

        if(ispickaxe)
        {
            pickaxeSC picSC = pickaxeSC.Instance;
            text6.text = picSC.Use_Pickaxe_Count.ToString();
            text5.text = "ピッケルの耐久値・・・";
        }
        else if(!ispickaxe)
        {
            text5.text = "";
            text6.text = "";
        }
        if(istorch)
        {
            TorchSC torchSC = TorchSC.Instance;
            if(torchSC.TorchLight.range < 10.0f)
            {
                text7.text = "トーチの火が消えそうだ";
            }

            if(torchSC.TorchLight.range == 0)
            {
                text7.text = "";
            }

            if(torchSC.burnFlag)
            {
                text7.text = "";
            }


        }
        else if(!istorch)
        {
            text7.text = "";
        }

        if(isbow)
        {
            text8.text = "残数ゲージ";
            text9.text = "射撃　・・・ R1 ボタン";
        }
        else
        {
            text8.text = "";
            text9.text = "";
        }

    }
}
