using UnityEngine;

public class ClearCheck3 : MonoBehaviour
{
    public string tagname;
    private bool ClearFlag = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag(tagname) && !ClearFlag)
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
