using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick leftJoystick;
        [SerializeField] private Joystick rightJoystick;

        [HideInInspector] public bool canAttack;
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
            canAttack = false;

            readyToClear = false;
        }

        private void ProcessInputs()
        {
            h += leftJoystick.Horizontal;
            v += leftJoystick.Vertical;

            canAttack = canAttack || rightJoystick.Direction != Vector2.zero;

            rotateDirection = rightJoystick.Direction;
        }

        private void FixedUpdate()
        {
            readyToClear = true;
        }
    }
}