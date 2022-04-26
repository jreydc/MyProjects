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

   
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();//getting the Rigidbody component of the Player gameobject
        anime2r = GetComponent<Animator>();//getting the Animator component of the Player gameobject
        scaleRight = true;

    }

    // FixedUpdate is called in a fixed cycle
    void FixedUpdate()
    {
  
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
