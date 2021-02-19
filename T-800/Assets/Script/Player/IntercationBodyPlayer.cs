using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercationBodyPlayer : MonoBehaviour
{

    EtatDuPlayer m_EtatDeMonRobot = EtatDuPlayer.DeuxBras;

    // Start is called before the first frame update
    void Start()
    {
        m_EtatDeMonRobot = EtatDuPlayer.DeuxBras;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public EtatDuPlayer Etat
    {
        get { return m_EtatDeMonRobot; }
        set { m_EtatDeMonRobot = value; }
    }
}
public enum EtatDuPlayer
{
    DeuxBras,
    UnBras,
    SansBras
}