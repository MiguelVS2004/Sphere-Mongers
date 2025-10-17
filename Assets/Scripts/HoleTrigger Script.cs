using UnityEngine;
using UnityEngine.UI;
using System.Collections; // necessário para usar corrotinas

public class HoleTrigger : MonoBehaviour
{
    public Text Scoreboard;
    private Collider ballCollider;
    public int points;
    [SerializeField] private GameObject vfxPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // Desativa colisões para permitir "queda"
        other.enabled = false;

        float ballSize = other.transform.localScale.x; // assumindo escala uniforme

        points = (ballSize < 1.0f) ? points + 1 : points + 3;

        ballCollider = other.GetComponent<Collider>();
        ballCollider.enabled = false;

        Physics.SyncTransforms();

        Scoreboard.text = points.ToString();

        // Inicia a corrotina para spawnar o VFX com atraso
        StartCoroutine(SpawnVFX());
    }

    private IEnumerator SpawnVFX()
    {
        yield return new WaitForSeconds(0.4f); // espera 0.4 segundos

        GameObject vfx = Instantiate(vfxPrefab, transform.position, transform.rotation);
        Destroy(vfx, 2f); // destrói o VFX após 2 segundos
    }
}
