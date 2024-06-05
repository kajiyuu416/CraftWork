using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionSC : MonoBehaviour
{
    private Vector3 OriginPosition;
//�Q�[���J�n�������ʒu�̋L�^
    private void Awake()
    {
        OriginPosition = transform.position;
    }
    //���Z�b�g�t���O���Ԃ����Ƃ��ɃI�u�W�F�N�g�̈ʒu�������ʒu�ɖ߂�
    private void Update()
    {
        if(GameManager.SelectReSet)
        {
            transform.position = OriginPosition;
        }
    }
}
