using UnityEngine;

public class BallFallScript : MonoBehaviour
{
    private GameObject[] holes;
    private GameObject[] middles;
    private GameObject[] stencils;
    private Vector3[] stencilSizes;
    private Collider ballCollider;
    private Vector3 ballSize;

    void Start()
    {
        // Obtém todos os buracos
        holes = GameObject.FindGameObjectsWithTag("Hole");

        int n = holes.Length;
        middles = new GameObject[n];
        stencils = new GameObject[n];
        stencilSizes = new Vector3[n];

        // Para cada buraco, encontra o Middle e o Stencil (assumindo que são filhos)
        for (int i = 0; i < n; i++)
        {
            middles[i] = holes[i].transform.Find("Middle")?.gameObject;
            stencils[i] = holes[i].transform.Find("Stencil")?.gameObject;

            if (stencils[i] != null)
                stencilSizes[i] = stencils[i].GetComponent<Renderer>().bounds.size;
            else
                Debug.LogWarning($"Stencil não encontrado em {holes[i].name}");
        }

        ballCollider = GetComponent<Collider>();
        ballSize = GetComponent<Renderer>().bounds.size;
    }

    void Update()
    {
        Vector2 ballPos = new Vector2(transform.position.x, transform.position.z);

        for (int i = 0; i < holes.Length; i++)
        {
            if (middles[i] == null || stencils[i] == null)
                continue;

            Vector2 circlePos = new Vector2(middles[i].transform.position.x, middles[i].transform.position.z);
            float holeRadius = Mathf.Max(stencilSizes[i].x, stencilSizes[i].z) / 2f;
            float ballRadius = Mathf.Max(ballSize.x, ballSize.z) / 2f;

            // Verifica se a bola está dentro do buraco
            if (Vector2.Distance(circlePos, ballPos) < holeRadius - ballRadius)
            {
                // Desativa o collider para a bola cair
                ballCollider.enabled = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (middles == null) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < middles.Length; i++)
        {
            if (middles[i] == null || stencils[i] == null) continue;
            Gizmos.DrawWireSphere(middles[i].transform.position, Mathf.Max(stencilSizes[i].x, stencilSizes[i].z) / 2f);
        }
    }

}
