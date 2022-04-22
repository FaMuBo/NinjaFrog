using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float jumpForce; //variable for jump force
    public float heroSpeed; //variable for hero speed
    Animator anim;          //Animation for hero move
    Rigidbody2D rgdBody;
    public bool directionToRight = true; //variable for check direction
    private bool onTheGround;    //varibale for check if object is on the ground (jump correction)
    public Transform groundTester; //variable for test colision with ground
    private float radius = 0.1f;   //radius of testing circle
    public LayerMask layersToTest;  //variable for new layer to test collision with ground
    public GameObject spawnPoint;
    public GameObject checkpoint;
    public bool checkpointTest = false;
    
    //public AudioClip jump_clip;

    public GameObject bulletObject;
    public Transform shootPosition;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //get into animator component
        rgdBody = GetComponent<Rigidbody2D>();//get into Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("cactusContact"))
        {
            rgdBody.velocity = Vector2.zero;
            return;
        } 
        
        onTheGround = Physics2D.OverlapCircle(groundTester.position, radius, layersToTest);// check if character is on the ground
        
        if (Input.GetKeyDown(KeyCode.Space) && onTheGround) //if statment check if Space is pressed and character is on the ground
        {

            rgdBody.AddForce(new Vector2(0f, jumpForce));
            anim.SetTrigger("jump");
            //AudioSource.PlayClipAtPoint(jump_clip, transform.position);
        }
        float horizontalMove = Input.GetAxis("Horizontal"); //check if we press rihgt or left arrow 
        anim.SetFloat("speed",Mathf.Abs(horizontalMove)); //sign speed value into horizontalMove variable
        rgdBody.velocity = new Vector2(horizontalMove * heroSpeed, rgdBody.velocity.y);
        
        if(horizontalMove<0 && directionToRight)
         {
             Flip();
         }
         if(horizontalMove>0 &&!directionToRight)
         {
             Flip();
         }

         if (Input.GetKeyDown(KeyCode.E))
         {
             Shoot();
         }
     }

    void Shoot()
    {
        var newBullet = Instantiate(bulletObject, shootPosition.position, shootPosition.rotation);
        Destroy(newBullet, 2f);
    }
    void Flip()
    {
        directionToRight = !directionToRight;
        
        transform.Rotate(0f, 180f, 0f);
        //Vector3 heroScale = gameObject.transform.localScale;
        //heroScale.x *= -1;
        //gameObject.transform.localScale = heroScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.FinishGame();
        }
        if (col.gameObject.CompareTag("Checkpoint"))
        {
            checkpointTest = true;
        }
    }
}
