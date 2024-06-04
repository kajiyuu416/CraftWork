using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private GameObject lock_target = null;
    private const  float camera_speed = 5f;
    private float shake_timer = 0f;
    private float shake_intensity = 1f;
    private Vector3 cur_pos;
    private Vector3 target_offset = Vector3.zero;
    private Vector3 shake_vector = Vector3.zero;
    private Vector2 RightInput;
    private Camera cam;
    private static CameraController _instance;

    void Awake()
    {
        _instance = this;
        cam = GetComponent<Camera>();

    }
    private void Update()
    {
        if(!GameManager.ReSetFlag &&!GameManager.SettingFlag)
        {
            CameraControl();
        }
    }
    void LateUpdate()
    {
        GameObject cam_target = target;

        if (lock_target != null)
            cam_target = lock_target;

        if (cam_target != null)
        {
            Vector3 target_pos = cam_target.transform.position + target_offset;
            Vector3 diff = target_pos - transform.position;
            if (diff.magnitude > 0.1f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref cur_pos, 1f / camera_speed, Mathf.Infinity, Time.deltaTime);
            }
        }
        if (shake_timer > 0f)
        {
            shake_timer -= Time.deltaTime;
            shake_vector = new Vector3(Mathf.Cos(shake_timer * Mathf.PI * 8f) * 0.02f, Mathf.Sin(shake_timer * Mathf.PI * 7f) * 0.02f, 0f);
            transform.position += shake_vector * shake_intensity;
        }
    }
    public float GetFrustrumHeight()
    {
        if (cam.orthographic)
            return 2f * cam.orthographicSize;
        else
            return 2.0f * Mathf.Abs(transform.position.z) * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    public float GetFrustrumWidth()
    {
        return GetFrustrumHeight() * cam.aspect;
    }

    public void LockCameraOn(GameObject ltarget)
    {
        lock_target = ltarget;
    }

    public void UnlockCamera()
    {
        lock_target = null;
    }

    public void Shake(float intensity = 2f, float duration = 0.5f)
    {
        shake_intensity = intensity;
        shake_timer = duration;
    }

    public static CameraController Get()
    {
        return _instance;
    }

    public static Camera GetCamera()
    {
        if (_instance)
            return _instance.cam;
        return null;
    }

    public void CameraControl()
    {
        RightInput = PlayerController.CameraInputVal;

        if(RightInput.x > 0.9f)
        {
            target_offset = new Vector3(7, -3.5f, -7.5f);
        }
        else if(RightInput.x < -0.9f)
        {
            target_offset = new Vector3(-7, -3.5f, -7.5f);
        }
        else if(RightInput.y > 0.9f)
        {
            target_offset = new Vector3(0, 0.5f, -7.5f);
        }
        else if(RightInput.y < -0.9f)
        {
            target_offset = new Vector3(0, -7, -7.5f);
        }
        else
        {
            target_offset = new Vector3(0, -3.5f, -7.5f);
        }
    }

}
