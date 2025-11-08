using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 7.0f;
    void Start()
    {
        spinSpeed += Random.Range(-0.5f, 0.5f);
    }
    void Update()
    {
        if (!CollisionHandler.paused)
        {
            transform.Rotate(0.0f, 0.0f, spinSpeed);
        }
    }
}
