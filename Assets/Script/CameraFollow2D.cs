using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    private Vector3 startOffset; // Lưu khoảng cách ban đầu
    public Transform target; // Nhân vật cần theo dõi
    public float smoothSpeed = 2f; // Giảm tốc độ mượt
    public float followThreshold = 0.5f; // Khoảng cách tối thiểu trước khi camera di chuyển

    void Start()
    {
        if (target != null)
        {
            startOffset = transform.position - target.position; // Lưu khoảng cách ban đầu
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + startOffset;
            float distance = Vector3.Distance(transform.position, desiredPosition);

            // Khi nhân vật di chuyển xa một khoảng nhất định, camera di chuyển chậm dần về vị trí mới
            if (distance > followThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPosition, (smoothSpeed * distance) * Time.deltaTime);
            }
        }
    }
}
