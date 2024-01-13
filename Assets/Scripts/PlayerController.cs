using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float climbingSpeed;
    private BoxCollider2D feetCollider;
    private CapsuleCollider2D bodyCollider;
    private bool _isAlive = true;
    //[SerializeField] private AudioClip deathSong;
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        
    }

    private void Update()
    {
        Move();
        FlipSprite();
        Jump();
        Climbing();
        Die();
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal") ;
        Vector2 horizontalVector = new Vector2(inputX * playerSpeed, _playerRb.velocity.y);
        _playerRb.velocity = horizontalVector;
        bool _hasSpeedX = Mathf.Abs(_playerRb.velocity.x) > Mathf.Epsilon;
        _playerAnimator.SetBool("isRunning",_hasSpeedX);
    }

    void FlipSprite()
    {
        bool hasSpeedX = Mathf.Abs(_playerRb.velocity.x) > Mathf.Epsilon;
        if (hasSpeedX)
        {
            transform.localScale = new Vector2( Mathf.Sign(_playerRb.velocity.x),1f);
        }
    }

    void Jump()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerRb.velocity += new Vector2(0, jumpSpeed);
            bool hasSpeedY = Mathf.Abs(_playerRb.velocity.y) > Mathf.Epsilon;
            _playerAnimator.SetBool("isJumping",hasSpeedY);
        }
    }
    private void Climbing()
    {
        
        if (!(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))))
        {
            _playerRb.gravityScale =3;
            _playerAnimator.SetBool("isClimbing",false);
            return;
        }
        float inputY = Input.GetAxisRaw("Vertical");
        _playerRb.velocity = new Vector2(_playerRb.velocity.x, inputY * climbingSpeed);
        _playerRb.gravityScale = 0;
        bool hasSpeedY = Mathf.Abs(_playerRb.velocity.y) > Mathf.Epsilon;
        _playerAnimator.SetBool("isClimbing",hasSpeedY);
    }
    private void Die()
    {
        
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask( "Lava")))
        {
            
            _playerRb.velocity = new Vector2(20, 20);
            
            _playerAnimator.SetTrigger("Death");
            StartCoroutine("DeathTime");

            
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Ladder"))
        {
            _playerAnimator.SetBool("isJumping",false);
        }
    }
    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(1f);
        bodyCollider.gameObject.SetActive(false);
    }
}
