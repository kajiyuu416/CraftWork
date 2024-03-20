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
            float speed = 0.5f; // �ړ��̑��x���w��
            Transform objectTransform = gameObject.GetComponent<Transform>(); // �Q�[���I�u�W�F�N�g��Transform�R���|�[�l���g���擾
            objectTransform.position = Vector3.Lerp(objectTransform.position, targetPos, speed * Time.deltaTime); // �ړI�̈ʒu�Ɉړ�
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
                Debug.Log("�Ώۂ�Item�ƐڐG���܂���");
            }
        }
    }
    public void SoundUnpause()
    {
        SoundManager SM = SoundManager.Instance;
        SM.SoundUnPause();
    }
}
