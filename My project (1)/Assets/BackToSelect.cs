using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BackToSelect : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("button clicked");
        LoadSelectScene();
    }

    public void LoadSelectScene()
    {
        SceneManager.LoadScene("Select");
    }
}
