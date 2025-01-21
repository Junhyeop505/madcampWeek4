using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameStart : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("button clicked");
        GameStartScene();
    }

    public void GameStartScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
