using UnityEngine;
public class OpendoorSC : Charade
{
    public Vector3 targetPos;

    private void Update()
    {
        movedoor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                HitFlag = true;
                GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(0f, 0f, 0f));
                Destroy(collision.gameObject);
                PlayerController.Instance.ItemLost();
                SoundManager.Instance.SoundPause();
                SoundManager.Instance.SettingPlaySE6();
                SoundManager.Instance.SettingPlaySE7();
                Invoke("SoundUnpause", 5.0f);
            }

        }
    }

    private void movedoor()
    {
        if(HitFlag)
        {
            float speed = 0.5f; // 移動の速度を指定
            Transform objectTransform = gameObject.GetComponent<Transform>(); // ゲームオブジェクトのTransformコンポーネントを取得
            objectTransform.position = Vector3.Lerp(objectTransform.position, targetPos, speed * Time.deltaTime); // 目的の位置に移動
        }
    }
    private void SoundUnpause()
    {
        SoundManager.Instance.SoundUnPause();
    }
}
