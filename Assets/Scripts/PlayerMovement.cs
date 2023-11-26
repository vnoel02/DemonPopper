using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 8;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce = 375.0f;
    [SerializeField] bool isGrounded = true;

    [SerializeField] Animator animator;

    const int IDLE = 0;
    const int ACTION = 1;
    


    public ArrowPath ArrowPrefab;
    public Transform LaunchOffset;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

        animator.SetInteger("Motion", IDLE);
        
    }

    // Update is called once per frame --used for user input
    //do NOT use for physics & movement
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W))
            jumpPressed = true;

        
        //firing projectile
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool("Shoot",true);
            Instantiate(ArrowPrefab, LaunchOffset.position, transform.rotation);
        } else {
            animator.SetBool("Shoot",false);
        }
        
            
        
    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(SPEED * movement, rigid.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
        if (jumpPressed && isGrounded)
            Jump();
        else {
            if (movement > 0 || movement < 0) {
                animator.SetInteger("Motion", ACTION);
            } else if (movement == 0 ){
                animator.SetInteger("Motion", IDLE);
            }

        }
            // jumpPressed = false;
        }
       

        
    

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(0, jumpForce));
        Debug.Log("jumped");
        jumpPressed = false;
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "OneWayPlatform") {
            isGrounded = true;
        } else if (collision.gameObject.tag == "Distractor") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else
            Debug.Log(collision.gameObject.tag);
    }
}
