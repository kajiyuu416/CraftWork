using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Physics2DExtentsion : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] Sprite[] sprites;
    public GameObject objctacleRayObject;
    public GameObject HoldtoObj;
    public BoxCollider2D boxCol;
    public LayerMask layerMask;
    public bool holdFlag;
    public BowSC bowSC;
    private  bool Pickaxe_Hold_Flag;
    private bool Torch_Hold_Flag;
    private bool Bow_Hold_Flag;
    private SpriteRenderer Sprite;
    private Sprite ItemSprite;
    public float distance = 5.0f;
    private float CharacterDirection;

    private void Start()
    {
        CharacterDirection = 0f;
    }
    private void Update()
    {
        Hold_Item_Check();
        Flying_ray();
    }
    //Player���A�C�e���𓊂��閔�͒u�������A�擾�������̃��Z�b�g
    public void ItemLost()
    {
        boxCol.enabled = true;
        holdFlag = false;
        HoldtoObj = null;
        ItemSprite = null;
    }
    //Player������̃A�C�e����ێ����Ă��邩�`�F�b�N���A�Y���̏ꍇ�t���O��Ԃ�����
    private void Hold_Item_Check()
    {
        if(PC.moveInputVal !=Vector2.zero)
        {
            PlayerController.SelectReSet = false;
        }

        if(ItemSprite == sprites[0] && !Pickaxe_Hold_Flag)
        {
            Pickaxe_Hold_Flag = true;
        }
        else if(ItemSprite != sprites[0] && Pickaxe_Hold_Flag)
        {
            Pickaxe_Hold_Flag = false;
        }

        if(ItemSprite == sprites[1] && !Torch_Hold_Flag)
        {
            Torch_Hold_Flag = true;
        }
        else if(ItemSprite != sprites[1] && Torch_Hold_Flag)
        {
            Torch_Hold_Flag = false;
        }

        if(ItemSprite == sprites[2] && !Bow_Hold_Flag)
        {
            Bow_Hold_Flag = true;
        }
        else if(ItemSprite != sprites[2] && Bow_Hold_Flag)
        {
            Bow_Hold_Flag = false;
        }

    }

    //Player���ړ������Ƃ�Ray���΂�Ray���A�C�e���Əd�Ȃ��Ă����ꍇ�A�Y���A�C�e���̏����擾���ێ��ł����Ԃɂ��鏈��
    private void Flying_ray()
    {
        if(PC.moveInputVal.x < 0)
        {
            CharacterDirection = -1f;
        }
        else if(PC.moveInputVal.x > 0)
        {
            CharacterDirection = 1;
        }
        else
        {
            CharacterDirection = 0;
        }
        if(PC.moveInputVal !=Vector2.zero&&!PlayerController.ReSetFlag && !PlayerController.SettingFlag)
        {
            RaycastHit2D hitObstacle = Physics2D.Raycast(objctacleRayObject.transform.position, Vector2.right * new Vector2(CharacterDirection, 0f), distance, layerMask);
            if(hitObstacle.collider != null)
            {
                holdFlag = true;
                HoldtoObj = hitObstacle.collider.gameObject;
                boxCol = hitObstacle.collider.GetComponent<BoxCollider2D>();
                Sprite = hitObstacle.collider.GetComponent<SpriteRenderer>();
                ItemSprite = Sprite.sprite;

                if(Bow_Hold_Flag)
                {
                    bowSC = hitObstacle.collider.GetComponent<BowSC>();
                }
                else if(!Bow_Hold_Flag)
                {
                    bowSC = null;
                }
            }
            else
            {
                holdFlag = false;
                ItemSprite = null;
            }
        }
    }

    public bool Duplicate_Pickaxe_Hold_Flag
    {
        get
        {
            return Pickaxe_Hold_Flag;
        }
        set
        {
            Pickaxe_Hold_Flag = value;
        }
    }
    public bool Duplicate_Torch_Hold_Flag
    {
        get
        {
            return Torch_Hold_Flag;
        }
        set
        {
            Torch_Hold_Flag = value;
        }
    }
    public bool Duplicate_Bow_Hold_Flag
    {
        get
        {
            return Bow_Hold_Flag;
        }
        set
        {
            Bow_Hold_Flag = value;
        }
    }
    public bool Duplicate_Hold_Flag
    {
        get
        {
            return holdFlag;
        }
    }


}
