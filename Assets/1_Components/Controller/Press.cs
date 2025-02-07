using UnityEngine;

public partial class CameraController
{
    public class Press : ControllerModule
    {
        private readonly Camera mainCamera;

        public Press(CameraController controller) : base(controller)
        {
            mainCamera = Camera.main;
        }

        internal override void Handle()
        {
            if (!enabled) return;
            if (!ShouldProcessInput()) return;


            if (Map.FindAction("Press").WasPressedThisFrame())
            {
                Vector2 pressPosition = Map.FindAction("PressPosition").ReadValue<Vector2>();

                Ray ray = mainCamera.ScreenPointToRay(pressPosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent<IPressObject>(out var pressObject))
                    {
                        pressObject.OnPress();
                    }

                }
            }
        }
    }
}


public interface IPressObject
{
    public void OnPress();
}