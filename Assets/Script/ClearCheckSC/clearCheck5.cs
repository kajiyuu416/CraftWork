using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class clearCheck5 : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField] CinemachineVirtualCamera camera2;
    private bool clearFlag = false;
    private SpriteRenderer originSprite;
    public Vector3 On_Pos;

    private void Awake()
    {
        originSprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !clearFlag)
        {
            clearFlag = true;
            originSprite.flipX = true;
            camera2.Priority = 11;
            SoundManager.Instance.SettingPlaySE8();
            StartCoroutine("ChangeCamera");
        }
    }
    private void Update()
    {
        if(clearFlag)
        {
            float speed = 1.0f; // ˆÚ“®‚Ì‘¬“x‚ðŽw’è
            Transform objectTransform = targetObj.gameObject.GetComponent<Transform>();
            objectTransform.position = Vector3.Lerp(objectTransform.position, On_Pos, speed * Time.deltaTime);
        }
    }
    IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(3.0f);
        camera2.Priority = 9;
    }
}
