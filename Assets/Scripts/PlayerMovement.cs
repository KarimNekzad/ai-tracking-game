using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    // Method 1 - moving using velocity
    //[SerializeField] private float speed = 300.0f;
    // Method 2 - moving using MovePosition
    [SerializeField] private float moveSpeed = 7.0f;

    [SerializeField] private Vector2 movement;
    [SerializeField] private Vector2 _movementDirection;

    private Animator _animator;
    private const int _idle = 0;
    private const int _moving = 1;
    private const int _takeDamage = 2;
    private PlayerBulletCollision _playerBulletCollision;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerBulletCollision = GetComponent<PlayerBulletCollision>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (!_playerBulletCollision.IsTakingDamage())
        {
            if (movement.x > 0.1 || movement.x < -0.1 || movement.y > 0.1 || movement.y < -0.1)
            {
                _animator.SetInteger("movementState", _moving);
            }
            else
            {
                _animator.SetInteger("movementState", _idle);
            }
        }
        else
        {
            _animator.SetInteger("movementState", _takeDamage);
        }
    }

    private void FixedUpdate()
    {
        // Method 1 - moving using velocity
        //_rigidbody2D.velocity = new Vector2(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime);

        // Method 2 - moving using MovePosition
        _rigidbody2D.MovePosition(_rigidbody2D.position + movement * moveSpeed * Time.fixedDeltaTime);
        Flip();

        //if (movement.x > 0.1 || movement.x < -0.1 || movement.y > 0.1 || movement.y < -0.1)
        //{
        //    _animator.SetInteger("movementState", _moving);
        //}
        //else
        //{
        //    _animator.SetInteger("movementState", _idle);
        //}
    }

    private void Flip()
    {
        if (movement.x == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270f);
            _movementDirection.x = 1;
            _movementDirection.y = 0;
        }
        else if (movement.x == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90f);
            _movementDirection.x = -1;
            _movementDirection.y = 0;
        }
        else if (movement.y == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0f);
            _movementDirection.x = 0;
            _movementDirection.y = 1;
        }
        else if (movement.y == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180f);
            _movementDirection.x = 0;
            _movementDirection.y = -1;
        }
    }

    public Vector2 GetMovementDirection()
    {
        if (_movementDirection.x != 0 || _movementDirection.y != 0)
        {
            return _movementDirection;
        }
        else
        {
            Vector2 defaultMovementDirection = new Vector2(0, 1);
            return defaultMovementDirection;
        }
    }
}
