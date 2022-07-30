using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;

    public Player inputAction;
    public PlayerManager playerManager;

    [HideInInspector] public Vector2 movementInput;

    private void Awake()
    {
        playerManager = PlayerManager.instance;
    }

    private void OnEnable()
    {
        if (inputAction == null)
        {
            inputAction = new Player();
            inputAction.PlayerActions.Move.performed +=
                inputAction => movementInput = inputAction.ReadValue<Vector2>();
        }
        
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    public void TickInput()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal)+ Mathf.Abs(vertical));
    }

    public void HandleAttack()
    {
        playerManager.animationHandler.PlayTargetAnimation("LightAttack",true);
    }
}
