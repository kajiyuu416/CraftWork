using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Physics2DExtentsion PE;
    public SpriteRenderer originSR;
    public GameObject NowHoldobj;
    public GameObject HoldObj;
    public bool NowHoldItem = false;
    public bool NowMoove;
    public float move_accel = 1f;
    public float move_deccel = 1f;
    public float move_max = 1f;
    public Vector2 moveInputVal;
    public Vector2 CameraInputVal;

    private bool HoldInput;
    private bool ThrowInput;
    private bool ThrowUpInput;
    private bool ThrowDownInput;
    private float side = 1f;
    private Rigidbody2D rigid;
    private Vector2 lookat = Vector2.zero;
    private Vector2 holdItem = Vector2.zero;
    private Vector2 holdItemScale = Vector2.zero;
    private Vector2 move;

    public static PlayerController Instance
    {
        get; private set;
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        rigid = GetComponent<Rigidbody2D>();
        holdItemScale = new Vector2(0.4f, 0.4f);
    }

    public void Update()
    {
        PlayerMove();

        if(moveInputVal.x < 0 || moveInputVal.y < 0)
        {
            NowMoove = true;
        }
        else if(moveInputVal.x > 0 || moveInputVal.y > 0)
        {
            NowMoove = true;
        }else
        {
            NowMoove = false;
        }

    }
    private void FixedUpdate()
    {
        PlayerHoldItem();

        float desiredSpeedX = Mathf.Abs(moveInputVal.x) > 0.1f ? moveInputVal.x * move_max : 0f;
        float accelerationX = Mathf.Abs(moveInputVal.x) > 0.1f ? move_accel : move_deccel;
        move.x = Mathf.MoveTowards(move.x, desiredSpeedX, accelerationX * Time.fixedDeltaTime);
        float desiredSpeedY = Mathf.Abs(moveInputVal.y) > 0.1f ? moveInputVal.y * move_max : 0f;
        float accelerationY = Mathf.Abs(moveInputVal.y) > 0.1f ? move_accel : move_deccel;
        move.y = Mathf.MoveTowards(move.y, desiredSpeedY, accelerationY * Time.fixedDeltaTime);

        HoldInput = false;
        ThrowInput = false;
        ThrowUpInput = false;
        ThrowDownInput = false;

        rigid.velocity = move;
    }
    private void PlayerMove()
    {
        //Update lookat side
        if(move.magnitude > 0.1f)
            lookat = move.normalized;
        if(Mathf.Abs(lookat.x) > 0.02)
            side = Mathf.Sign(lookat.x);
        // ゲームパッド（デバイス取得）
        var gamepad = Gamepad.current;
        if(gamepad == null)
            return;
    }
    private void PlayerHoldItem()
    {
        if(PE.holdFlag && HoldInput && !NowHoldItem)
        {
            NowHoldItem = true;
            NowHoldobj = HoldObj;
            PE.boxCol.enabled = false;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE3();
            Debug.Log("Itemを持ち上げました");
        }
        else if(HoldInput && NowHoldItem)
        {
            Debug.Log("Itemを置きました");
            PE.boxCol.enabled = true;
            NowHoldobj.transform.position = transform.position;
            NowHoldobj.transform.localScale = new Vector2(0.5f, 0.5f);
            ItemLost();
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE4();
        }
        //Itemをつかんだ時にフラグはTrueになっているが実際にオブジェクトが登録されていないからエラーが出ている
        //Todo; errorが出た時の処理またはエラーが出ない方法を探す
        if(NowHoldItem)
        {
            holdItem = transform.position;
            holdItem.x = target.position.x;
            holdItem.y = target.position.y;
            NowHoldobj.transform.position = holdItem;
            NowHoldobj.transform.localScale = holdItemScale;
            if(originSR.flipX)
            {
                NowHoldobj.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(!originSR.flipX)
            {
                NowHoldobj.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

  

        if(ThrowInput && NowHoldItem&& originSR.flipX)
        { 
          Debug.Log("Itemを左側に投げました");
          PE.ItemLost();
          Vector2 force = new Vector2(10.0f, 0);
          Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
          HoldItemRB.AddForce(-force, ForceMode2D.Impulse);
          NowHoldobj.transform.localScale = new Vector2(0.5f, 0.5f);
          ItemLost();
          SoundManager SM = SoundManager.Instance;
          SM.SettingPlaySE5();
        }
        else if(ThrowInput && NowHoldItem && !originSR.flipX)
        {
          Debug.Log("Itemを右側に投げました");
          PE.ItemLost();
          Vector2 force = new Vector2(10.0f, 0);
          Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
          HoldItemRB.AddForce(force, ForceMode2D.Impulse);
          NowHoldobj.transform.localScale = new Vector2(0.5f, 0.5f);
          ItemLost();
          SoundManager SM = SoundManager.Instance;
          SM.SettingPlaySE5();
        }
        else if(ThrowUpInput && NowHoldItem)
        {
            Debug.Log("Itemを上側に投げました");
            PE.ItemLost();
            Vector2 force = new Vector2(0, 10.0f);
            Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
            HoldItemRB.AddForce(force, ForceMode2D.Impulse);
            NowHoldobj.transform.localScale = new Vector2(0.5f, 0.5f);
            ItemLost();
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE5();
        }
        else if(ThrowDownInput && NowHoldItem)
        {
            Debug.Log("Itemを下側に投げました");
            PE.ItemLost();
            Vector2 force = new Vector2(0, 10.0f);
            Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
            HoldItemRB.AddForce(-force, ForceMode2D.Impulse);
            NowHoldobj.transform.localScale = new Vector2(0.5f, 0.5f);
            ItemLost();
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE5();
        }
        HoldObj = PE.HoldtoObj;
    }

    public void ItemLost()
    {
        NowHoldobj = null;
        HoldObj = null;
        NowHoldItem = false;
    }
    public void OnMove(InputValue var)
    {
        moveInputVal = var.Get<Vector2>();
    }
    public void OnCameraMove(InputValue var)
    {
        CameraInputVal = var.Get<Vector2>();
    }

    public void OnHold(InputValue var)
    {
        HoldInput = var.isPressed;
    }

    public void OnThrow(InputValue var)
    {
        ThrowInput = var.isPressed;
    }
    public void OnThrowUp(InputValue var)
    {
        ThrowUpInput = var.isPressed;
    }
    public void OnThrowDown(InputValue var)
    {
        ThrowDownInput = var.isPressed;
    }

    public Vector2 GetMove()
    {
        return move;
    }
    public int GetSideAnim()
    {
        return (side >= 0) ? 1 : 3;
    }
}

