using UnityEngine;
public class door : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] Sprite targetObjSprite;
    [SerializeField] Sprite hitObjSprite;
    [SerializeField] BoxCollider2D boxCol;
    private SpriteRenderer hitObjSpriteRenderer;
    private bool OpenFlag = false;
    public Vector3 targetPos;

    void Update()
    {
        if(OpenFlag)
        {
            float speed = 0.5f; // 移動の速度を指定
            Transform objectTransform = gameObject.GetComponent<Transform>(); // ゲームオブジェクトのTransformコンポーネントを取得
            objectTransform.position = Vector3.Lerp(objectTransform.position, targetPos, speed * Time.deltaTime); // 目的の位置に移動
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !OpenFlag)
            {
                Destroy(collision.gameObject);
                boxCol.enabled = false;
                OpenFlag = true;
                PC.NowHoldobj = null;
                PC.NowHoldItem = false; 
                SoundManager SM = SoundManager.Instance;
                SM.SoundPause();
                SM.SettingPlaySE6();
                SM.SettingPlaySE7();
                Invoke("SoundUnpause", 6.0f);
                Debug.Log("対象のItemと接触しました");
            }
        }
    }
    public void SoundUnpause()
    {
        SoundManager SM = SoundManager.Instance;
        SM.SoundUnPause();
    }
}
