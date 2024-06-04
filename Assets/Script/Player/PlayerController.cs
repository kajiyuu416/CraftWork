using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform target;
    private Physics2DExtentsion PE;
    public SpriteRenderer originSR;
    public GameObject NowHoldobj;
    public GameObject HoldObj;
    public bool NowHoldItem = false;
    public Vector2 moveInputVal;
    public static Vector2 CameraInputVal;
    public static Vector3 CP = new Vector3();
    public static PlayerController Instance
    {
        get; private set;
    }
    private bool connect;
    private const float move_accel = 10.0f;
    private const float move_deccel = 20.0f;
    private const float move_max = 5.0f;
    private float side = 1f;
    private Rigidbody2D rigid;
    private Vector2 lookat = Vector2.zero;
    private Vector2 holdItem = Vector2.zero;
    private Vector2 holdItemScale = new Vector2(0.4f, 0.4f);
    private Vector2 DefaultItemScale = new Vector2(0.5f, 0.5f);
    private Vector2 move;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if(CP != Vector3.zero)
        {
            transform.position = CP;
        }
        PE = FindObjectOfType<Physics2DExtentsion>();
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        PlayerMove();
        GamePad_connection_Check();
        PlayerHoldItem();
        if(GameManager.SelectReSet)
        {
            transform.position = CP;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            GameManager.SelectReSet = false;

        }
    }
    //Player�̓�������A�A�C�e���ێ��̗L�����`�F�b�N
    private void PlayerMove()
    {
        //Reset��ʁABGM�ASE�Z�b�e�B���O��ʂł̓v���C���[�̈ړ��𐧌�
        if(GameManager.ReSetFlag || GameManager.SettingFlag)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        if(!GameManager.ReSetFlag && !GameManager.SettingFlag)
        {
            float desiredSpeedX = Mathf.Abs(moveInputVal.x) > 0.1f ? moveInputVal.x * move_max : 0f;
            float accelerationX = Mathf.Abs(moveInputVal.x) > 0.1f ? move_accel : move_deccel;
            move.x = Mathf.MoveTowards(move.x, desiredSpeedX, accelerationX * Time.fixedDeltaTime);
            float desiredSpeedY = Mathf.Abs(moveInputVal.y) > 0.1f ? moveInputVal.y * move_max : 0f;
            float accelerationY = Mathf.Abs(moveInputVal.y) > 0.1f ? move_accel : move_deccel;
            move.y = Mathf.MoveTowards(move.y, desiredSpeedY, accelerationY * Time.fixedDeltaTime);

            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        rigid.velocity = move;
        if(move.magnitude > 0.1f)
           lookat = move.normalized;
           if(Mathf.Abs(lookat.x) > 0.02)
           side = Mathf.Sign(lookat.x);
           side = Mathf.Sign(lookat.x);

        //�ێ����Ă���A�C�e����Player�̍��W�ֈړ��A�X�P�[���A�����̕ύX
        if(HoldObj == PE.HoldtoObj)
        {
            if(NowHoldItem)
            {
                NowHoldobj = HoldObj;
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
        }
        HoldObj = PE.HoldtoObj;
    }
    //Player���A�C�e���ێ��̃A�N�V��������
    private void PlayerHoldItem()
    {
        if(!GameManager.ReSetFlag && !GameManager.SettingFlag)
        {
            if(connect)
            {
                var current_GP = Gamepad.current;
                var shot = current_GP.rightShoulder;
                var hold = current_GP.buttonEast;
                var Throw_left_right = current_GP.buttonWest;
                var Throw_up = current_GP.buttonNorth;
                var Throw_Down = current_GP.buttonSouth;

                bool isbow = PE.Duplicate_Bow_Hold_Flag;

                //�A�C�e���������グ��
                if(PE.holdFlag && hold.wasPressedThisFrame && !NowHoldItem)
                {
                    NowHoldItem = true;
                    NowHoldobj = HoldObj;
                    PE.boxCol.enabled = false;
                    SoundManager SM = SoundManager.Instance;
                    SM.SettingPlaySE3();
                }
                //�A�C�e��������
                else if(PE.holdFlag && hold.wasPressedThisFrame && NowHoldItem)
                {
                    PE.boxCol.enabled = true;
                    NowHoldobj.transform.position = transform.position;
                    NowHoldobj.transform.localScale = DefaultItemScale;
                    ItemLost();
                    SoundManager SM = SoundManager.Instance;
                    SM.SettingPlaySE4();
                }
                //�A�C�e���������ɓ�����
                if(Throw_left_right.wasPressedThisFrame && NowHoldItem && originSR.flipX)
                {
                    Vector3 force = new Vector2(10.0f, 0);
                    Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
                    HoldItemRB.AddForce(-force, ForceMode2D.Impulse);
                    Itemthrow();

                }
                //�A�C�e�����E���ɓ�����
                else if(Throw_left_right.wasPressedThisFrame && NowHoldItem && !originSR.flipX)
                {
                    Vector2 force = new Vector2(10.0f, 0);
                    Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
                    HoldItemRB.AddForce(force, ForceMode2D.Impulse);
                    Itemthrow();
                }
                //�A�C�e�����㑤�ɓ�����
                else if(Throw_up.wasPressedThisFrame && NowHoldItem)
                {
                    Vector2 force = new Vector2(0, 10.0f);
                    Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
                    HoldItemRB.AddForce(force, ForceMode2D.Impulse);
                    Itemthrow();
                }
                //�A�C�e���������ɓ�����
                else if(Throw_Down.wasPressedThisFrame && NowHoldItem)
                {
                    Vector2 force = new Vector2(0, 10.0f);
                    Rigidbody2D HoldItemRB = NowHoldobj.GetComponent<Rigidbody2D>();
                    HoldItemRB.AddForce(-force, ForceMode2D.Impulse);
                    Itemthrow();
                }
                //�|���������Ă���ꍇ�ˌ����ł��鏈��
                if(isbow && shot.wasPressedThisFrame && NowHoldItem)
                {
                    GameManager GM = GameManager.instance;
                    if(originSR.flipX)
                    {
                        PE.bowSC.LeftShot();
                        GM.Check("arrow");
                    }
                    else if(!originSR.flipX)
                    {
                        PE.bowSC.RighitShot();
                        GM.Check("arrow");
                    }
                }
                if(current_GP == null)
                    return;
            }
        }
    }
    //�`�F�b�N�|�C���g�ʉߎ��̍X�V����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CheckPoint"))
        {
            CP = transform.position;
        }
    }
    //Player���A�C�e���𓊂��閔�͒u�������A�擾�������̃��Z�b�g
    public void ItemLost()
    {
        NowHoldobj = null;
        HoldObj = null;
        NowHoldItem = false;
    }
    private void Itemthrow()
    {
        NowHoldobj.transform.localScale = DefaultItemScale;
        PE.ItemLost();
        ItemLost();
        SoundManager.Instance.SettingPlaySE5();
    }
    //�R���g���[���[���ڑ�����Ă��邪�̃`�F�b�N
    private void GamePad_connection_Check()
    {
        var controllerNames = Input.GetJoystickNames();

        if(controllerNames[0] == "")
        {
            connect = false;
        }
        else
        {
            connect = true;
        }
    }
    public void OnMove(InputValue var)
    {
        moveInputVal = var.Get<Vector2>();
    }
    public void OnCameraMove(InputValue var)
    {
        CameraInputVal = var.Get<Vector2>();
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

