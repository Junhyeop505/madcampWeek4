using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public Image tutorialImage; // UI Image 컴포넌트
    public Sprite[] tutorialSprites; // 튜토리얼에 사용할 이미지 배열

    private int currentImageIndex = 0; // 현재 표시 중인 이미지의 인덱스

    void Start()
    {
        // 첫 번째 이미지로 설정
        if (tutorialSprites.Length > 0)
        {
            tutorialImage.sprite = tutorialSprites[currentImageIndex];
        }
    }

    void Update()
    {
        // 마우스 왼쪽 클릭 시 다음 이미지로 변경
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextImage();
        }
    }

    void ShowNextImage()
    {
        // 현재 이미지 인덱스를 증가
        currentImageIndex++;

        if (currentImageIndex < tutorialSprites.Length)
        {
            // 다음 이미지로 변경
            tutorialImage.sprite = tutorialSprites[currentImageIndex];
        }
        else
        {
            // 튜토리얼 종료 시 LobyScene으로 이동
            LoadLobyScene();
        }
    }

    void LoadLobyScene()
    {
        SceneManager.LoadScene("LobyScene");
    }
}
