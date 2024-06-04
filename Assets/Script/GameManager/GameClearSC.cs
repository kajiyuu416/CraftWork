using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class GameClearSC : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] Image[] legacyItemImages;
    private Color defaultColor = new Color(255, 255, 255, 255);
    private void Start()
    {
        text1.text = "Game Clear";
        text2.text = "クリアタイム :";
        text3.text = GameManager.minute.ToString("00") + ":" + ((int) GameManager.seconds).ToString("00");
        text4.text = "集めたお宝";
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE22();
        GetCheck();
    }

    public void GetCheck()
    {
        foreach(var FL in LegacyItemGetCheckSC.getLIFlagList)
        {
            if(FL)
            {
                foreach(var lII in legacyItemImages)
                {
                    lII.color = defaultColor;
                }
            }
        }
    }

}
