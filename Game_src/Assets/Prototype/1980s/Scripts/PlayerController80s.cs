using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController80s : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] float speed = 8f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;

    public TMP_Text sceneChangeText;
    public bool fadeOut;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collider Entered");
        sceneChangeText.gameObject.SetActive(true);

        if (Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene("1980s_Interview");
            fadeOut = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collier Exit");
        sceneChangeText.gameObject.SetActive(false);
    }

}
