using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public class clearCheck4 : MonoBehaviour
{
    [SerializeField] CandleSC[] Charades;
    [SerializeField] GameObject targetObj;
    [SerializeField] Vector3 targetPos;
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField] CinemachineVirtualCamera camera2;
    private bool clearFlag = false;
    private bool FilstClear;
    private void Update()
    {
        if (clearFlag && !FilstClear)
        {
            FilstClear = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE7();
            camera2.Priority = 11;
            StartCoroutine("ChangeCamera");
        }

        if(clearFlag)
        {
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

    public void ClearCheck()
    {
        clearFlag = true;
        foreach(var ch in Charades)
        {
            //キャンドルにトーチが接触したとき、ヒットフラグを返す。
            //事前にインスペクター上で、セットしたanswerと一致していればclealFlagがtrueになる 
            //Charade1,4,6,8,9,10,12がfalse
            //Charade2,3,5,7,11 がtrue
            if(ch.HitFlag != ch.answer)
            {
                clearFlag = false;
                Debug.Log(ch);
            }
            Debug.Log("オブジェクト名" + ch.name);
            Debug.Log("フラグ" + ch.HitFlag);

        }
    }

}
