using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour
{
    public int Speed;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            var player = collider.gameObject.GetComponent<Player>();
            player.Speed += Speed;

            Destroy(gameObject);
        }
    }
}
