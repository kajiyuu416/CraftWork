using System;
using System.Collections;
using UnityEngine;
public class TorchSC : MonoBehaviour
{
    [SerializeField] float countdownSecond = 0;
    [SerializeField] Sprite torch_On_Sprite;
    [SerializeField] Sprite torch_Off_Sprite;
    private SpriteRenderer Origin_Sprite;
    private bool torch_off;
    public float Destroy_value;
    public bool burnFlag;
    public Light TorchLight;
    private bool DestroyFlag;
    public static TorchSC Instance
    {
        get; private set;
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        TorchLight = GetComponent<Light>();
        Origin_Sprite = GetComponent<SpriteRenderer>();
        Origin_Sprite.sprite = torch_Off_Sprite;
    }
    private void FixedUpdate()
    {
        CountDown();
    }
    //Playerが特定の座標にいる場合トーチをPlayerの座標に移動させる処理
    private void Update()
    {
        if(PlayerController.SelectReSet)
        {
            transform.position = PlayerController.CP;
            if(!burnFlag)
            {
                ignition();
            }
        }
        Vector3 posi = this.transform.localPosition;

        if(posi.x > Destroy_value &&!DestroyFlag)
        {
            DestroyFlag = true;
            PlayerController Pc = PlayerController.Instance;
            Pc.ItemLost();
            Destroy(gameObject);
        }

    }
    //特定のエリアに当たると強さが一定の値まで上がる処理、トーチのライトの強さが徐々に小さくなる処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("supplyArea"))
        {
            ignition();
        }
    }
    public IEnumerator Torch_light_lose()
    {
        var min_range = 0;
        var max_range = 20.0f;

        yield return new WaitForSeconds(1f);
        while(TorchLight.range >= min_range)
        {
            TorchLight.range -= 0.02f;
            if(TorchLight.range == min_range)
            {
                Origin_Sprite.sprite = torch_Off_Sprite;
                yield break;
            }

            if(burnFlag)
            {
                TorchLight.range = max_range;
                yield break;
            }
            yield return null;
        }
    }
    private void ignition()
    {
        TorchLight.range = 20;
        countdownSecond = 30;
        torch_off = false;
        burnFlag = true;
        Origin_Sprite.sprite = torch_On_Sprite;
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE14();
    }
    private void CountDown()
    {
        countdownSecond -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int) countdownSecond);

        if(countdownSecond <= 20.0f && !torch_off)
        {
            torch_off = true;
            burnFlag = false;
            StartCoroutine(Torch_light_lose());
        }

        if(countdownSecond <= 0)
        {
            countdownSecond = 0;
        }
    }

}
