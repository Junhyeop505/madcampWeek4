using UnityEngine;

public class PaperPlaneMovement : MonoBehaviour
{
    public float speed = 15.0f; // 비행기 이동 속도
    public float yRange = 3.0f; // Y축 포물선 높이
    public float startOffset = 5.0f; // 시작 위치를 화면 왼쪽에서 얼마나 오른쪽으로 이동할지
    public float rangeMultiplier = 1.5f; // 이동 반경을 늘리는 비율
    public float rotationAngle = 90.0f; // 비행기의 회전 각도
    public string titleObjectName = "TitlePP"; // Title 오브젝트 이름 (Hierarchy에 있는 이름)
    public float titleStartOffset = 0.3f; // 제목이 나타나기 시작하는 시점 (0~1)
    public float cameraTiltDuration = 5.0f; // 카메라 회전 애니메이션 지속 시간

    private GameObject titleObject;
    private SpriteRenderer spriteRenderer; // Sprite Renderer 참조
    private Vector3 startPosition;
    private float screenLeftEdge;
    private float screenRightEdge;
    private bool titleAnimationTriggered = false;
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
    screenLeftEdge -= (screenRightEdge - screenLeftEdge) * 0.5f; // 왼쪽 경계를 더 확장
    screenRightEdge += (screenRightEdge - screenLeftEdge) * 0.5f; // 오른쪽 경계를 더 확장

    // 시작 위치를 화면 왼쪽 경계에서 더 왼쪽으로 이동
    startPosition = new Vector3(screenLeftEdge - startOffset, transform.position.y, 0); // Z축 고정
    transform.position = startPosition;

    // TitlePP 오브젝트 자동 연결 및 SpriteRenderer 가져오기
    titleObject = GameObject.Find(titleObjectName);
    if (titleObject == null)
    {
        Debug.LogError($"Title Object '{titleObjectName}'를 찾을 수 없습니다. 이름을 확인하세요!");
        return;
    }

    spriteRenderer = titleObject.GetComponent<SpriteRenderer>();
    if (spriteRenderer == null)
    {
        Debug.LogError("SpriteRenderer를 찾을 수 없습니다!");
        return;
    }

    // 제목 초기 상태 설정 (투명)
    var color = spriteRenderer.color;
    color.a = 0.0f; // Alpha 값을 0으로 설정
    spriteRenderer.color = color;

    // 카메라 애니메이션 시작
    StartCoroutine(TiltCamera());
}


    void Update()
    {
        if (!cameraAnimationCompleted) return; // 카메라 애니메이션이 끝날 때까지 대기

        // 시간에 따라 비행기 X축으로 이동
        float newX = transform.position.x + (speed * Time.deltaTime);

        // X축 위치에 따라 포물선 계산
        float newY = startPosition.y + Mathf.Sin((newX - screenLeftEdge) / (screenRightEdge - screenLeftEdge) * Mathf.PI) * yRange;

        // 새로운 위치 적용
        transform.position = new Vector3(newX, newY, startPosition.z);

        // 이동에 따라 비행기 회전
        float rotationProgress = (newX - screenLeftEdge) / (screenRightEdge - screenLeftEdge); // 0에서 1 사이
        float newRotationZ = rotationProgress * rotationAngle; // 회전 각도 계산
        transform.rotation = Quaternion.Euler(0, 0, -newRotationZ); // 시계 방향 회전

        // 비행기가 화면 오른쪽 경계를 넘어가면 비활성화
        if (newX > screenRightEdge)
        {
            gameObject.SetActive(false);
        }

        // 제목 애니메이션 처리
        if (!titleAnimationTriggered && rotationProgress >= titleStartOffset)
        {
            titleAnimationTriggered = true; // 한 번만 실행
            StartCoroutine(FadeInTitle());
        }
    }

    private System.Collections.IEnumerator FadeInTitle()
    {
        if (spriteRenderer == null) yield break;

        float duration = 1.0f; // 투명도 변화 지속 시간
        float elapsedTime = 0.0f;

        var color = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Alpha 값 증가
            color.a = Mathf.Lerp(0.0f, 1.0f, elapsedTime / duration);
            spriteRenderer.color = color;

            yield return null;
        }

        // 최종 Alpha 값 보장
        color.a = 1.0f;
        spriteRenderer.color = color;
    }

    private System.Collections.IEnumerator TiltCamera()
    {
        float elapsedTime = 0.0f;

        // 카메라 초기 각도와 목표 각도 설정
        Quaternion initialRotation = Quaternion.Euler(45.0f, 0.0f, 0.0f); // 지면을 향한 초기 각도
        Quaternion targetRotation = Quaternion.Euler(-45.0f, 0.0f, 0.0f); // 하늘을 향한 목표 각도

        // 카메라 회전
        while (elapsedTime < cameraTiltDuration)
        {
            elapsedTime += Time.deltaTime;

            // 카메라의 회전 애니메이션
            cameraTransform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / cameraTiltDuration);

            yield return null;
        }

        // 최종 회전값 보장
        cameraTransform.rotation = targetRotation;

        // 카메라 애니메이션 완료
        cameraAnimationCompleted = true;
    }


}
