using UnityEngine;

[CreateAssetMenu(menuName = "Collectibles/Shield")]
public class CollectibleShield : Collectible
{
    public override void Collect(CollisionHandler collisionHandler)
    {
        collisionHandler.shield = true;
    }
}
