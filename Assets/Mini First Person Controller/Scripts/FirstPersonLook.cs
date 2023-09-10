using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2f;
    public float smoothing = 1.5f;

    Vector2 mouseLook;
    Vector2 smoothV;

    void Start()
    {
        // Desbloquear el cursor del mouse para que sea visible y se pueda mover libremente.
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Obtener el movimiento del mouse.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        // Aplicar suavizado al movimiento del mouse.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
        mouseLook += smoothV;

        // Limitar la rotación vertical para evitar giros excesivos.
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        // Aplicar las rotaciones a la cámara y el personaje.
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.up);
        character.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.right);
    }
}
