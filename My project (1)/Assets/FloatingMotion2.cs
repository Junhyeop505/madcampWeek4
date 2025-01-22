using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 추가

public class FloatingMotion2 : MonoBehaviour
{
    public float speed = 2.0f; // 위아래로 움직이는 속도
    public float amplitude = 0.5f; // 움직이는 거리

    private Vector3 startPosition;
    private Vector3 originalScale; // 원래 크기 저장


    void Start()
    {
        // 오브젝트의 시작 위치 저장
        startPosition = transform.position;
        originalScale = transform.localScale;

    }

    void Update()
    {
        // 사인파를 사용해 위아래로 움직이는 모션 구현
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    void OnMouseDown()
    {
        // 클릭 시 크기 줄이기
        transform.localScale = originalScale * 0.9f;

        // 약간의 지연 후 씬 전환 (0.1초 후)
        Invoke("LoadTutorialScene", 0.1f);

    }

    void LoadTutorialScene()
    {
        SceneManager.LoadScene("PlaymapScene");
    }
}
