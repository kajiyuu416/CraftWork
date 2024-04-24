using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BowSC : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    private SpriteRenderer origin_Sprite;
    public static BowSC instance
    {
        get; private set;
    }
    private void Awake()
    {
        origin_Sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        rotationChange();
    }

    private void rotationChange()
    {
        Transform origin_transform = this.transform;
        if(origin_Sprite.flipX)
        {
            Vector3 localAngle = origin_transform.localEulerAngles;
            localAngle.z = 40.0f;
            origin_transform.localEulerAngles = localAngle;
        }
        else
        {
            Vector3 localAngle = origin_transform.localEulerAngles;
            localAngle.z = -40.0f;
            origin_transform.localEulerAngles = localAngle;
        }
    }
    public void shot()
    {
        Debug.Log("shot1");
        GameObject geneObj = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, 0f));
    }

}
