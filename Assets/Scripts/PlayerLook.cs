using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensitivity = 3.5f;
    public float smoothing = 1.5f;

    private Vector2 mouseLook;
    private Vector2 smoothMovement;

    private GameObject player;

    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector2 mDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mDir.x *= sensitivity * smoothing;
        mDir.y *= sensitivity * smoothing;

        smoothMovement.x = Mathf.Lerp(smoothMovement.x, mDir.x, 1f / smoothing);
        smoothMovement.y = Mathf.Lerp(smoothMovement.y, mDir.y, 1f / smoothing);

        mouseLook += smoothMovement;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -80f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        player.transform.rotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
