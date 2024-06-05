using UnityEngine;
public class SumoonEnemyDestroy : MonoBehaviour
{
    private void Update()
    {
        if(GameManager.SelectReSet)
        {

            Debug.Log("destroy");
        }
    }
}
