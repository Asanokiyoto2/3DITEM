using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    // 落下判定
    public float fallHeight = -5f;

    private Rigidbody rb;
    private Vector2 input;

    // 初期位置
    private Vector3 startPosition;

    // カメラ
    private Camera mainCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;

        mainCamera = Camera.main;
    }

    void Update()
    {
        // 落下したら初期位置に戻す
        if (transform.position.y < fallHeight)
        {
            ResetPosition();
        }

        if (Keyboard.current == null)
        {
            input = Vector2.zero;
            return;
        }

        float x = 0f;
        float z = 0f;

        if (Keyboard.current.aKey.isPressed)
            x = -1f;

        if (Keyboard.current.dKey.isPressed)
            x = 1f;

        if (Keyboard.current.wKey.isPressed)
            z = 1f;

        if (Keyboard.current.sKey.isPressed)
            z = -1f;

        input = new Vector2(x, z).normalized;
    }

    void FixedUpdate()
    {
        // カメラの向いている方向を取得
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // 上下方向は無視
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // カメラ基準で移動方向を作る
        Vector3 movement =
            cameraRight * input.x +
            cameraForward * input.y;

        // プレイヤーを移動
        Vector3 nextPosition =
            rb.position +
            movement * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }

    void ResetPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = startPosition;
    }
}
