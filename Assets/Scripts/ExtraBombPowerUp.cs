using UnityEngine;
using System.Collections;

public class ExtraBombPowerUp : MonoBehaviour
{

    public int BombIncrement;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            var player = collider.gameObject.GetComponent<Player>();
            player.MaxBombCount += BombIncrement;

            Destroy(gameObject);
        }
    }
}
