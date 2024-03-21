using UnityEngine;

public class TeleportSC : MonoBehaviour
{
    [SerializeField] Transform TeleportPos;
    private Transform targetPos;
    private bool hitFlag =false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hitFlag)
        {
            targetPos = collision.gameObject.GetComponent<Transform>();
            targetPos.transform.position = TeleportPos.position;
            hitFlag = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE10();
        }
    }
}
