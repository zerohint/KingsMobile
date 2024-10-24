using UnityEngine;
using UnityEngine.InputSystem;

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
        /// Runs on update
        /// </summary>
        internal abstract void Handle();
    }
}