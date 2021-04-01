﻿using System.Collections;
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

    public void InstantiatePrefab(Transform p_Trsf)
    {
        Instantiate(m_Prefab, p_Trsf.position, Quaternion.identity);
    }
    
    public void InstantiatePrefab(JumpInfo p_infos)
    {
        Instantiate(m_Prefab, p_infos.JumpOrigin + new Vector3(0, -.3f, 0), Quaternion.identity);
    }

    public void InstantiatePrefab(LeverInfos p_infos)
    {
        Instantiate(m_Prefab, p_infos.Origin, p_infos.OriginRotation);
    }

    public void InstantiatePrefab(DoorInfos p_infos)
    {
        Instantiate(m_Prefab, p_infos.Origin + new Vector3(-.4f,0,0), p_infos.Rotation);
    }

    public void InstantiatePrefab(ArmSnatchInfos p_infos)
    {
        Instantiate(m_Prefab, p_infos.Position , p_infos.Rotation);
    }
}