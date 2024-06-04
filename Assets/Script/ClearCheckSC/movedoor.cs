using UnityEngine;
public class movedoor : MonoBehaviour
{
    public Vector3 targetPos;
    [SerializeField] Charade charade;
    private Vector3 origin;

    private void Awake()
    {
        origin = transform.position;
    }
    private void Update()
    {
        if(charade.HitFlag)
        {
            float speed = 0.5f; // �ړ��̑��x���w��
            Transform objectTransform = gameObject.GetComponent<Transform>(); // �Q�[���I�u�W�F�N�g��Transform�R���|�[�l���g���擾
            objectTransform.position = Vector3.Lerp(objectTransform.position, targetPos, speed * Time.deltaTime); // �ړI�̈ʒu�Ɉړ�
        }else
        {
            transform.position = origin;
        }
    }
}
