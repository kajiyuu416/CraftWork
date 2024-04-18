using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CrealCheck5 : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField] CinemachineVirtualCamera camera2;
    public static bool LeverOn = false;
    public static bool firstClealFlag;
    private SpriteRenderer originSprite;
    public Vector3 On_Pos;

    private void Awake()
    {
        originSprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !LeverOn)
        {
            LeverOn = true;
            firstClealFlag = true;
            originSprite.flipX = true;
            camera2.Priority = 11;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE8();
            StartCoroutine("ChangeCamera");
            Debug.Log("aaa");
        }
    }
    void Update()
    {
        if(LeverOn)
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
