using UnityEngine;
public partial class CameraController
{
    public class Rotate : ControllerModule
    {
        [SerializeField] private float rotationSpeed = 40f;

        public Rotate(CameraController controller) : base(controller) { }

        internal override void Handle()
        {
            if (!enabled) return;

            if (Map.FindAction("Rotate").ReadValue<float>() > 0)
            {
                Vector2 rotateInput = Map.FindAction("MouseDelta").ReadValue<Vector2>();

                float yaw = rotateInput.x * rotationSpeed * Time.deltaTime;
                float pitch = -rotateInput.y * rotationSpeed * Time.deltaTime;

                CamT.RotateAround(Vector3.zero, Vector3.up, yaw); // yaw
                CamT.RotateAround(Vector3.zero, CamT.right, pitch); // pitch
            }
        }
    }
}
