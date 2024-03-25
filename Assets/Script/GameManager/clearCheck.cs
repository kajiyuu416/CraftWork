using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCheck : MonoBehaviour
{
    [SerializeField] Charade charade;
    private bool ClearFlag;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(charade.HitFlag && !ClearFlag)
        {
                ClearFlag = true;
                SoundManager SM = SoundManager.Instance;
                SM.SoundPause();
                SM.SettingPlaySE7();
                Invoke("SoundUnpause", 3.5f);
        }
    }
    public void SoundUnpause()
    {
        SoundManager SM = SoundManager.Instance;
        SM.SoundUnPause();
    }
}
