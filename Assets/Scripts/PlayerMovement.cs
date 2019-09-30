using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private float speed;
    [SerializeField] private Camera mainCamera;

    #endregion

    #region NonSerializeFields

    [HideInInspector] public Vector2 direction;

    private Vector2 movement;
    private float h, v;
    private Transform playerTransform;

    #endregion
    
    private void Awake()
    {
        playerTransform = transform;
    }

    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Rotate();
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, vertical);
        movement = Time.deltaTime * speed * movement.normalized;
        playerTransform.position += (Vector3)movement;
    }

    private void Rotate()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - (Vector2) playerTransform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}