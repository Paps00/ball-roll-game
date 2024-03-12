
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 3f;
    private Rigidbody rigid;
    private int count;
    private bool isGrounded; // Added variable to track grounding
    public Text scoreText;
    public Text winText;
    public AudioSource collideSoundEffect;
    public AudioSource jumpSoundEffect;
    public GameObject particlePrefab; // Assign your Particle System Prefab in the Unity Editor

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
    }

    void Update()
    {
        // Check for grounding in the Update method
        isGrounded = Mathf.Abs(rigid.velocity.y) < 0.001f;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigid.AddForce(movement * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpSoundEffect.Play();
        }
    }
   

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            count++;
            scoreText.text = "Score: " + count;
            collideSoundEffect.Play();

            // Instantiate and play the particle system
            Instantiate(particlePrefab, transform.position, Quaternion.identity);

            if (count >= 10)
            {
                winText.text = "Congratulations! You have Won";
                StartCoroutine(MenuScene());
            }
        }
    }
       private IEnumerator MenuScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }
   
}
