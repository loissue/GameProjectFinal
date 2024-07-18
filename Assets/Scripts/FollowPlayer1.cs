using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
    public Transform player; // Tham chiếu tới Transform của người chơi
    public float followSpeed = 2f; // Tốc độ di chuyển để theo dõi người chơi

    void Update()
    {
        if (player != null)
        {
            // Tính toán vị trí mới bằng cách sử dụng Vector3.Lerp để di chuyển mượt mà
            Vector3 newPosition = Vector3.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }
}
