using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

/// <summary>
/// File Name: PlayerController.cs
/// Author: Seoyoung
/// Last Modified by: Seoyoung
/// Date Last Modified: Nov. 3, 2019
/// Description: Controller for the Player prefab
/// </summary>

public class PlayerController : MonoBehaviour
{
    public HeroAnimState heroAnimState;

    [Header("Object Properties")]

    public Animator heroAnimator;
    public SpriteRenderer heroSpriteRenderer;
    public Rigidbody2D heroRigidBody;

    [Header("Physics Related")]
    public float moveForce;
    public float jumpForce;

    public bool isGrounded;
    public Transform groundTarget;

    public Vector2 maximumVelocity = new Vector2(x: 20.0f, y: 30.0f);

    [Header("Sounds")]
    public AudioSource jumpSound;
    public AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        heroAnimState = HeroAnimState.IDLE;
        isGrounded = false;

    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        isGrounded = Physics2D.BoxCast(
                 transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f, 1 << LayerMask.NameToLayer("Ground"));

        //Idle State
        if (Input.GetAxis("Horizontal") == 0)
        {
            heroAnimState = HeroAnimState.IDLE;
            heroAnimator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
        }

        //Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            heroSpriteRenderer.flipX = false;
            if (isGrounded)
            {
                heroAnimState = HeroAnimState.WALK;
                heroAnimator.SetInteger(name: "AnimState", value: (int)HeroAnimState.WALK);
                heroRigidBody.AddForce(Vector2.right * moveForce);

            }
        }


        //Move Leftt
        if (Input.GetAxis("Horizontal") < 0)
        {
            heroSpriteRenderer.flipX = true;
            if (isGrounded)
            {

                heroAnimState = HeroAnimState.WALK;
                heroAnimator.SetInteger(name: "AnimState", value: (int)HeroAnimState.WALK);
                heroSpriteRenderer.flipX = true;
                heroRigidBody.AddForce(Vector2.left * moveForce);

            }
        }

        //Jump
        if ((Input.GetAxis("Jump") > 0) && (isGrounded))
        {
            heroAnimState = HeroAnimState.JUMP;
            heroAnimator.SetInteger(name: "AnimState", value: (int)HeroAnimState.JUMP);
            heroRigidBody.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            jumpSound.Play();
        }

        heroRigidBody.velocity = new Vector2(
            x: Mathf.Clamp(value: heroRigidBody.velocity.x, min: -maximumVelocity.x, max: maximumVelocity.x),
            y: Mathf.Clamp(value: heroRigidBody.velocity.y, min: -maximumVelocity.y, max: maximumVelocity.y)
            );
    }

    //coin 
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Coin"))
        {


            // update the scoreboard - add points
            coinSound.Play();
            Destroy(other.gameObject);
            gameManager.instance.AddScore(100);

        }

        if (other.gameObject.CompareTag("BigCoin"))
        {

            // update the scoreboard - add points
            
            Destroy(other.gameObject);
            gameManager.instance.AddScore(500);

        }


    }

}
