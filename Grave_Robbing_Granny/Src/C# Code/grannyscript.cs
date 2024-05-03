using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class grannyscript : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce = 50f; // Force of the jump
    public float fallMultiplier = 2.5f; // To make character fall faster
    public float lowJumpMultiplier = 2f; // To control height of the jump when moving left or right
    private float lastYPosition; // Last y position of the character
    private float timeSinceLastYChange; // Time since the last change in y position
    public inventoryupdate inventory;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite idleSprite;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private bool isWalking = false; // To check if the character is walking
    public bool isGrounded;
    public AudioSource jumpSound1;
    public AudioSource gruntSound;
    public AudioSource gobletSound;
    public int jumpSoundVOLUME;
    public int gruntSoundVOLUME;
    public int gobletSoundVOLUME;


    void Start()
    {
        gameObject.name = "GRANNY";
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
        lastYPosition = transform.position.y;
        timeSinceLastYChange = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component


    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a goblet
        if (collision.gameObject.CompareTag("Goblet"))
        {
            // Call OnGobletCollected method from the Inventory script
            inventory.OnGobletCollected();

            // Destroy the goblet
            Destroy(collision.gameObject);

            gobletSound.Play();
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            // restart if touching death objects
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Check if the character is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set isGrounded to true when touching the ground
        }
    }


    void Update()
    {

        if (inventory.currentGoblets >= inventory.maxGoblets)  // Change this line
        {
            // Load the congratulations scene
            SceneManager.LoadScene("end scene");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
    {
        if (!isWalking)
        {
            StartCoroutine(WalkAnimation());
        }
    }
    else
    {
        if (isWalking)
        {
            StopCoroutine(WalkAnimation());
            isWalking = false;
            spriteRenderer.sprite = idleSprite; // Set to idle sprite when not moving
        }
    }


        // Check if y position has changed
        if (transform.position.y != lastYPosition)
        {
            lastYPosition = transform.position.y;
            timeSinceLastYChange = 0f;
        }
        else
        {
            timeSinceLastYChange += Time.deltaTime;
        }

        // Move right
        if (Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                myRigidbody.velocity = new Vector2(runSpeed, myRigidbody.velocity.y);
            }

            else
            {
                myRigidbody.velocity = new Vector2(walkSpeed, myRigidbody.velocity.y);
            }

            transform.rotation = Quaternion.Euler(0, 0, 0); // Face right
        }

        // Move left
        if (Input.GetKey(KeyCode.A))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                myRigidbody.velocity = new Vector2(-runSpeed, myRigidbody.velocity.y);
            }

            else
            {
                myRigidbody.velocity = new Vector2(-walkSpeed, myRigidbody.velocity.y);
            }

            transform.rotation = Quaternion.Euler(0, 180, 0); // Face left
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            timeSinceLastYChange = 0f;
            isGrounded = false; // Set isGrounded to false when jumping

            // Play jump sounds
            jumpSound1.Play();
            gruntSound.Play();

        }

        // Make character fall faster
        if (myRigidbody.velocity.y < 0)
        {
            myRigidbody.velocity += new Vector3(0, Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime, 0);
        }
        else if (myRigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            myRigidbody.velocity += new Vector3(0, Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime, 0);
        }
    }

    IEnumerator WalkAnimation()
    {
        isWalking = true;
        while (isWalking)
        {
            spriteRenderer.sprite = sprite1;
            yield return new WaitForSeconds(1f / 12f); // Wait for 12 frames
            spriteRenderer.sprite = sprite2;
            yield return new WaitForSeconds(1f / 12f); // Wait for 12 frames
        }
    }


}
