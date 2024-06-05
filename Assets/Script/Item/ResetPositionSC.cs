using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionSC : MonoBehaviour
{
    private Vector3 OriginPosition;
//ゲーム開始時初期位置の記録
    private void Awake()
    {
        OriginPosition = transform.position;
    }
    //リセットフラグが返ったときにオブジェクトの位置を初期位置に戻す
    private void Update()
    {
        if(GameManager.SelectReSet)
        {
            transform.position = OriginPosition;
        }
    }
}
