using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Camera cameraObject;
    public PlayerManager player;
    [SerializeField] private Transform cameraPivotTransform;

    [Header("Camera Settings")] 
    private float cameraSmoothSpeed = 1; //The bigger the number, the longer it takes the camera to reach its position
    [SerializeField] private float upAndDownRotationSpeed = 200;
    [SerializeField] private float leftAndRightRotationSpeed = 220f;
    [SerializeField] private float minimumPivot = -15; //Look down stop point
    [SerializeField] private float maximumPivot = 60; // Look up stop point
    
    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    [SerializeField] private float leftAndRightLookAngle;
    [SerializeField] private float upAndDownLookAngle;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        cameraObject = GetComponentInChildren<Camera>();
    }

    public void HandleAllCameraActions()
    {
        if (player != null)
        {
        // Follow player
        HandleFollowTarget();
        // Rotate around player
        HandleRotations();
        //Collide with objects

        }
        
        
    }

    private void HandleFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void HandleRotations()
    {

        leftAndRightLookAngle += PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed * Time.deltaTime;
        upAndDownLookAngle += PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed * Time.deltaTime;
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        // Rotate Y this game object
        transform.rotation = Quaternion.Euler(0f, leftAndRightLookAngle, 0f);

        // Rotate X for the pivot child
        cameraPivotTransform.localRotation = Quaternion.Euler(upAndDownLookAngle, 0f, 0f);
        
    }
}
