using System;
using Helpers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private float speed;
        [SerializeField] private Transform weaponsHolder;

        #endregion

        #region NonSerializeFields

        private Vector2 _movement;
        private Transform _playerTransform;
        private SpriteRenderer _spriteRenderer;
        private PlayerInput _playerInput;

        #endregion
        
        private void Awake()
        {
            _playerTransform = transform;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerInput = GetComponent<PlayerInput>();

            GameManager.OnGameLost += DisableOnDead;
        }

        private void FixedUpdate()
        {
            CheckFlip(_playerInput.h);
            Move(_playerInput.h, _playerInput.v);
            Rotate(_playerInput.rotateDirection);
        }

        private void CheckFlip(float h)
        {
            if (Math.Abs(h) < Mathf.Epsilon) return;
            _spriteRenderer.flipX = h < 0;
        }

        private void Move(float h, float v)
        {
            _movement.Set(h, v);
            _movement = Time.deltaTime * speed * _movement.normalized;
            _playerTransform.position += (Vector3) _movement;
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
        }

        private void OnDisable()
        {
            GameManager.OnGameLost -= DisableOnDead;
        }
    }
}