using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Physics2DExtentsion : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] float distance;
    [SerializeField] Sprite PickaxeSprite;
    [SerializeField] Sprite TorchSprite;
    [SerializeField] Sprite BowSprite;
    public BowSC bowSC;
    public GameObject objctacleRayObject;
    public GameObject HoldtoObj;
    public BoxCollider2D boxCol;
    public LayerMask layerMask;
    public bool holdFlag;
    public bool Pickaxe_Hold_Flag;
    public bool Torch_Hold_Flag;
    public bool Bow_Hold_Flag;
    private SpriteRenderer Sprite;
    private Sprite ItemSprite;
    private float CharacterDirection;
    private Vector2 MI;

    private void Awake()
    {
        MI = PC.moveInputVal;
    }
    private void Start()
    {
        CharacterDirection = 0f;
    }
    private void Update()
    {
        ItemHold();
        Flying_ray();
    }
    //Playerがアイテムを投げる又は置いた時、取得した情報のリセット
    public void ItemLost()
    {
        boxCol.enabled = true;
        holdFlag = false;
        HoldtoObj = null;
        ItemSprite = null;
    }
    //Playerが特定のアイテムを保持しているかチェックし、該当の場合フラグを返す処理
    private void ItemHold()
    {
        if(holdFlag)
        {
            PlayerController.SelectReSet = false;
        }

        if(ItemSprite == PickaxeSprite && !Pickaxe_Hold_Flag)
        {
            Pickaxe_Hold_Flag = true;
        }
        else if(ItemSprite != PickaxeSprite && Pickaxe_Hold_Flag)
        {
            Pickaxe_Hold_Flag = false;
        }

        if(ItemSprite == TorchSprite && !Torch_Hold_Flag)
        {
            Torch_Hold_Flag = true;
        }
        else if(ItemSprite != TorchSprite && Torch_Hold_Flag)
        {
            Torch_Hold_Flag = false;
        }

        if(ItemSprite == BowSprite && !Bow_Hold_Flag)
        {
            Bow_Hold_Flag = true;
    
        }
        else if(ItemSprite != BowSprite && Bow_Hold_Flag)
        {
            Bow_Hold_Flag = false;
        }

    }
    //Playerが移動したときRayを飛ばしRayがアイテムと重なっていた場合、該当アイテムの情報を取得し保持できる状態にする処理
    private void Flying_ray()
    {
        if(MI.x < 0)
        {
            CharacterDirection = -1f;
        }
        else if(MI.x > 0)
        {
            CharacterDirection = 1;
        }
        else
        {
            CharacterDirection = 0;
        }
        if(PC.NowMove &&!PlayerController.ReSetFlag && !PlayerController.SettingFlag)
        {
            RaycastHit2D hitObstacle = Physics2D.Raycast(objctacleRayObject.transform.position, Vector2.right * new Vector2(CharacterDirection, 0f), distance, layerMask);

            if(hitObstacle.collider != null)
            {
                holdFlag = true;
                HoldtoObj = hitObstacle.collider.gameObject;
                boxCol = hitObstacle.collider.GetComponent<BoxCollider2D>();
                Sprite = hitObstacle.collider.GetComponent<SpriteRenderer>();
                ItemSprite = Sprite.sprite;
                Debug.DrawRay(objctacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(CharacterDirection, 0f), Color.red);
                Debug.Log("Itemを拾うことができます");
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
                Debug.DrawRay(objctacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(CharacterDirection, 0f), Color.green);
                Debug.Log("拾うItemが存在しません");
            }
        }
    }

}
