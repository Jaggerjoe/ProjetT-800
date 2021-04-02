using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndCredits : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer m_Credit;
      [SerializeField]
    private GameObject m_ObjCredit;



    void Start() { m_Credit.loopPointReached += CheckOver; }

    void CheckOver(UnityEngine.Video.VideoPlayer p_Credit)
    {
        print("Video Is Over");
        m_ObjCredit.SetActive(false);
    }

}
