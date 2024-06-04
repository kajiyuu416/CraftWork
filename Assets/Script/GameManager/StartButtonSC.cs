using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButtonSC : MonoBehaviour
{
    public Button startButton;
    [SerializeField] GameObject StartButton;
    private bool NowMenuFlag;
    private bool collOne;
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(() => {
        GameManager.GameStart();
        });
        EventSystem.current.SetSelectedGameObject(StartButton);
    }
    void Update()
    {
        if(!GameManager.SettingFlag)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                EventSystem.current.SetSelectedGameObject(StartButton);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if(GameManager.SettingFlag)
        {
            NowMenuFlag = true;
            collOne = true;
        }
        else
        {
            NowMenuFlag = false;
        }

        if(!NowMenuFlag&&collOne)
        {
            EventSystem.current.SetSelectedGameObject(StartButton);
            collOne = false;
        }
    }
}