using UnityEngine;
public class clearCheck3 : MonoBehaviour
{
    public string tagname;
    private bool clearFlag = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagname) && !clearFlag)
        {
            clearFlag = true;
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
