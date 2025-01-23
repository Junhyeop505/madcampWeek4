using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickAdd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 originalScale; // 원래 크기 저장

    void Start()
    {
        // 오브젝트의 원래 크기를 저장
        originalScale = transform.localScale;
    }

    void OnMouseDown()
    {
        // 클릭 시 크기 줄이기
        transform.localScale = originalScale * 0.9f;

        // 약간의 지연 후 씬 전환 (0.1초 후)
        Invoke("LoadTutorialScene", 0.1f);
    }
    void OnMouseUp()
    {
        // 클릭 해제 시 크기 원래대로 복원
        transform.localScale = originalScale;
    }

    void LoadTutorialScene()
    {
        SceneManager.LoadScene("Select and Add");
    }
}
