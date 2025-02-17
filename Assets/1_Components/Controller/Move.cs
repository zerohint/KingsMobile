using UnityEngine;
using UnityEngine.InputSystem;

public partial class CameraController
{
    public class Move : ControllerModule
    {
        [SerializeField] private float moveSpeed = 40f;
        [SerializeField] private float dragSpeed = 10f;

        [SerializeField] private Vector2 xLimits = new Vector2(-300f, 300f);
        [SerializeField] private Vector2 zLimits = new Vector2(-150f, 150f);

        private Vector2 lastMousePosition;
        private bool isDragging = false;

        private InputAction moveAction;
        private InputAction mousePositionAction;
        private InputAction leftClickAction;

        public Move(CameraController controller) : base(controller)
        {
            moveAction = Map.FindAction("Move");
            mousePositionAction = Map.FindAction("PressPosition");
            leftClickAction = Map.FindAction("Press");

            leftClickAction.started += OnLeftClickStarted;
            leftClickAction.canceled += OnLeftClickCanceled;
        }

        private void OnLeftClickStarted(InputAction.CallbackContext context)
        {
            lastMousePosition = mousePositionAction.ReadValue<Vector2>();
            isDragging = true;
        }

        private void OnLeftClickCanceled(InputAction.CallbackContext context)
        {
            isDragging = false;
        }

        internal override void Handle()
        {
            if (!enabled) return;
            if (!ShouldProcessInput()) return;

            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector3 moveDirection = (CamT.right * moveInput.x) + (CamT.forward * moveInput.y);
            moveDirection.y = 0f;
            CamT.position += moveSpeed * Time.deltaTime * moveDirection;

            if (isDragging)
            {
                Vector2 currentMousePosition = mousePositionAction.ReadValue<Vector2>();
                Vector2 deltaMousePosition = currentMousePosition - lastMousePosition;

                Vector3 dragMovement = new Vector3(-deltaMousePosition.x, 0, -deltaMousePosition.y) * dragSpeed * Time.deltaTime;
                Vector3 worldDrag = CamT.right * dragMovement.x + Vector3.ProjectOnPlane(CamT.forward, Vector3.up) * dragMovement.z;

                CamT.position += worldDrag;
                lastMousePosition = currentMousePosition;
            }

            Vector3 clampedPosition = CamT.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, xLimits.x, xLimits.y);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, zLimits.x, zLimits.y);
            CamT.position = clampedPosition;
        }
    }
}
