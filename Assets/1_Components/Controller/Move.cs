using UnityEngine;
using UnityEngine.InputSystem;

public partial class CameraController
{
    public class Move : ControllerModule
    {
        [SerializeField] private float moveSpeed = 40f;
        [SerializeField] private float dragSpeed = 10f;

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
        }

        internal override void Handle()
        {
            if (!enabled) return;
            if (!ShouldProcessInput()) return; // UI üzerindeyse veya ekran dýþýndaysa input iþlenmez

            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector3 moveDirection = (CamT.right * moveInput.x) + (CamT.forward * moveInput.y);
            moveDirection.y = 0f;
            CamT.position += moveSpeed * Time.deltaTime * moveDirection;

            if (leftClickAction.WasPressedThisFrame())
            {
                lastMousePosition = mousePositionAction.ReadValue<Vector2>();
                isDragging = true;
            }
            else if (leftClickAction.WasReleasedThisFrame())
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Vector2 currentMousePosition = mousePositionAction.ReadValue<Vector2>();
                Vector2 deltaMousePosition = currentMousePosition - lastMousePosition;

                Vector3 dragMovement = new Vector3(-deltaMousePosition.x, 0, -deltaMousePosition.y) * dragSpeed * Time.deltaTime;
                Vector3 worldDrag = CamT.right * dragMovement.x + Vector3.ProjectOnPlane(CamT.forward, Vector3.up) * dragMovement.z;

                CamT.position += worldDrag;
                lastMousePosition = currentMousePosition;
            }
        }
    }
}
