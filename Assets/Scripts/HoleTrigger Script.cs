using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HoleTrigger : MonoBehaviour
{
    public Text Scoreboard;
    private Collider ballCollider;
    public int points;

    private void OnTriggerEnter(Collider other)
    {

        // Desativa colisões para permitir "queda"
        other.enabled = false;

        float ballSize = other.transform.localScale.x; // assumindo escala uniforme

        points = (ballSize < 1.0f) ? points + 1 : points + 3;

        ballCollider = other.GetComponent<Collider>();

        ballCollider.enabled = false;

        Physics.SyncTransforms();

        Scoreboard.text = "" + points;

    }
}
