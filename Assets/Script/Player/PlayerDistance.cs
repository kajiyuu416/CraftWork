using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    private bool ChangeMusic1;
    private bool ChangeMusic2;

    private void Start()
    {
        ChangeMusic1 = false;
        ChangeMusic2 = false;
        SoundManager SM = SoundManager.Instance;
        SM.StopBGM();
        SM.Startbgm1();
    }
    void Update()
    {
        Vector3 posi = this.transform.localPosition;
        Debug.Log(posi);

        if(posi.y < 150 && !ChangeMusic1)
        {
            ChangeMusic1 = true;
            ChangeMusic2 = false;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm1();
        }

        if(posi.y > 150 &&!ChangeMusic2)
        {
            ChangeMusic2 = true;
            ChangeMusic1 = false;
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm3();
        }

    }
}
