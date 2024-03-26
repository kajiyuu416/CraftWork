using UnityEngine;

public class ClearCheck3 : MonoBehaviour
{
    private bool ClearFlag = false;
    [SerializeField] GameObject teleportObj1;
    [SerializeField] GameObject teleportObj2;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Item") && !ClearFlag)
        {
            ClearFlag = true;
            teleportObj1.SetActive(true);
            teleportObj2.SetActive(true);
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
