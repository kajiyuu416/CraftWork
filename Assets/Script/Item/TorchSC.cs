using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TorchSC : MonoBehaviour
{
    [SerializeField] float countdownSecond = 0;
    [SerializeField] Sprite torch_On_Sprite;
    [SerializeField] Sprite torch_Off_Sprite;
    //private Text timeText;
    private SpriteRenderer Origin_Sprite;
    private bool torch_off;
    private bool burnFlag;
    private Light TorchLight;
    // Start is called before the first frame update
    private void Awake()
    {
        TorchLight = GetComponent<Light>();
        Origin_Sprite = GetComponent<SpriteRenderer>();
        Origin_Sprite.sprite = torch_Off_Sprite;
    }
    void FixedUpdate()
    {
        countdownSecond -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int) countdownSecond);

        if(countdownSecond <= 20.0f &&!torch_off)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("supplyArea"))
        {
            TorchLight.range = 20;
            countdownSecond = 30;
            torch_off = false;
            burnFlag = true;
            Origin_Sprite.sprite = torch_On_Sprite;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE14();
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
}
