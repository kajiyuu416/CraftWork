using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public class clearCheck4 : MonoBehaviour
{
    [SerializeField] Charade charade1;
    [SerializeField] Charade charade2;
    [SerializeField] Charade charade3;
    [SerializeField] Charade charade4;
    [SerializeField] Charade charade5;
    [SerializeField] Charade charade6;
    [SerializeField] Charade charade7;
    [SerializeField] Charade charade8;
    [SerializeField] Charade charade9;
    [SerializeField] Charade charade10;
    [SerializeField] Charade charade11;
    [SerializeField] Charade charade12;
    [SerializeField] GameObject targetObj;
    [SerializeField] Vector3 targetPos;
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField] CinemachineVirtualCamera camera2;
    public static bool clealFlag = false;
    private static bool FilstCleal;
    private void Update()
    {
        if(!charade1.HitFlag&& !charade4.HitFlag && !charade6.HitFlag && !charade8.HitFlag && !charade9.HitFlag && !charade10.HitFlag && !charade12.HitFlag)
        {
            if(charade2.HitFlag && charade3.HitFlag && charade5.HitFlag && charade7.HitFlag && charade11.HitFlag)
            {
                clealFlag = true;
            }
        }

        if(clealFlag && !FilstCleal)
        {
            FilstCleal = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE7();
            camera2.Priority = 11;
            StartCoroutine("ChangeCamera");
        }

        if(clealFlag)
        {
            charade2.HitFlag = true;
            charade3.HitFlag = true;
            charade5.HitFlag = true;
            charade7.HitFlag = true;
            charade11.HitFlag = true;
            float speed = 0.75f; // 移動の速度を指定
            Transform objectTransform = targetObj.GetComponent<Transform>(); // ゲームオブジェクトのTransformコンポーネントを取得
            objectTransform.position = Vector3.Lerp(objectTransform.position, targetPos, speed * Time.deltaTime); // 目的の位置に移動
        }
    }
    IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(3.0f);
        camera2.Priority = 9;
    }
}
