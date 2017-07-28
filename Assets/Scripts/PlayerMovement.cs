using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed;

    Rigidbody2D rb2D;
    Vector2 movement;
    Vector2 direction;
    Vector2 mousePosition;
    float h;
    float v;
    float angle;

	// Use this for initialization
	void Awake ()
    {
        rb2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate ()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Rotate();
    }

    void Move (float h, float v)
    {
        movement.Set(h,v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb2D.velocity = movement;
    }

    void Rotate ()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - (Vector2)transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
