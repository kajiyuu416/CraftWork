using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItemSC : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D[] boxCollider2D;
    //�Q�[���J�n���擾
    private void Awake()
    {
        if(spriteRenderer == null || boxCollider2D == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider2D = GetComponents<BoxCollider2D>();
        }
    }
    //���Z�b�g�t���O���Ԃ����Ƃ��X�v���C�g�ƃR���C�_�[�̕\�����s��
    private void Update()
    {
        if(GameManager.SelectReSet)
        {
            spriteRenderer.enabled = true;
            boxCollider2D[0].enabled = true;
            boxCollider2D[1].enabled = true;

        }
    }
}
