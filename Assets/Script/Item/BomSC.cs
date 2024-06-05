using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomSC : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject ignitionEffect;
    [SerializeField] BoxCollider2D boxCol1;
    [SerializeField] BoxCollider2D boxCol2;
    [SerializeField] CapsuleCollider2D capsuleCol;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor = new Color(255f, 255f, 255f, 255f);
    private Color resetColor = new Color(1f, 1f, 1f, 1f);
    private float countdownSecond = 5;
    private  const float maxcount = 5;
    public bool ignitionFlag;
    private bool first_ignitionFlag;
    private bool burnFlag;
    private const int defaultLayer = 0;
    private const int itemLayer = 3;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Ignition();
        BomCount();
        //�t���O�A�J�E���g�A�R���C�_�[�A�����_���[�̃��Z�b�g
        if(GameManager.SelectReSet)
        {
            countdownSecond = maxcount;
            gameObject.layer = 3;
            spriteRenderer.enabled = true;
            spriteRenderer.color = resetColor;
            burnFlag = false;
            ignitionFlag = false;
            first_ignitionFlag = false;
            boxCol1.enabled = true;
            boxCol2.enabled = true;
            capsuleCol.enabled = false;
            GameManager.CloneBomDestroy();
        }
    }
    //�����̃^�C�~���O�������ł���悤�ɁA�c�莞�Ԃɉ����ŃJ���[�����X�ɕύX
    public IEnumerator explosion_Count()
    {
        yield return new WaitForSeconds(1f);
        while(spriteRenderer.color.r <= 1)
        {
            spriteRenderer.color -= new Color(0, 0.01f, 0.01f, 0);
            yield return null;
        }
    }
    //�I�u�W�F�N�g�ɐڐG���J�E���g�_�E���J�n
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!ignitionFlag)
        {
            Instantiate(ignitionEffect, transform.position, transform.rotation);
            first_ignitionFlag = true;
            ignitionFlag = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE17();
        }
    }
    //�������̑��I�u�W�F�N�g�֏���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�q�b�g�����I�u�W�F�N�g�̃^�O��"Untagged"�̏ꍇ
        //�R���C�_�[�ƃ��b�V�������_���[���\���ɂ���
        if(collision.CompareTag("Untagged"))
        {
            collision.GetComponent<MeshRenderer>().enabled = false;
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
        //�q�b�g�����I�u�W�F�N�g�̃^�O��"arrow"�̏ꍇ
        //�I�u�W�F�N�g�̍폜
        if(collision.CompareTag("arrow"))
        {
            Destroy(collision.gameObject);
        }
        //�q�b�g�����I�u�W�F�N�g�̃^�O��"Player"�̏ꍇ
        //���Z�b�g�t���O��Ԃ�
        if(collision.CompareTag("Player"))
        {
            GameManager.GameReset();
        }
        //�q�b�g�����I�u�W�F�N�g�̃^�O��"Bom"�A"CloneBom"�̏ꍇ
        //��������
        if(collision.CompareTag("Bom")|| collision.CompareTag("CloneBom"))
        {
            Explosion();
        }
    }
    //���e�������̏���
    //�{�b�N�X�R���C�_�[�ƃX�v���C�g�����_���[�̔�\���A�J���[�ύX
    //�J�v�Z���R���C�_�[�A�G�t�F�N�g�̕\��
    private void Explosion()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        boxCol1.enabled = false;
        boxCol2.enabled = false;
        capsuleCol.enabled = true;
        spriteRenderer.color = defaultColor;
        spriteRenderer.enabled = false;
        SoundManager.Instance.SettingPlaySE18();

    }
    //���e�����O�̃J�E���g�_�E������
    //���e�̃��C���[�ύX
    private void BomCount()
    {
        if(countdownSecond <= 4.8f)
        {
            gameObject.layer = itemLayer;
        }
        if(countdownSecond < 0.5f)
        {
            gameObject.layer = defaultLayer;
        }
        if(countdownSecond <= 0.0f && !burnFlag)
        {
            Explosion();
            StartCoroutine(SetTime());
            burnFlag = true;
        }
    }
    //���e�_�Ύ��̏���
    private void Ignition()
    {
        if(ignitionFlag)
        {
            countdownSecond -= Time.deltaTime;
            boxCol2.enabled = false;
            if(first_ignitionFlag)
            {
               first_ignitionFlag = false;
               StartCoroutine(explosion_Count());
            }
        }
    }
    private IEnumerator SetTime()
    {
        yield return new WaitForSeconds(1.0f);
        capsuleCol.enabled = false;
    }
}
