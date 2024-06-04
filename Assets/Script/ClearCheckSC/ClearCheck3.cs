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
            SoundManager.Instance.SoundPause();
            SoundManager.Instance.SettingPlaySE7();
            Invoke("SoundUnpause", 3.5f);
        }
    }
    public void SoundUnpause()
    {
        SoundManager.Instance.SoundUnPause();
    }
}
