using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    float Speed = 5.0f;
    public Animator AnimController;
    bool timez;
    int ballCounter = 0;
    Rigidbody playerRb;
    float gravityModifier = 2f;
    float jumpCount = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartRun();
            AnimController.SetBool("IsRun", true);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            StartRun();
            AnimController.SetBool("IsRun", true);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            StartRun();
            AnimController.SetBool("IsRun", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            StartRun();
            AnimController.SetBool("IsRun", true);

        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            AnimController.SetBool("IsRun", false);
        }
        jumpplayer();
    }

    void StartRun()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
    private void jumpplayer()
    {
        if (jumpCount < 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                jumpCount++;
                AnimController.SetTrigger("Jump");
               
            }
        }
    
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Cone") && (ballCounter == 4))
        {
            Debug.Log("Touched Cone Start Countdown 10 seconds");
            timez = true;
        }
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("touched ball");
            Destroy(other.gameObject);
            ballCounter++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlane") || collision.gameObject.CompareTag("Bridge"))
        {
            jumpCount = 0f;
            AnimController.SetBool("Jump", false);
            AnimController.SetBool("JumpCheck", true);
        }
        if (collision.gameObject.CompareTag("QuestionMark"))
        {
            SceneManager.LoadScene("WinScene");
        }
    }

}
