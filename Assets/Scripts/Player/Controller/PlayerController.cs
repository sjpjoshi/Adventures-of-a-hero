using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    // movement variables 
    [Header("Movement")]
    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public bool _isFacingRight = true;
    private bool _isMoving = false;
    private bool _isRunning = false;

    // Components
    Rigidbody2D myRb;
    Animator animator;

    /// <>
    /// Properties
    /// <>
    public bool IsMoving  
    {
        get { return _isMoving; } // get 

        private set
        {
            _isMoving = value;
            animator.SetBool(AnimatorStrings.isMoving, value);
        } // private set 

    } // IsMoving

    public bool IsRunning 
    {
        get { return _isRunning; } // get

        private set 
        { 
            _isRunning = value;
            animator.SetBool(AnimatorStrings.isRunning, value); 
        } // private set

    } // IsRunning 

    public float CurrentMoveSpeed 
    {
        get 
        {
            if (IsMoving)
            {
                if (IsRunning)
                    return runSpeed;
                else
                    return walkSpeed;

            } // if
            else
                return 0; // Idle Speed is 0

        } // get

    } // CurrentMoveSpeed

    public bool isFacingRight 
    {
        get { return _isFacingRight; } // get
        private set 
        {
            if (_isFacingRight != value) 
                transform.localScale *= new Vector2(-1, 1); // Flip the local scale to make the player face the opposite direction

            _isFacingRight = value;

        } // private set

    } // isFacingRight

    /// <>
    /// On Compile Time 
    /// <>

    void Awake() {
        myRb = GetComponent<Rigidbody2D>(); // our rb2D
        animator = GetComponent<Animator>(); // Animator

    } // Awake
    void Start() {} // Start
    void Update() {} // Update
    void FixedUpdate() {

        //myRb.velocity = new Vector2(moveInput.x * walkSpeed * time.fixedDeltaTime, myRb.velocity.y); Unity Docs state that fixedDeltaTime is now built into R.B 2D
        myRb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, myRb.velocity.y); // move on X only, y is affected by gravity

    } // FixedUpdate

    /// <>
    /// Invoke Methods
    /// <>
    public void OnMove(InputAction.CallbackContext context) { 
        moveInput = context.ReadValue<Vector2>(); // x and y movement
        IsMoving = moveInput != Vector2.zero; // ensure movement is possible
        SetFacingDirection(moveInput);

    } // OnMove

    private void SetFacingDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !isFacingRight)
            isFacingRight = true;
        else if (moveInput.x < 0 && isFacingRight)
            isFacingRight = false;

    } // SetFacingDirection

    public void OnRun(InputAction.CallbackContext context) {
        if (context.started) 
            IsRunning = true;  
        else if (context.canceled) 
            IsRunning = false;
        
    } // OnRun

} // PlayerController
