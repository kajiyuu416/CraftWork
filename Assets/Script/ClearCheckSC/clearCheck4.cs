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
            float speed = 0.75f; // �ړ��̑��x���w��
            Transform objectTransform = targetObj.GetComponent<Transform>(); // �Q�[���I�u�W�F�N�g��Transform�R���|�[�l���g���擾
            objectTransform.position = Vector3.Lerp(objectTransform.position, targetPos, speed * Time.deltaTime); // �ړI�̈ʒu�Ɉړ�
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
            //�L�����h���Ƀg�[�`���ڐG�����Ƃ��A�q�b�g�t���O��Ԃ��B
            //���O�ɃC���X�y�N�^�[��ŁA�Z�b�g����answer�ƈ�v���Ă����clealFlag��true�ɂȂ� 
            //Charade1,4,6,8,9,10,12��false
            //Charade2,3,5,7,11 ��true
            if(ch.HitFlag != ch.answer)
            {
                clearFlag = false;
                Debug.Log(ch);
            }
            Debug.Log("�I�u�W�F�N�g��" + ch.name);
            Debug.Log("�t���O" + ch.HitFlag);

        }
    }

}
