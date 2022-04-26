using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Player Movement Declarations
    private Rigidbody2D rb2d;// reference to the Player's Rigidbody component
    private Animator anime2r;//reference to the Player's Animator component
    private bool scaleRight;//indicator if the Player's gameobject is facing right
    [SerializeField]private float speed;// speed multiplicator for the Player's velocity
    #endregion

    #region Player Ground Checking Declarations
    [SerializeField]private bool isGrounded; //Linecast variable & indicator returning value if the Player is in the ground
    [SerializeField]private float jumpForce; //jump force multiplicator for the Player's jump height
    [SerializeField]private Transform groundCheck;//Endpoint location indicator for the Linecast
    [SerializeField]private LayerMask playerMask;//To identify the player 
    #endregion
   
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();//getting the Rigidbody component of the Player gameobject
        anime2r = GetComponent<Animator>();//getting the Animator component of the Player gameobject
        scaleRight = true;
        isGrounded = false;
    }

    // FixedUpdate is called in a fixed cycle
    void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(rb2d.transform.position, groundCheck.position, playerMask);//Linecast
        anime2r.SetBool("jumped", !isGrounded);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            JumpMovement();
        }

        float hInput = Input.GetAxis("Horizontal"); //always checks whether we pressed Horizontal buttons

        HandleMovements(hInput);// called method for PlayerMovements
        Scaling(hInput);//called method for Scaling (facing left-right)   
    }

    private void HandleMovements(float h)
    {   //computation of velocity | force
        rb2d.velocity = new Vector2(h * speed, rb2d.velocity.y);
        //rb2d.AddForce((transform.right * h) * speed);
        anime2r.SetFloat("move", Mathf.Abs(h)); //setting the float (move) parameters in Animator
    }

        private void JumpMovement()
    {
        rb2d.velocity = Vector2.up * jumpForce;//computation of the height of the jump using velocity
    }

    private void Scaling(float h)
    {
        if ((h > 0 && !scaleRight) || (h < 0 && scaleRight))
        {
            scaleRight = !scaleRight;
            //scale computation

            Vector3 scale = transform.localScale;

            scale.x *= -1;

            transform.localScale = scale;
        }
    }
}
