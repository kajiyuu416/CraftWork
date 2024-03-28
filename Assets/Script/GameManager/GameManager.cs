using UnityEngine;
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
    private bool ReSetUIexpression;
    private bool SettingUIexpression;

    public static GameManager instance
    {
        get; private set;
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        ReSetUIexpression = true;
        SettingUIexpression = true;
    }
    void Update()
    {
        if(PlayerController.ReSetFlag && ReSetUIexpression&&!PlayerController.SettingFlag)
        {
            ReSetUI.enabled = true;
            ReSetUIexpression = false;
            EventSystem.current.SetSelectedGameObject(ReSetButton);
        }
        else if(!PlayerController.ReSetFlag &&!ReSetUIexpression)
        {
         ReSetUIexpression = true;
         ReSetUI.enabled = false;
        }

        if(PlayerController.SettingFlag && SettingUIexpression&&!PlayerController.ReSetFlag)
        {
            SettingUI.enabled = true;
            SettingUIexpression = false;
            EventSystem.current.SetSelectedGameObject(SettingButton);
        }
        else if(!PlayerController.SettingFlag && !SettingUIexpression)
        {
            SettingUI.enabled = false;
            SettingUIexpression = true;
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
    }

    public static void SelectCancel()
    {
        instance.SelectCl();
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
}
