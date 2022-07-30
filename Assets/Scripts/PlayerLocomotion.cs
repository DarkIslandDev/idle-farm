using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private Transform cameraObject;
    private InputHandler inputHandler;
    private PlayerManager playerManager;

    public Vector3 moveDirection;

    [HideInInspector] public Transform myTransform;
    [HideInInspector] public AnimationHandler animationHandler;

    public new Rigidbody rigidbody;
    
    [Header("Статы")] 
    [SerializeField] public float movementSpeed = 5;
    [SerializeField] private float rotationSpeed = 10;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        
        playerManager = PlayerManager.instance;
        
        animationHandler = GetComponentInChildren<AnimationHandler>();

        if (Camera.main != null) cameraObject = Camera.main.transform;
        myTransform = transform;
        animationHandler.Initialize();
    }
    
    #region Movement

    private Vector3 normalVector;
    private Vector3 targetPosition;

    public void HandleMovement(float delta)
    {
        if (playerManager.isInteracting)
        {
            return;
        }

        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        Vector3 projectiveVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectiveVelocity;
        
        animationHandler.UpdateAnimatorValues(inputHandler.moveAmount,
            0);

        if (animationHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    public void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = myTransform.forward;
        }

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }
    
    #endregion
}