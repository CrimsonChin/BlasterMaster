using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Player _player;
    private bool _isDead;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();

    }

    public void Update()
    {
        if (_isDead)
        {
            return;
        }

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Explosion"))
        {
            StartCoroutine(Die());
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        _isDead = true;
        GetComponent<Collider2D>().enabled = false;

        InvokeRepeating("Flash", 0f, 0.1f);
        yield return new WaitForSeconds(1);
        CancelInvoke("Flash");
        _spriteRenderer.enabled = false;
        //_rigidbody.IsAwake() = false;

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    private void Flash()
    {
        _spriteRenderer.enabled = !_spriteRenderer.enabled;
    }
}
