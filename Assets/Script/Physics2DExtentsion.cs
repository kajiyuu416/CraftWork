using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Physics2DExtentsion : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] PlayerController PC;
    public GameObject HoldtoObj;
    public GameObject objctacleRayObject;
    public LayerMask layerMask;
    public BoxCollider2D boxCol;
    public bool holdFlag;
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

     void Update()
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
        if(PC.NowMoove)
        {
            RaycastHit2D hitObstacle = Physics2D.Raycast(objctacleRayObject.transform.position, Vector2.right * new Vector2(CharacterDirection, 0f), distance, layerMask);

            if(hitObstacle.collider != null)
            {
                holdFlag = true;
                HoldtoObj = hitObstacle.collider.gameObject;
                boxCol = hitObstacle.collider.GetComponent<BoxCollider2D>();
                Debug.DrawRay(objctacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(CharacterDirection, 0f), Color.red);
                Debug.Log("ItemÇèEÇ§Ç±Ç∆Ç™Ç≈Ç´Ç‹Ç∑");
            }
            else
            {
                holdFlag = false;
                Debug.DrawRay(objctacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(CharacterDirection, 0f), Color.green);
                Debug.Log("èEÇ§ItemÇ™ë∂ç›ÇµÇ‹ÇπÇÒ");
            }
        }
    

    }
}
