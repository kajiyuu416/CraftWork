using UnityEngine;
public class PlayerDistance : MonoBehaviour
{
    private bool ChangeMusic1;
    private bool ChangeMusic2;
    private bool ChangeMusic3;
    private bool ChangeMusic4;

    //Player‚ÌÀ•W‚É‰‚¶‚ÄBGM‚ğØ‚è‘Ö‚¦‚éˆ—
    private void Start()
    {
        ChangeMusic1 = false;
        ChangeMusic2 = false;
        ChangeMusic3 = false;
        ChangeMusic4 = false;
    }
    void Update()
    {
        Change_Music();
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
        else if(posi.x > 1400 && !ChangeMusic3)
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
