using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : ScriptableObject
{
    [SerializeField]
    private GameObject m_Prefab = null;

    public void InstantiatePrefab()
    {
        Instantiate(m_Prefab);
    }
}
