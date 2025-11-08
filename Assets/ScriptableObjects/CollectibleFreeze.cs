using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectibles/Freeze")]
public class CollectibleFreeze : Collectible
{
    public override void Collect(CollisionHandler collisionHandler)
    {
        CollisionHandler.paused = true;
    }
}
