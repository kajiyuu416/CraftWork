using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ReSetButton;
    [SerializeField] Canvas ReSetUI;
    private bool UIexpression ;

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
        UIexpression = true;
    }
    void Update()
    {
        if(PlayerController.ReSetFlag && UIexpression)
        {
            ReSetUI.enabled = true;
            UIexpression = false;
            EventSystem.current.SetSelectedGameObject(ReSetButton);
        }
        else if(!PlayerController.ReSetFlag &&!UIexpression)
             {
                UIexpression = true;
                ReSetUI.enabled = false;
             }

    }
    public static void GameReset()
    {
        Scene loadScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadScene.name);
        instance.SelectCl();
    }
    public static void SelectCancel()
    {
        instance.SelectCl();
    }
    public void SelectCl()
    {
        PlayerController.ReSetFlag = false;
        UIexpression = true;
        ReSetUI.enabled = false;
    }
}
