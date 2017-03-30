using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Lootable))]
public class Tile : MonoBehaviour
{
	public bool IsPath;
	public bool Destroyable;
	public GameObject PowerUp;

	public void AttemptDestroy()
	{
		if (!Destroyable)
			return;

		var spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;

		var boxCollider2D = GetComponent<BoxCollider2D>();
		boxCollider2D.enabled = false;

		if (PowerUp != null)
		{
			var loot = Instantiate(PowerUp, transform.position, Quaternion.identity);
			Destroy(loot, 3f);
		}

		Destroyable = false;
	}
}
