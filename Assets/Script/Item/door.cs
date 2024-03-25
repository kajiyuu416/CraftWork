using UnityEngine;
public class door : Charade
{
    public Vector3 targetPos;

    void Update()
    {
        if(HitFlag)
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

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(0f, 0f, 0f));
                Destroy(collision.gameObject);
                HitFlag = true;
                PC.NowHoldobj = null;
                PC.NowHoldItem = false; 
                SoundManager SM = SoundManager.Instance;
                SM.SoundPause();
                SM.SettingPlaySE6();
                SM.SettingPlaySE7();
                Invoke("SoundUnpause", 5.0f);
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
