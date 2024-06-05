using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItemSC : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D[] boxCollider2D;
    //ゲーム開始時取得
    private void Awake()
    {
        if(spriteRenderer == null || boxCollider2D == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider2D = GetComponents<BoxCollider2D>();
        }
    }
    //リセットフラグが返ったときスプライトとコライダーの表示を行う
    private void Update()
    {
        if(GameManager.SelectReSet)
        {
            spriteRenderer.enabled = true;
            boxCollider2D[0].enabled = true;
            boxCollider2D[1].enabled = true;

        }
    }
}
