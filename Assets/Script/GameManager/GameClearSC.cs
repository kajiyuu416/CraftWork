using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameClearSC : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] Image gold_ring_Image;
    [SerializeField] Image gold_coins_Image;
    [SerializeField] Image gem_diamond_Image;
    [SerializeField] Image gold_crown_Image;
    [SerializeField] Image gold_bars_Image;

    Color defaultColor = new Color(255, 255, 255, 255);
    private void Start()
    {
        text1.text = "Game Clear";
        text2.text = "クリアタイム :";
        text3.text = GameManager.minute.ToString("00") + ":" + ((int) GameManager.seconds).ToString("00");
        text4.text = "集めたお宝";
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE22();

        if(LegacyItemGetCheckSC.GetLI1)
        {
            gold_ring_Image.color = defaultColor;
        }
        if(LegacyItemGetCheckSC.GetLI2)
        {
            gold_coins_Image.color = defaultColor;
        }
        if(LegacyItemGetCheckSC.GetLI3)
        {
            gem_diamond_Image.color = defaultColor;
        }
        if(LegacyItemGetCheckSC.GetLI4)
        {
            gold_crown_Image.color = defaultColor;
        }
        if(LegacyItemGetCheckSC.GetLI5)
        {
            gold_bars_Image.color = defaultColor;
        }
    }
}
