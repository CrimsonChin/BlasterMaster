using UnityEngine;
using System.Collections;

public class MoveCharController : MonoBehaviour
{
    public float Speed = 3f;
    private Animator _animator;
    private CharacterController _characterController;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

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
            _characterController.Move(new Vector2(x, y) * Time.deltaTime * Speed);
        }

        _animator.SetBool("Walk", isWalking);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}