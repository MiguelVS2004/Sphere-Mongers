using UnityEngine;

public class HoleFallScript : MonoBehaviour
{
    private Collider m_Collider;
    

    void Start()
    {
        m_Collider = GetComponent<Collider>();

    }
    
    private void OnTriggerEnter(Collider other)
    {
        m_Collider.enabled = false;

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    m_Collider.enabled = true;

    //}

}
