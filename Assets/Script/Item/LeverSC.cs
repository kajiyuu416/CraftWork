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
    public enum SelectNum{one,two,tree,four,five}
    public SelectNum selectNumber;

    private void Awake()
    {
        originSprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& LeverOff)
        {
            Lever_On();
        }
        else if(collision.CompareTag("Player")&& LeverOn)
        {
            Lever_Off();
        }

        if(collision.CompareTag("arrow") && LeverOff)
        {
            Lever_On();
        }
        else if(collision.CompareTag("arrow") && LeverOn)
        {
            Lever_Off();
        }
    }
    private void Update()
    {
        obj_migration();
    }
    private void obj_migration()
    {
        if(selectNumber == SelectNum.one)
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
        else if(selectNumber == SelectNum.two)
        {
            if(LeverOn)
            {
                float speed = 1.0f; // 移動の速度を指定
                Transform objectTransform = targetObj.gameObject.GetComponent<Transform>();
                objectTransform.transform.eulerAngles = Vector3.Lerp(objectTransform.eulerAngles, On_Pos, speed * Time.deltaTime);
            }
            if(LeverOff)
            {
                float speed = 1.0f; // 移動の速度を指定
                Transform objectTransform = targetObj.gameObject.GetComponent<Transform>();
                objectTransform.transform.eulerAngles = Vector3.Lerp(objectTransform.eulerAngles, Off_Pos, speed * Time.deltaTime);
            }
        }
    }
    private void Lever_On()
    {
        LeverOn = true;
        LeverOff = false;
        originSprite.flipX = true;
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE8();
    }
    private void Lever_Off()
    {
        LeverOff = true;
        LeverOn = false;
        originSprite.flipX = false;
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE8();
    }

}
