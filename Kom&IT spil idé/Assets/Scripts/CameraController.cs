using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public static CameraController instance;
    [HideInInspector] public Transform followTransform;

    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float rotationAmount;
    [SerializeField] private float zoomAmount;
    [SerializeField] private float smoothTime;
    private float movementSpeed;
    private Vector3 zoomVector;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 newZoom;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    private void Start()
    {
        instance = this;

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = Camera.main.transform.localPosition;
        zoomVector = new Vector3(0, -zoomAmount, zoomAmount);
    }

    private void Update()
    {
        // Handle Inputs
        HandleKeyboardInput();
        HandleMouseInput();

        // Speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        // Follow
        if (followTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, followTransform.position, Time.deltaTime * smoothTime);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    private void HandleKeyboardInput()
    {
        // Movement
        if (followTransform == null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                newPosition += transform.forward * movementSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newPosition -= transform.right * movementSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newPosition -= transform.forward * movementSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newPosition += transform.right * movementSpeed;
            }

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smoothTime);
        }

        // Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * smoothTime);

        // Zoom
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomVector;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomVector;
        }

        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, newZoom, Time.deltaTime * smoothTime);
    }

    private void HandleMouseInput()
    {
        // Movement
        if (followTransform == null)
        {
            if (Input.GetMouseButtonDown(2))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }
            if (Input.GetMouseButton(2))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);

                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }
        }

        // Rotation
        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }

        // Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomVector * 10;
        }
    }
}
