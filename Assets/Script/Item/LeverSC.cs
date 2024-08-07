using UnityEngine;
using UnityEngine.Assertions.Must;
public class LeverSC : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    [SerializeField] clearCheck1 clearCheck1;
    public bool LeverOn = false;
    public bool LeverOff = true;
    public bool answer;
    private SpriteRenderer originSprite;
    public Vector3 On_Pos;
    public Vector3 Off_Pos;
    public enum SelectNum{one,two,tree,four,five}
    public SelectNum selectNumber;

    private void Awake()
    {
        originSprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        obj_migration();
    }

    //インスペクターでナンバーを決めナンバーのよって動作が変更する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(selectNumber == SelectNum.one || selectNumber == SelectNum.two)
        {
            if(LeverOff)
            {
                if(collision.CompareTag("Player") || collision.CompareTag("arrow"))
                {
                    Lever_On();
                }
            }
            else if(LeverOn)
            {
                if(collision.CompareTag("Player") || collision.CompareTag("arrow"))
                {
                    Lever_Off();
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(selectNumber == SelectNum.tree)
        {
            if(LeverOff)
            {
                if(collision.CompareTag("Player") || collision.CompareTag("arrow"))
                {
                    Lever_On();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(selectNumber == SelectNum.tree)
        {
            if(LeverOn)
            {
                if(collision.CompareTag("Player") || collision.CompareTag("arrow"))
                {
                    Lever_Off();
                }
            }
        }
    }
    private void obj_migration()
    {
        if(selectNumber == SelectNum.one || selectNumber == SelectNum.tree)
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

        if(GameManager.SelectReSet && LeverOn)
        {
            Lever_Off();
        }
    }
    private void Lever_On()
    {
        LeverOn = true;
        LeverOff = false;
        originSprite.flipX = true;
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE8();
        if(clearCheck1 != null && answer)
        {
            clearCheck1.LeverCheck();
        }
    }
    private void Lever_Off()
    {
        LeverOff = true;
        LeverOn = false;
        originSprite.flipX = false;
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE8();
        if(clearCheck1 != null &&!answer)
        {
            clearCheck1.LeverCheck();
        }
    }

}
