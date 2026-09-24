using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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
        Vector3 movement = new Vector3(input.x, 0f, input.y);

        Vector3 nextPosition =
            rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }
}
