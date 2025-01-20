using UnityEngine;

public class PaperPlanesMovement : MonoBehaviour
{
    public float speed = 10.0f; // 비행기 이동 속도
    public float yRange = 3.0f; // Y축 포물선 높이
    public float startOffset = 5.0f; // 시작 위치를 화면 왼쪽에서 얼마나 오른쪽으로 이동할지
    public float zOffset = 10.0f; // Z축 오프셋(뒤쪽 위치)
    public float rotationSpeed = -45.0f; // 비행기 회전 속도(각도/초)
    public float repeatInterval = 5.0f; // 반복 간격(초)
    public float extraRightDistance = 5.0f; // 화면 오른쪽 경계를 넘어가는 추가 이동 거리

    private Vector3 startPosition;
    private float screenLeftEdge;
    private float screenRightEdge;
    private float elapsedTime = 0.0f;

    void Start()
    {
        // Main Camera의 경계를 계산
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera가 Scene에 없습니다!");
            return;
        }

        screenLeftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, Mathf.Abs(mainCamera.transform.position.z))).x;
        screenRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, Mathf.Abs(mainCamera.transform.position.z))).x;

        // 시작 위치를 화면 왼쪽 경계에서 더 왼쪽으로 이동하고 뒤쪽으로 설정
        startPosition = new Vector3(screenLeftEdge - startOffset, transform.position.y, zOffset);
        transform.position = startPosition;

        // 이동 시작
        StartCoroutine(MoveAndRepeat());
    }

    System.Collections.IEnumerator MoveAndRepeat()
    {
        while (true)
        {
            // 비행기의 초기 위치 설정
            transform.position = startPosition;
            elapsedTime = 0.0f;

            // 비행기가 포물선을 그리며 이동
            while (transform.position.x < screenRightEdge + extraRightDistance)
            {
                // X축 이동
                float newX = transform.position.x + (speed * Time.deltaTime);
                // Y축 이동 (포물선)
                float newY = startPosition.y + Mathf.Sin((newX - screenLeftEdge) / (screenRightEdge - screenLeftEdge) * Mathf.PI) * yRange;

                // Z축 고정
                float newZ = startPosition.z;

                // 위치 업데이트
                transform.position = new Vector3(newX, newY, newZ);

                // 비행기 회전
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

                yield return null; // 다음 프레임까지 대기
            }

            // 반복 간격 대기
            yield return new WaitForSeconds(repeatInterval);
        }
    }
}
