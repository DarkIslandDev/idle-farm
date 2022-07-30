using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Instance

    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("у тебя тут где-то ещё один такой же скрипт. Найди его быстрее и удали к хуям собачим, а может быть лишний скрипт это Я.");
            return;
        }

        instance = this;
    }

    #endregion

    [HideInInspector] public InputHandler inputHandler;
    [HideInInspector] public PlayerLocomotion playerLocomotion;
    [HideInInspector] public AnimationHandler animationHandler;
   
    public Transform wheatHolder;
    public List<Transform> whBlocks;

    public GameObject blade;

    private Animator animator;
    
    [Header("Player Flags")]
    public bool isInteracting;
    

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();

        animator = GetComponentInChildren<Animator>();
        animationHandler = GetComponentInChildren<AnimationHandler>();

        blade.SetActive(false);
        
        isInteracting = false;
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = animator.GetBool("isInteracting");
        
        inputHandler.TickInput();
        
        playerLocomotion.HandleMovement(delta);
    }

    public void SetupPositionForBlock()
    {
        for (int i = 1; i < whBlocks.Count; i++)
        {
            whBlocks[i].localPosition = new Vector3(0, 
                whBlocks[i - 1].localPosition.y + whBlocks[i].localScale.y, 0);
        }
    }
}