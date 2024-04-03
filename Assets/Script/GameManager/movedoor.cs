using UnityEngine;

public class movedoor : MonoBehaviour
{
    public Vector3 targetPos;
    [SerializeField] Charade charade;
    void Update()
    {
        if(charade.HitFlag)
        {
            float speed = 0.5f; // 移動の速度を指定
            Transform objectTransform = gameObject.GetComponent<Transform>(); // ゲームオブジェクトのTransformコンポーネントを取得
            objectTransform.position = Vector3.Lerp(objectTransform.position, targetPos, speed * Time.deltaTime); // 目的の位置に移動
        }
    }
}
