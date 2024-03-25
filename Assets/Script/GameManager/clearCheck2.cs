using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCheck2 : MonoBehaviour
{
    [SerializeField] Charade charade;
    [SerializeField] GameObject teleportObj1;
    [SerializeField] GameObject teleportObj2;
    private bool ClearFlag;

    void Update()
    {
        if(charade.HitFlag && !ClearFlag)
        {
            ClearFlag = true;
            teleportObj1.SetActive(true);
            teleportObj2.SetActive(true);
        }
    }
}
