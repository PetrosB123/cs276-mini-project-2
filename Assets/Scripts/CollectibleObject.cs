using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public Collectible collectible;

    public void Collect(CollisionHandler collisionHandler)
    {
        collectible.Collect(collisionHandler);
        Destroy(gameObject);
    }
    }
