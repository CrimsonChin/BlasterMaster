using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public int Speed;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            var player = collider.gameObject.GetComponent<Player>();
            player.Speed += Speed;

            Destroy(gameObject);
        }
    }
}
