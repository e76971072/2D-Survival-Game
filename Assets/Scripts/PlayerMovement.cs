using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private float speed;
    
    [SerializeField] private Transform weaponsHolder;

    #endregion

    #region NonSerializeFields

    private Vector2 movement;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private PlayerInput playerInput;

    #endregion

    private void Awake()
    {
        playerTransform = transform;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        
        GameManager.OnGameLost += DisableOnDead;
    }

    private void FixedUpdate()
    {
        CheckFlip(playerInput.h);
        Move(playerInput.h, playerInput.v);
        Rotate(playerInput.rotateDirection);
    }

    private void CheckFlip(float h)
    {
        if (Math.Abs(h) < Mathf.Epsilon) return;
        spriteRenderer.flipX = h < 0;
    }

    private void Move(float h, float v)
    {
        movement.Set(h, v);
        movement = Time.deltaTime * speed * movement.normalized;
        playerTransform.position += (Vector3) movement;
    }

    private void Rotate(Vector2 rotateDirection)
    {
        var angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        weaponsHolder.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void DisableOnDead()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Animator>().enabled = false;
        Debug.Log("Movement Disabled");
    }
}