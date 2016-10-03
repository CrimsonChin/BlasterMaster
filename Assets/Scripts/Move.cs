using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public float Speed = 3f;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        bool isWalking = false;
        if (x != 0 || y != 0)
        {
            if (x > 0 && _spriteRenderer.flipX || x < 0 && !_spriteRenderer.flipX)
            {
                Flip();
            }

            _animator.SetFloat("MoveX", x);
            _animator.SetFloat("MoveY", y);
            isWalking = true;
            _rigidbody.MovePosition(_rigidbody.position + new Vector2(x,y) * Time.deltaTime * Speed);
        }

        _animator.SetBool("IsWalking", isWalking);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
