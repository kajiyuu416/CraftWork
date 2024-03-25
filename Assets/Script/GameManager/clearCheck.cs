using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCheck : MonoBehaviour
{
    [SerializeField] Charade charade1;
    [SerializeField] Charade charade2;
    private bool ClearFlag;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(charade1.HitFlag || charade2.HitFlag)
        {
            if(!ClearFlag)
            {
                ClearFlag = true;
                SoundManager SM = SoundManager.Instance;
                SM.SoundPause();
                SM.SettingPlaySE7();
                Invoke("SoundUnpause", 3.5f);
            }
        }
    }
    public void SoundUnpause()
    {
        SoundManager SM = SoundManager.Instance;
        SM.SoundUnPause();
    }
}
