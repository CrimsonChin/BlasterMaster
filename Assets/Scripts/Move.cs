using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Player _player;

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();

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
            _rigidbody.MovePosition(_rigidbody.position + new Vector2(x, y) * Time.deltaTime * _player.Speed);
        }

        _animator.SetBool("IsWalking", isWalking);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
