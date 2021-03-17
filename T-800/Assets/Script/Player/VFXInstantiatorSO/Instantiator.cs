using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GAME/Particles", fileName = "NewParticles")]
public class Instantiator : ScriptableObject
{
    [SerializeField]
    private GameObject m_Prefab = null;

    public void InstantiatePrefab(MovementInfo p_infos)
    {
        Instantiate(m_Prefab, p_infos.currentPosition + new Vector3(0,-.5f,0), Quaternion.identity);
    }
}
