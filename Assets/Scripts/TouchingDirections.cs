using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <TouchingDirections>
/// Uses the collider to check directions to see if the object is on the ground, air, or touching the ceiling 
public class TouchingDirections : MonoBehaviour {

    // variables
    public ContactFilter2D contactFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    // Components
    CapsuleCollider2D touchingCollider;
    Rigidbody2D rb2D;
    Animator animator;

    // Raycast hits
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];



    /// Properties ----------------------------------
    [SerializeField]
    private bool _isGround; 
    public bool isGround  { 
        get { return _isGround; } // get

        private set {
            _isGround = value; 
            animator.SetBool(AnimatorStrings.isGrounded, value); 
        } // private set

    } // isGround

    private bool _IsOnWall;
    public bool IsOnWall
    {
        get { return _IsOnWall; } // get

        private set
        {
            _IsOnWall = value;
            animator.SetBool(AnimatorStrings.IsOnWall, value);
        } // private set

    } // isGround

    private bool _IsOnCeiling;
    public bool IsOnCeiling
    {
        get { return _IsOnCeiling; } // get

        private set
        {
            _IsOnCeiling = value;
            animator.SetBool(AnimatorStrings.isOnCeiling, value);
        } // private set

    } // isGround
    /// Properties ----------------------------------

    // Lambdas
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;


    private void Awake() {
        // grabbing the colliders
        rb2D = GetComponent<Rigidbody2D>();
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

    } // Awake

    void FixedUpdate() {
        isGround = touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;

    } // FixedUpdate

} // TouchingDirections
