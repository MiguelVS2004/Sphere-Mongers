using UnityEngine;

public class BallDespawnerScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        GameObject ball = collision.gameObject;
        Destroy(ball);
    }
}
