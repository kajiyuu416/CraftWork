using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ReSetButton;
    [SerializeField] Canvas ReSetUI;
    [SerializeField] GameObject SettingButton;
    [SerializeField] Canvas SettingUI;
    [SerializeField] GameObject BGMSlider;
    [SerializeField] Canvas AudioUI;
    [SerializeField] GameObject AudioUIobj;
    [SerializeField] GameObject ResetUIobj;
    private bool ReSetUIexpression;
    private bool SettingUIexpression;
    private string beforeScene;
    Image blackScreen;
    public static GameManager instance
    {
        get; private set;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;

        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        ReSetUIexpression = true;
        SettingUIexpression = true;
    }
    private void Start()
    {
        StartCoroutine(FadeIn());
        beforeScene = "title";
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    void Update()
    {

        if(PlayerController.ReSetFlag && ReSetUIexpression&&!PlayerController.SettingFlag)
        {
            ResetUIobj.SetActive(true);
            ReSetUI.enabled = true;
            ReSetUIexpression = false;
            EventSystem.current.SetSelectedGameObject(ReSetButton);
        }
        
        if(!PlayerController.ReSetFlag &&ReSetUIexpression)
        {
         ReSetUIexpression = true;
         ReSetUI.enabled = false;
         ResetUIobj.SetActive(false);
        }

        if(PlayerController.SettingFlag && SettingUIexpression&&!PlayerController.ReSetFlag)
        {
            AudioUIobj.SetActive(true);
            SettingUI.enabled = true;
            SettingUIexpression = false;
            EventSystem.current.SetSelectedGameObject(SettingButton);
        }
        
        if(!PlayerController.SettingFlag && SettingUIexpression)
        {
           
            SettingUI.enabled = false;
            SettingUIexpression = true;
            AudioUIobj.SetActive(false);
        }
    }
    public static void GameReset()
    {
        Scene loadScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadScene.name);
        instance.SelectCl();
    }
    public static void SettingAudio()
    {
        instance.SelectAudio();
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE13();
    }

    public static void SelectCancel()
    {
        instance.SelectCl();
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE13();
    }
    public static void GameStart()
    {
        instance.StartCoroutine(instance.LoadScene("MainScene"));
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE12();
    }
    public static void EndGame()
    {
        Application.Quit();
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE12();
    }
    //Todo;;タイトルに戻るとエラーが発生するため、一時使用不可
    public static void BacktoTitle()
    {
        instance.StartCoroutine(instance.LoadScene("title"));
    }

    public IEnumerator LoadScene(string sceneName)
    {
        var color = blackScreen.color;
        while(color.a <= 1)
        {
            color += new Color(0, 0, 0, 0.05f);
            blackScreen.color = color;

            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
    public IEnumerator FadeIn()
    {
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        var color = blackScreen.color;
        yield return new WaitForSeconds(1f);

        while(color.a >= 0)
        {
            color -= new Color(0, 0, 0, 0.05f);
            blackScreen.color = color;

            yield return null;
        }
    }
    public void SelectCl()
    {
        PlayerController.ReSetFlag = false;
        PlayerController.SettingFlag = false;
        ReSetUIexpression = true;
        SettingUIexpression = true;
        ReSetUI.enabled = false;
        SettingUI.enabled = false;
        AudioUI.enabled = false;
    }
    public void SelectAudio()
    {
        AudioUI.enabled = true;
        EventSystem.current.SetSelectedGameObject(BGMSlider);
        ReSetUI.enabled = false;
        SettingUI.enabled = false;
    }
    public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        // Scene1からScene2へ
        if(beforeScene == "title" && nextScene.name == "MainScene")
        {
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm1();
            StartCoroutine(FadeIn());
        }
        if(beforeScene == "MainScene" && nextScene.name == "title")
        {
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            StartCoroutine(FadeIn());
        }
        if(beforeScene == "MainScene" && nextScene.name == "MainScene")
        {
            SoundManager SM = SoundManager.Instance;
            SM.StopBGM();
            SM.Startbgm1();
            StartCoroutine(FadeIn());
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
}
