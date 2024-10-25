using UnityEngine;

public partial class CameraController
{
    public class Move : ControllerModule
    {
        [SerializeField] private float moveSpeed = 40f;

        public Move(CameraController controller) : base(controller) { }

        internal override void Handle()
        {
            if (!enabled) return;

            Vector3 moveInput = Map.FindAction("Move").ReadValue<Vector2>();

            Vector3 moveDirection = (CamT.right * moveInput.x) + (CamT.forward * moveInput.y);
            moveDirection.y = 0f;

            CamT.position += moveSpeed * Time.deltaTime * moveDirection;
        }
    }
}