using UnityEngine;
using UnityEngine.InputSystem;

public partial class CameraController : MonoBehaviour
{
    public Move move;
    public Zoom zoom;
    public Press press;

    [SerializeField] private InputActionAsset inputAction;

    private void Awake()
    {
        move = new Move(this);
        zoom = new Zoom(this);
        press = new Press(this);
    }


    private void Update()
    {
        move.Handle();
        zoom.Handle();
        press.Handle();
    }


    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}
