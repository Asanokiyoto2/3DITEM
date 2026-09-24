using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 8f, -8f);
    public float followSpeed = 10f;

    // マウス感度
    public float mouseSensitivity = 3f;

    private float rotationY = 0f;

    void Start()
    {
        // 現在のカメラの横向き角度を取得
        rotationY = transform.eulerAngles.y;

        // マウスを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // マウスの左右移動だけ取得
        float mouseX = Input.GetAxis("Mouse X");

        // 横方向に回転
        rotationY += mouseX * mouseSensitivity;

        // 回転を作る
        Quaternion rotation = Quaternion.Euler(0f, rotationY, 0f);

        // プレイヤーを中心にカメラを配置
        Vector3 targetPosition =
            target.position + rotation * offset;

        // カメラを追従
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        // プレイヤーを見る
        transform.LookAt(target);
    }
}
