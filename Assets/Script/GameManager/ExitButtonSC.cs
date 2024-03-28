using UnityEngine;
using UnityEngine.UI;

public class ExitButtonSC : MonoBehaviour
{
    public Button exitButton;
    void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(() =>
        {
            GameManager.EndGame();
        });
    }
}
