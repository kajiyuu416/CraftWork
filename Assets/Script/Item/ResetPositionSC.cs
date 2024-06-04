using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionSC : MonoBehaviour
{
    private Vector3 OriginPosition;

    private void Awake()
    {
        OriginPosition = transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        if(GameManager.SelectReSet)
        {
            transform.position = OriginPosition;
        }
    }
}
