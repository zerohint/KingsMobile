using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public partial class CameraController
{
    [System.Serializable]
    public abstract class ControllerModule
    {
        public bool enabled = true;
        protected readonly CameraController controller;

        protected Transform CamT => controller.transform;
        protected InputActionMap Map => controller.inputAction.actionMaps[0];

        public ControllerModule(CameraController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Input i�lemlerinin devam edebilmesi i�in farenin ekran i�inde olup UI �zerinde olmamas� gerekir.
        /// </summary>
        protected bool ShouldProcessInput()
        {
            // Farenin pozisyonunu kontrol ediyoruz
            Vector2 mousePos = Mouse.current.position.ReadValue();
            bool withinScreen = mousePos.x >= 0 && mousePos.y >= 0 && mousePos.x <= Screen.width && mousePos.y <= Screen.height;

            // UI �zerinde olup olmad���n� kontrol ediyoruz
            bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

            return withinScreen && !overUI;
        }

        /// <summary>
        /// Update s�ras�nda �al��acak metod
        /// </summary>
        internal abstract void Handle();
    }
}
