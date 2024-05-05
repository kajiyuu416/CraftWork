using UnityEngine;
using TMPro;
public class GameClearSC : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    private void Start()
    {
        text1.text = "Game Clear";
        text2.text = "クリアタイム :";
        text3.text = GameManager.minute.ToString("00") + ":" + ((int) GameManager.seconds).ToString("00");
        text4.text = "集めたお宝";
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE22();
    }
}
