using UnityEngine;
using UnityEngine.UI;

public class AudioSettingSC : MonoBehaviour
{
    public Button AudidoSettingButton;
    void Start()
    {
        AudidoSettingButton = GetComponent<Button>();
        AudidoSettingButton.onClick.AddListener(() => {
            GameManager.SettingAudio();
        });
    }
}
