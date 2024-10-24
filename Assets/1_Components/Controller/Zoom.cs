using UnityEngine;

public partial class CameraController
{
    public class Zoom : ControllerModule
    {
        [SerializeField] private float zoomSpeed = 7f;
        [SerializeField] private float minZoomDistance = 5f;
        [SerializeField] private float maxZoomDistance = 100f;

        private Vector3 focusPoint;

        public Zoom(CameraController controller) : base(controller) { }

        internal override void Handle()
        {
            if (!enabled) return;

            float scrollInput = Map.FindAction("Zoom").ReadValue<Vector2>().y;

            Vector3 zoomDirection = scrollInput * Time.deltaTime * zoomSpeed * CamT.forward;
            Vector3 newCameraPosition = CamT.position + zoomDirection;

            float distanceToFocus = Vector3.Distance(newCameraPosition, focusPoint);
            if (distanceToFocus >= minZoomDistance && distanceToFocus <= maxZoomDistance)
            {
                CamT.position = newCameraPosition;
            }
        }
    }
}
