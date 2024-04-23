using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    private bool ChangeMusic1;
    private bool ChangeMusic2;
    private bool ChangeMusic3;

    private void Start()
    {
        ChangeMusic1 = false;
        ChangeMusic2 = false;
        ChangeMusic3 = false;
        SoundManager SM = SoundManager.Instance;
        SM.StopBGM();
        SM.Startbgm1();
    }
    void Update()
    {
        Change_Music();

    }
    private void Change_Music()
    {
        Vector3 posi = this.transform.localPosition;
        // Debug.Log(posi);
        if(posi.x < 250&& !ChangeMusic1)
        {
            ChangeMusic1 = true;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm1();
            Debug.Log("music1");
        }
        else if(posi.x > 500 && !ChangeMusic2)
        {
            ChangeMusic2 = true;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm3();
            Debug.Log("music2");
        }
        else if(posi.x > 1400 && !ChangeMusic3)
        {
            ChangeMusic3 = true;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm4();
            Debug.Log("music3");
        }
    }

}
