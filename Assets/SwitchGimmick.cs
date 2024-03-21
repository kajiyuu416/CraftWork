using UnityEngine;
public class SwitchGimmick : MonoBehaviour
{
    [SerializeField] LeverSC lever1SC;
    [SerializeField] LeverSC lever2SC;
    [SerializeField] LeverSC lever3SC;
    [SerializeField] BoxCollider2D targetBoxCol1;
    [SerializeField] BoxCollider2D targetBoxCol2;
    [SerializeField] BoxCollider2D targetBoxCol3;
    public bool SGFlag;
    // Update is called once per frame

    private void Awake()
    {
        
    }
    void Update()
    {
        if(lever1SC.LeverOn&&lever2SC.LeverOff &&lever3SC.LeverOn && !SGFlag)
        {
            SGFlag = true;
            targetBoxCol1.enabled = false;
            targetBoxCol2.enabled = false;
            targetBoxCol3.enabled = false;
            SoundManager SM = SoundManager.Instance;
            SM.SoundPause();
            SM.StopSE();
            SM.SettingPlaySE7();
            Invoke("SoundUnpause", 4.0f);
        }
    }

    public void SoundUnpause()
    {
        SoundManager SM = SoundManager.Instance;
        SM.SoundUnPause();
    }
}
