using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Lootable))]
public class Tile : MonoBehaviour
{
    public bool Destroyable;

    public void AttemptDestroy()
    {
        if (!Destroyable)
            return;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        var boxCollider2D = GetComponent<BoxCollider2D>();
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = false;
        }

        var lootable = GetComponent<Lootable>();
        if (lootable)
        {
            lootable.GenerateLoot();
        }

        Destroyable = false;
    }
}
