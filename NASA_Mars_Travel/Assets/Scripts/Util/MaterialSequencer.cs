using UnityEngine;
using System.Collections;


public class MaterialSequencer : MonoBehaviour
{
    public Material[] m_Materials;
     MeshRenderer m_renderer;
    public float m_MaterialDurationSeconds = 0.25f;
    public bool m_EnableMaterialSequence = true;
    public Material m_CorrectMaterial;

    // Use this for initialization
    void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        
    }
    public void EnableBlinkingTarget()
    {
        m_EnableMaterialSequence = true;
        StartCoroutine(AutoChangeMaterial());
    }
    public void SetCorrectLandingMaterial()
    {
        m_EnableMaterialSequence = false;
        m_renderer.material = m_CorrectMaterial;
    }
    public IEnumerator AutoChangeMaterial()
    {
        int index = 0;
        while (m_EnableMaterialSequence)
        {
            m_renderer.material = m_Materials[index ];
            index = (index + 1) % m_Materials.Length;
            yield return new WaitForSeconds(m_MaterialDurationSeconds);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}