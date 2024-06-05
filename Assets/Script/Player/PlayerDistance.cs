using UnityEngine;
public class PlayerDistance : MonoBehaviour
{
    private bool ChangeMusic1;
    private bool ChangeMusic2;
    private bool ChangeMusic3;
    private bool ChangeMusic4;

    //Playerの座標に応じてBGMを切り替える処理
    private void Start()
    {
        ChangeMusic1 = false;
        ChangeMusic2 = false;
        ChangeMusic3 = false;
        ChangeMusic4 = false;
    }
    private void Update()
    {
        Change_Music();
        
        if(GameManager.SelectReSet)
        {
            ChangeMusic1 = false;
            ChangeMusic2 = false;
            ChangeMusic3 = false;
            ChangeMusic4 = false;
        }
    }
    private void Change_Music()
    {
        Vector3 posi = this.transform.localPosition;
        if(posi.x < 250&& posi.x < 500 && !ChangeMusic1)
        {
            ChangeMusic1 = true;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm1();
        }
        else if(posi.x > 500 && posi.x < 1400 && !ChangeMusic2)
        {
            ChangeMusic2 = true;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm3();
        }
        else if(posi.x > 1400 && posi.x < 1707 && !ChangeMusic3)
        {
            ChangeMusic3 = true;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm4();
        }
        else if(posi.x > 1707 && !ChangeMusic4)
        {
            ChangeMusic4 = true;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm5();
        }
    }

}
