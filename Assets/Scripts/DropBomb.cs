using Assets.Scripts;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public int MaxDroppedBombs;
    public int DroppedBombs;
    public GameObject Bomb;
    private Player _player;

    void Awake()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _player.ActiveBombs < _player.MaxBombCount)
        {
            var bombGameObject = (GameObject)Instantiate(Bomb, new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 25f), Quaternion.identity);
            var bomb = bombGameObject.GetComponent<Bomb>();
            bomb.Power = _player.ExplostionPower;
            _player.ActiveBombs++;
        }
    }
}
