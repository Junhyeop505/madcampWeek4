using UnityEngine;

public class BookController : MonoBehaviour
{
    public GameObject bookPrefab; // 책 Prefab을 연결
    public Transform spawnPoint; // 책 생성 위치
    public float spawnInterval = 1f; // 책 생성 간격
    public float moveSpeed = 60f; // 책 이동 속도
    public float gravity = 9.8f; // 책이 떨어질 때 중력 효과

    private void Start()
    {
        // 책을 주기적으로 생성
        InvokeRepeating(nameof(SpawnBook), 0f, spawnInterval);
    }

    private void SpawnBook()
    {
        // 책을 생성
        GameObject book = Instantiate(bookPrefab, spawnPoint.position, spawnPoint.rotation);
        book.GetComponent<Book>().Initialize(moveSpeed, gravity);
    }
}
