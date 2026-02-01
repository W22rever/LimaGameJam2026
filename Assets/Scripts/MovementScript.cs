using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb2D;
    [SerializeField] private float speed = 5f;

    private Vector2 moveDirection;

    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    [Header("InputMaps")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference dash;

    private bool canDash = true;
    private bool canMove = true;

    private bool isFacingRight = true;

    private Animator animator;


    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            moveDirection = move.action.ReadValue<Vector2>();

           // if (moveDirection.x > 0 && !isFacingRight) Flip();
           // else if (moveDirection.x < 0 && isFacingRight) Flip();
        }
    }
        
    private void FixedUpdate()
    {
        if(canMove)
        {
            AnimationHandler.MovementAnim(animator, moveDirection);
            rb2D.linearVelocity = new Vector2(moveDirection.x * speed, rb2D.linearVelocity.y);
        }
    }

    private void OnDashing(InputAction.CallbackContext context)
    {
        if (!canDash) return;

        string controlName = context.control.name.ToLower();

        float dashDirection = 0f;

        if (controlName.Contains("right"))
        {
            dashDirection = 1f;
        }
        else if (controlName.Contains("left"))
        {
            dashDirection = -1f;
        }

        bool correctRight = (dashDirection > 0 && isFacingRight);

        bool correctLeft = (dashDirection < 0 && isFacingRight);

        if (correctRight || correctLeft)
        {
            StartCoroutine(DashingRoutine(dashDirection));
        }
    }

    private IEnumerator DashingRoutine(float direction)
    {   
        canDash = false;
        canMove = false;

        AnimationHandler.DashAnim(animator);

        rb2D.linearVelocity = new Vector2(direction * dashForce, 0f);

        yield return new WaitForSeconds(dashDuration);


        rb2D.linearVelocity = Vector2.zero;

        canMove = true;

        yield return new WaitForSeconds(0.5f); // Cooldown para no del dash

        canDash = true;
    }

    /*private void Dashing(float direction)
    {
        canDash = false;
        canMove = false;

        

        rb2D.linearVelocity = new Vector2(direction * dashForce, 0f);

        //rb2D.linearVelocity = Vector2.zero;

        canMove = true;
        canDash = true;

        
    }/*

   /* private bool Flip()
    {
        isFacingRight = !isFacingRight;
        if (isFacingRight) transform.localScale = new Vector3(1,1,1);
        else transform.localScale = new Vector3(-1,1,1);
            Debug.Log(isFacingRight);
        return isFacingRight;
        
    }*/

    private void OnEnable()
    {
        dash.action.performed += OnDashing;
        
        dash.action.Enable();
        move.action.Enable();
    }

    private void OnDisable()
    {
        dash.action.performed -= OnDashing;
        
        dash.action.Disable();
        move.action.Disable();
    }
}

