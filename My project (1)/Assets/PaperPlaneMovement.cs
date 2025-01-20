using UnityEngine;

public class PaperPlaneMovement : MonoBehaviour
{
    public float speed = 15.0f; // 비행기 이동 속도
    public float yRange = 3.0f; // Y축 포물선 높이
    public float startOffset = 5.0f; // 시작 위치를 화면 왼쪽에서 얼마나 오른쪽으로 이동할지
    public float rangeMultiplier = 1.5f; // 이동 반경을 늘리는 비율
    public float rotationAngle = 90.0f; // 비행기의 회전 각도
    public string titleObjectName = "TitlePP"; // Title 오브젝트 이름
    public string startCloudObjectName = "StartCloud"; // StartCloud 오브젝트 이름
    public float titleStartOffset = 0.3f; // 제목과 StartCloud가 나타나기 시작하는 시점 (0~1)
    public float fadeInDuration = 1.5f; // 페이드 인 지속 시간
    public float cameraTiltDuration = 5.0f; // 카메라 회전 애니메이션 지속 시간

    private GameObject titleObject;
    private GameObject startCloudObject;
    private SpriteRenderer titleSpriteRenderer; // Title Sprite Renderer 참조
    private SpriteRenderer startCloudSpriteRenderer; // Start Cloud Sprite Renderer 참조
    private Vector3 startPosition;
    private float screenLeftEdge;
    private float screenRightEdge;
    private bool animationTriggered = false;
    private bool cameraAnimationCompleted = false;
    private Transform cameraTransform; // Main Camera Transform

    void Start()
    {
        // Main Camera의 Transform 자동 설정
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera가 Scene에 없습니다!");
            return;
        }
        cameraTransform = mainCamera.transform;

        // 화면의 왼쪽과 오른쪽 경계를 계산하고 이동 반경을 확대
        screenLeftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, Mathf.Abs(mainCamera.transform.position.z))).x;
        screenRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, Mathf.Abs(mainCamera.transform.position.z))).x;

        // 이동 범위 추가로 확장
        screenLeftEdge -= (screenRightEdge - screenLeftEdge) * 0.5f;
        screenRightEdge += (screenRightEdge - screenLeftEdge) * 0.5f;

        // 시작 위치를 화면 왼쪽 경계에서 더 왼쪽으로 이동
        startPosition = new Vector3(screenLeftEdge - startOffset, transform.position.y, 0);
        transform.position = startPosition;

        // TitlePP 오브젝트 자동 연결
        titleObject = GameObject.Find(titleObjectName);
        if (titleObject == null)
        {
            Debug.LogError($"Title Object '{titleObjectName}'를 찾을 수 없습니다!");
            return;
        }
        titleSpriteRenderer = titleObject.GetComponent<SpriteRenderer>();
        if (titleSpriteRenderer == null)
        {
            Debug.LogError("Title Object에 SpriteRenderer 컴포넌트가 없습니다!");
            return;
        }
        SetAlpha(titleSpriteRenderer, 0.0f); // 초기 상태 투명

        // StartCloud 오브젝트 자동 연결
        startCloudObject = GameObject.Find(startCloudObjectName);
        if (startCloudObject == null)
        {
            Debug.LogError($"StartCloud 오브젝트 '{startCloudObjectName}'를 찾을 수 없습니다!");
            return;
        }
        startCloudSpriteRenderer = startCloudObject.GetComponent<SpriteRenderer>();
        if (startCloudSpriteRenderer == null)
        {
            Debug.LogError("StartCloud에 SpriteRenderer 컴포넌트가 없습니다!");
            return;
        }
        SetAlpha(startCloudSpriteRenderer, 0.0f); // 초기 상태 투명

        // 카메라 애니메이션 시작
        StartCoroutine(TiltCamera());
    }

    void Update()
    {
        if (!cameraAnimationCompleted) return;

        // 비행기 이동
        float newX = transform.position.x + (speed * Time.deltaTime);
        float newY = startPosition.y + Mathf.Sin((newX - screenLeftEdge) / (screenRightEdge - screenLeftEdge) * Mathf.PI) * yRange;
        transform.position = new Vector3(newX, newY, startPosition.z);

        // 비행기 회전
        float rotationProgress = (newX - screenLeftEdge) / (screenRightEdge - screenLeftEdge);
        float newRotationZ = rotationProgress * rotationAngle;
        transform.rotation = Quaternion.Euler(0, 0, -newRotationZ);

        // 제목과 StartCloud 애니메이션 처리
        if (!animationTriggered && rotationProgress >= titleStartOffset)
    {
        animationTriggered = true; // 한 번만 실행
        StartCoroutine(FadeIn(titleSpriteRenderer)); // Title 먼저 페이드 인
        StartCoroutine(FadeInWithDelay(startCloudSpriteRenderer, 1.5f)); // StartCloud는 1초 지연 후 페이드 인
    }

        // 비행기가 화면 오른쪽 경계를 넘어가면 비활성화
        if (newX > screenRightEdge)
        {
            gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator FadeIn(SpriteRenderer spriteRenderer)
    {
        float elapsedTime = 0.0f;
        var color = spriteRenderer.color;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;

            // Alpha 값 증가
            color.a = Mathf.Lerp(0.0f, 1.0f, elapsedTime / fadeInDuration);
            spriteRenderer.color = color;

            yield return null;
        }

        // 최종 Alpha 값 설정
        color.a = 1.0f;
        spriteRenderer.color = color;
    }

    private void SetAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        var color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    private System.Collections.IEnumerator TiltCamera()
    {
        float elapsedTime = 0.0f;
        Quaternion initialRotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);
        Quaternion targetRotation = Quaternion.Euler(-45.0f, 0.0f, 0.0f);

        while (elapsedTime < cameraTiltDuration)
        {
            elapsedTime += Time.deltaTime;
            cameraTransform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / cameraTiltDuration);
            yield return null;
        }

        cameraTransform.rotation = targetRotation;
        cameraAnimationCompleted = true;
    }

    private System.Collections.IEnumerator FadeInWithDelay(SpriteRenderer spriteRenderer, float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        yield return FadeIn(spriteRenderer); // 기존 FadeIn 코루틴 실행
    }
}
