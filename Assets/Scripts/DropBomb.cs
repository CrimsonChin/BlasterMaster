using Assets.Scripts;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public int MaxDroppedBombs;
    public int DroppedBombs;
    public GameObject Bomb;
    private Player _player;

    public void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _player.ActiveBombs < _player.MaxBombCount)
        {
            var bombGameObject = Instantiate(Bomb, new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 25f), Quaternion.identity);
            var bomb = bombGameObject.GetComponent<Bomb>();
            bomb.Power = _player.ExplostionPower;
            _player.ActiveBombs++;
        }
    }
}
