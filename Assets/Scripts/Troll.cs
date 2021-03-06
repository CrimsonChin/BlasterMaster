﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Troll : MonoBehaviour
{
    private const string AnimatorMoveX = "MoveX";
    private const string AnimatorMoveY = "MoveY";
    private const string AnimatorIsWalking = "IsWalking";

    private List<Vector2> _paths;
    private Animator _animator;
    private float _idleTime;
    private bool _isDead;
    private bool _isMoving;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _target;

    public float Speed = 0.02f;

    public LayerMask ObstacleLayer;


    public void Start()
    {
        _paths = new List<Vector2> { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        _target = transform.position;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (_isDead)
        {
            return;
        }

        Vector2 position = transform.position;
        if (_isMoving)
        {
            if (position == _target)
            {
                _idleTime = Random.Range(1, 100);
                _isMoving = false;
            }
            else
            {
                var moveToward = Vector2.MoveTowards(position, _target, Speed);
                transform.position = new Vector3(moveToward.x, moveToward.y, transform.position.z);
            }
        }
        else
        {
            if (_idleTime > 0)
            {
                --_idleTime;

                _animator.SetFloat(AnimatorMoveX, 0);
                _animator.SetFloat(AnimatorMoveY, -1);
            }
            else
            {
                var direction = _paths.ElementAt(Random.Range(0, 4));
                if (!CanWalkInDirection(direction))
                {
                    return;
                }
                _target = position + direction;


                if (direction == Vector2.left && !_spriteRenderer.flipX
                    || direction == Vector2.right && _spriteRenderer.flipX)
                {
                    Flip();
                }

                _animator.SetFloat(AnimatorMoveX, direction.x);
                _animator.SetFloat(AnimatorMoveY, direction.y);
                _isMoving = true;
            }
        }

        _animator.SetBool(AnimatorIsWalking, _isMoving);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    // TODO could walk straight into a bomb... do we want to avoid that?
    public bool CanWalkInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, ObstacleLayer);
        return !hit.collider;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Explosion"))
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        _isDead = true;

        _animator.SetBool(AnimatorIsWalking, false);
        _animator.SetFloat(AnimatorMoveX, 0);
        _animator.SetFloat(AnimatorMoveY, -1);

        StartCoroutine(Flash2(0.25f, 0.2f)); // this is rubbish

        yield return new WaitForSeconds(2);

        var enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.HandleDeath();
        }

        Destroy(gameObject);
    }
    IEnumerator Flash2(float time, float intervalTime)
    {
        var elapsedTime = 0f;
        
        while (elapsedTime < time)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(intervalTime);
        }

        _spriteRenderer.enabled = false;
    }

    public void Flash()
    {
        _spriteRenderer.enabled = !_spriteRenderer.enabled;
    }
}