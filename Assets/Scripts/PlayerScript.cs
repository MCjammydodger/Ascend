using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour {

    [SerializeField]
    private float walkSpeed = 4;
    [SerializeField]
    private float strafeSpeed = 4;
    [SerializeField]
    private float climbSpeed = 4;
    [SerializeField]
    private float rotationSensitivity = 4;
    [SerializeField]
    private float jumpSpeed = 4;
    [SerializeField]
    private float fallSpeed = 4;
    [SerializeField]
    private float timeToJump = 1;
    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private Image fadeScreen;
    [SerializeField]
    private ScoreScript scoreScript;
    private CharacterController characterController;
    private float timeSinceJumpStart = 0;
    private bool jumping = false;
    private bool climbingLadder = false;
    private Vector3 movement;
    private float fadeLerp = 0;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
	// Update is called once per frame
	private void Update () {
        if (fadeLerp < 1)
        {
            fadeScreen.color = new Color(255, 255, 255, Mathf.Lerp(1, 0, fadeLerp));
            fadeLerp += Time.deltaTime;
            return;
        }
        CheckForInputs();
	}

    private void CheckForInputs()
    {
        movement = new Vector3();
        if (climbingLadder)
        {
            Climb(Input.GetAxis("Vertical"));
        }
        else
        {
            Walk(Input.GetAxis("Vertical"));
            Fall();

        }
        Strafe(Input.GetAxis("Horizontal"));
        Rotate(Input.GetAxis("Mouse X"));
        if (Input.GetButton("Jump") && (characterController.isGrounded || climbingLadder))
        {
            timeSinceJumpStart = 0;
            jumping = true;
        }
        Jump();
        characterController.Move(movement);
    }

    private void Walk(float input)
    {
        movement += transform.forward * walkSpeed * input * Time.deltaTime;
    }

    private void Strafe(float input)
    {
        movement += transform.right * strafeSpeed * input * Time.deltaTime;
    }

    private void Rotate(float input)
    {
        transform.Rotate(new Vector3(0, input * rotationSensitivity * Time.deltaTime, 0));
    }
    private void Jump()
    {
        if (jumping)
        {
            movement = transform.up * jumpSpeed * Time.deltaTime;
        }
        timeSinceJumpStart += Time.deltaTime;


    }
    private void Fall()
    {
        if(timeSinceJumpStart >= timeToJump || !jumping)
        {
            movement += transform.up * -fallSpeed * Time.deltaTime;
            jumping = false;
        }
    }
    private void Climb(float input)
    {
        if (characterController.isGrounded && input < 0)
        {
            Walk(input);
        }
        else
        {
            movement += transform.up * climbSpeed * input * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ladder")
        {
            climbingLadder = true;
            transform.SetParent(other.transform);
        }
        if(other.tag == "Laser")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(other.tag == "Bullet")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (other.tag == "MovingPlatform")
        {
            transform.SetParent(other.transform);
        }
        if(other.tag == "Finish")
        {
            scoreScript.Finish();
            endScreen.SetActive(true);
            this.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            climbingLadder = false;
            transform.SetParent(null);
        }
        if (other.tag == "MovingPlatform")
        {
            transform.SetParent(null);
        }
    }
}
