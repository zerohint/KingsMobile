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
        /// Input iþlemlerinin devam edebilmesi için farenin ekran içinde olup UI üzerinde olmamasý gerekir.
        /// </summary>
        protected bool ShouldProcessInput()
        {
            // Farenin pozisyonunu kontrol ediyoruz
            Vector2 mousePos = Mouse.current.position.ReadValue();
            bool withinScreen = mousePos.x >= 0 && mousePos.y >= 0 && mousePos.x <= Screen.width && mousePos.y <= Screen.height;

            // UI üzerinde olup olmadýðýný kontrol ediyoruz
            bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

            return withinScreen && !overUI;
        }

        /// <summary>
        /// Update sýrasýnda çalýþacak metod
        /// </summary>
        internal abstract void Handle();
    }
}
