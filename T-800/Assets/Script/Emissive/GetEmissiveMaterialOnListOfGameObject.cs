using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetEmissiveMaterialOnListOfGameObject : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_ListOfGameObject = new List<GameObject>();

    [SerializeField]
    private Color m_Color = new Color();

    [SerializeField]
    private float m_Intensity = 0;

    private Material m_Mat = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetEmissiveMaterial()
    { 
        if(m_ListOfGameObject.Count != 0)
        {
            for (int i = 0; i < m_ListOfGameObject.Count; i++)
            {
                m_Mat = m_ListOfGameObject[i].GetComponent<Renderer>().materials[1];
                m_Mat.SetVector("_EmissionColor", m_Color * m_Intensity);
            }
        }
    }
}
