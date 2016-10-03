using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class Troll : MonoBehaviour
{
    private const float Speed = 0.02f;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _target;
    private Vector2 _direction;
    private bool _isMoving = false;
    private float _idleTime = 0.0f;
    private BoardManager _boardManager;

    void Start()
    {
        _target = transform.position;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _boardManager = FindObjectOfType<BoardManager>();
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        if (_isMoving == true)
        {
            if (position == _target)
            {
                _idleTime = Random.Range(1, 100);
                _isMoving = false;
            }
            else
            {
                Vector2 moveToward = Vector2.MoveTowards(position, _target, Speed);
                transform.position = new Vector3(moveToward.x, moveToward.y, transform.position.z);
            }
        }
        else
        {
            if (_idleTime > 0)
            {
                --_idleTime;

                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", -1);
            }
            else
            {
                var paths = GetAdjacentPaths();
                _direction = paths.ElementAt(Random.Range(0, paths.Count()));
                _target = position + _direction;

                if (_direction == Vector2.left && !_spriteRenderer.flipX
                    || _direction == Vector2.right && _spriteRenderer.flipX)
                {
                    Flip();
                }

                _animator.SetFloat("MoveX", _direction.x);
                _animator.SetFloat("MoveY", _direction.y);
                _isMoving = true;
            }
        }

        _animator.SetBool("IsWalking", _isMoving);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private List<Vector2> GetAdjacentPaths()
    {
        List<Vector2> adjacentPaths = new List<Vector2>();

        Vector2 currentPosition = transform.position;
        currentPosition.x = Mathf.Round(currentPosition.x);
        currentPosition.y = Mathf.Round(currentPosition.y);

        AddPathIfAvailable(currentPosition, Vector2.up, adjacentPaths);
        AddPathIfAvailable(currentPosition, Vector2.right, adjacentPaths);
        AddPathIfAvailable(currentPosition, Vector2.down, adjacentPaths);
        AddPathIfAvailable(currentPosition, Vector2.left, adjacentPaths);

        return adjacentPaths;
    }

    private void AddPathIfAvailable(Vector2 currentPosition, Vector2 postentialDestination, IList<Vector2> adjacentPaths)
    {
        if (IsPath(currentPosition + postentialDestination))
            adjacentPaths.Add(postentialDestination);
    }

    private bool IsPath(Vector2 location)
    {
        var tile = _boardManager.GetTile(location.x, location.y);
        return tile.TileType == TileType.Path;
    }
}
