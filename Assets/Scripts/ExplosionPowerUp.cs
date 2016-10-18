using UnityEngine;
using System.Collections;

public class ExplosionPowerUp : MonoBehaviour
{
    public int Power;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            var player = collider.gameObject.GetComponent<Player>();
            player.ExplostionPower += Power;

            Destroy(gameObject);
        }
    }
}
