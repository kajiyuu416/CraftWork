using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class clearCheck1 : MonoBehaviour
{
    [SerializeField] List<LeverSC> levers = new List<LeverSC>();
    [SerializeField] List<BoxCollider2D> targetBoxCols = new List<BoxCollider2D>();
    public bool clearFlag;

    //インスペクターで指定したレバーのON、OFFをチェック
    public void LeverCheck()
    {
        clearFlag = true;
        foreach(var li in levers)
        {
            if(li.LeverOn != li.answer)
            {
                clearFlag = false;
            }
            
        }
        if(clearFlag)
        {
            foreach( var bc in targetBoxCols)
            {
                bc.enabled = false;
            }
            SoundManager SM = SoundManager.Instance;
            SM.SoundPause();
            SM.StopSE();
            SM.SettingPlaySE7();
            Invoke("SoundUnpause", 4.0f);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LeverCheck();
        }
    }

    public void SoundUnpause()
    {
        SoundManager SM = SoundManager.Instance;
        SM.SoundUnPause();
    }
}
