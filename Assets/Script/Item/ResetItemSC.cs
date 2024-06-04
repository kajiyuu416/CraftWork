using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItemSC : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D[] boxCollider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponents<BoxCollider2D>();
    }
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
