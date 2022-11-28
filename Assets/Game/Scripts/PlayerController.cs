using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float ForwardMoveSpeed;
    public float MaxForwardSpeed;

    public float JumpMoveSpeed;
    public float MaxJumpSpeed;

    public float DanceSpeed;

    public AudioSource CollectGoldSound;
    public float pitch = 1;

    public GameObject playButton;

    private Rigidbody mRigidbody;
    private Renderer playerMaterial;
    private Animator animator;
    private bool jump;
    private bool dance;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        playerMaterial = GetComponentInChildren<Renderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        jump = false;
        dance = false;
        animator.Play("Default");
        gameObject.tag = "Green";
        GameManager.Instance.ColorButtons[0].onClick.AddListener(RedPlayerColor);
        GameManager.Instance.ColorButtons[1].onClick.AddListener(GreenPlayerColor);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.start && !jump && !dance)
        {
            animator.Play("Walk");
            Move();
        }
        if (dance)
        {
            var rot = transform.rotation;
            rot.y = 150;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, DanceSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gold")
        {
            CollectGoldSound.transform.position = other.transform.position;
            CollectGoldSound.Play();
            CollectGoldSound.pitch = pitch;
            pitch += 0.05f;

            if (pitch > 2)
                pitch = 1;

            other.GetComponent<Gold>().Collect();
        }

        if ((other.tag == "Red" && gameObject.tag == "Red") || (other.tag == "Green" && gameObject.tag == "Green"))
        {
            GameManager.Instance.Score++;
        }
        else if (other.tag == "SpeedArrow")
        {
            jump = true;
            animator.Play("RunToStop");
        }
        else if (other.tag == "FinishPlane")
        {
            jump = false;
            dance = true;

            animator.Play("Dance");
            var velocity = mRigidbody.velocity;
            velocity.z = 0;
            mRigidbody.velocity = velocity;
            playButton.SetActive(true);
        }
        else
        {
            var euler = other.GetComponentInParent<Transform>().eulerAngles;
            euler.x = 90;
            other.GetComponentInParent<Transform>().eulerAngles = euler;
        }
    }
    private void RedPlayerColor()
    {
        playerMaterial.material = GameManager.Instance.ColorMaterials[0];
        gameObject.tag = "Red";
    }
    private void GreenPlayerColor()
    {
        playerMaterial.material = GameManager.Instance.ColorMaterials[1];
        gameObject.tag = "Green";
    }

    private void Forward()
    {
        mRigidbody.AddForce(0, 0, ForwardMoveSpeed * 100 * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        mRigidbody.AddForce(0, JumpMoveSpeed * 100 * Time.fixedDeltaTime, 0);
    }
    private void Move()
    {
        if (!jump && !dance)
        {
            Forward();
            ClampVelocity();
        }
    }
    private void ClampVelocity()
    {
        var velocity = mRigidbody.velocity;
        velocity.z = Mathf.Clamp(velocity.z, -MaxForwardSpeed, MaxForwardSpeed);
        velocity.y = 0;
        velocity.x = 0;
        mRigidbody.velocity = velocity;
    }
}