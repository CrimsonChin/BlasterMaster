using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Troll : MonoBehaviour
{
    private const string AnimatorMoveX = "MoveX";
    private const string AnimatorMoveY = "MoveY";
    private const string AnimatorIsWalking = "IsWalking";

    private const float Speed = 0.02f;
    private Animator _animator;
    private BoardManager _boardManager;
    private Vector2 _direction;
    private float _idleTime;
    private bool _isDead;
    private bool _isMoving;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _target;

    private void Start()
    {
        _target = transform.position;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _boardManager = FindObjectOfType<BoardManager>();
    }

    private void FixedUpdate()
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
                var paths = GetAdjacentPaths();
                _direction = paths.ElementAt(Random.Range(0, paths.Count));
                _target = position + _direction;

                if (_direction == Vector2.left && !_spriteRenderer.flipX
                    || _direction == Vector2.right && _spriteRenderer.flipX)
                {
                    Flip();
                }

                _animator.SetFloat(AnimatorMoveX, _direction.x);
                _animator.SetFloat(AnimatorMoveY, _direction.y);
                _isMoving = true;
            }
        }

        _animator.SetBool(AnimatorIsWalking, _isMoving);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private List<Vector2> GetAdjacentPaths()
    {
        var adjacentPaths = new List<Vector2>();

        Vector2 currentPosition = transform.position;
        currentPosition.x = Mathf.Round(currentPosition.x);
        currentPosition.y = Mathf.Round(currentPosition.y);

        AddPathIfAvailable(currentPosition, Vector2.up, adjacentPaths);
        AddPathIfAvailable(currentPosition, Vector2.right, adjacentPaths);
        AddPathIfAvailable(currentPosition, Vector2.down, adjacentPaths);
        AddPathIfAvailable(currentPosition, Vector2.left, adjacentPaths);

        return adjacentPaths;
    }

    private void AddPathIfAvailable(Vector2 currentPosition, Vector2 direction, IList<Vector2> adjacentPaths)
    {
        if (IsPath(currentPosition + direction))
            adjacentPaths.Add(direction);
    }

    private bool IsPath(Vector2 location)
    {
        var tile = _boardManager.GetTile(location);
        return tile != null && tile.TileType == TileType.Path;
    }

    public Vector2 GetRoundedPosition()
    {
        return new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    public void Die()
    {
        _isDead = true;

        _animator.SetBool(AnimatorIsWalking, false);
        _animator.SetFloat(AnimatorMoveX, 0);
        _animator.SetFloat(AnimatorMoveY, -1);

        InvokeRepeating("Flash", 0f, 0.1f);
        Destroy(gameObject, 1f);
    }

    public void Flash()
    {
        _spriteRenderer.enabled = !_spriteRenderer.enabled;
    }
}