using UnityEngine;

[CreateAssetMenu(menuName = "Collectibles/Coin")]
public class CollectibleCoin : Collectible
{
    public override void Collect(CollisionHandler collisionHandler)
    {
        collisionHandler.scoreTracker.IncreaseScore(25);
    }
}
