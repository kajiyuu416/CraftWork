using UnityEngine;
using UnityEngine.Assertions.Must;

public class LeverSC : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    public bool LeverOn = false;
    public bool LeverOff = true;
    private SpriteRenderer originSprite;
    public Vector3 On_Pos;
    public Vector3 Off_Pos;

    private void Awake()
    {
        originSprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player") && LeverOff)
        {
            LeverOn = true;
            LeverOff = false;
            originSprite.flipX = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE8();
        }
        else if(collision.CompareTag("Player")&& LeverOn)
        {
            LeverOff = true;
            LeverOn = false;
            originSprite.flipX = false;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE8();
        }

    }
    private void Update()
    {
        if(LeverOn)
        {
            float speed = 1.0f; // 移動の速度を指定
            Transform objectTransform = targetObj.gameObject.GetComponent<Transform>(); 
            objectTransform.position = Vector3.Lerp(objectTransform.position, On_Pos, speed * Time.deltaTime);

        }
        if(LeverOff)
        {
            float speed = 1.0f; // 移動の速度を指定
            Transform objectTransform = targetObj.gameObject.GetComponent<Transform>(); 
            objectTransform.position = Vector3.Lerp(objectTransform.position, Off_Pos, speed * Time.deltaTime);

        }
    }

}
