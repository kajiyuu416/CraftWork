using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetWallSC : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;

    private void Awake()
    {
        if(meshRenderer == null || boxCollider2D == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }
    }
    private void Update()
    {
        if(GameManager.SelectReSet)
        {
            meshRenderer.enabled = true;
            boxCollider2D.enabled = true;

        }
    }
}
