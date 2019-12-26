using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private float speed;

    [SerializeField] private Camera mainCamera;

//    [SerializeField] private float dodgeDistance;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private Transform weaponsHolder;

    #endregion

    #region NonSerializeFields

    private Vector2 movement;
    private float h, v;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    #endregion

    private void Awake()
    {
        GameManager.OnGameLost += DisableOnDead;
        playerTransform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Rotate();
        Dodge();
    }

    private void Dodge()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

//        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, movement.normalized, dodgeDistance, wallLayerMask);

//        Vector2 dodgePoint = hit2D.collider == null ?  : hit2D.point
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, vertical);
        movement = Time.deltaTime * speed * movement.normalized;
        playerTransform.position += (Vector3) movement;
    }

    private void Rotate()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2) playerTransform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponsHolder.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void DisableOnDead()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Animator>().enabled = false;
        Debug.Log("Movement Disabled");
    }
}