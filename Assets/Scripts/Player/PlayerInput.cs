using Helpers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        
        [HideInInspector] public bool canShoot;
        [HideInInspector] public float h, v;
        [HideInInspector] public Vector2 rotateDirection;

        private bool readyToClear;

        private void Update()
        {
            ClearInput();

            ProcessInputs();

            h = Mathf.Clamp(h, -1f, 1f);
            v = Mathf.Clamp(v, -1f, 1f);
        }

        private void ClearInput()
        {
            if (!readyToClear)
                return;

            h = 0f;
            v = 0f;
            rotateDirection = Vector2.zero;
            canShoot = false;

            readyToClear = false;
        }

        private void ProcessInputs()
        {
            h += Input.GetAxisRaw("Horizontal");
            v += Input.GetAxisRaw("Vertical");

            canShoot = canShoot || Input.GetButton("Fire1");

            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            rotateDirection = (mousePosition - (Vector2) transform.position).normalized;
        }

        private void FixedUpdate()
        {
            readyToClear = true;
        }
    }
}