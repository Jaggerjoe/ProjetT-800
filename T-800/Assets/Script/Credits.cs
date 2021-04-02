using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using DG.Tweening;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer m_Credit;
    
    [SerializeField]
    private GameObject m_ObjCredit;

    [SerializeField]
    private Image m_End;
   
    [SerializeField]
    private Image m_BlackScreen;


    [SerializeField]
    RawImage m_Raw;



    void Start() 
    {
        m_ObjCredit.transform.DOScale(0.75f, 0);
        m_BlackScreen.transform.DOScale(0.75f, 0);
        m_Credit.loopPointReached += CheckOver; 
    }

    void CheckOver(VideoPlayer p_Credit)
    {
        Sequence l_MySequence = DOTween.Sequence();


        l_MySequence.Insert(0f, m_End.DOFade(0, 1.5f));
        l_MySequence.Insert(0f, m_Raw.DOFade(0, 1.5f));
        l_MySequence.Insert(1.5f, m_End.DOFade(0, 0.5f));

        l_MySequence.Insert(2f, m_ObjCredit.transform.DOScale(0.75f, 0));
        l_MySequence.Insert(2f, m_BlackScreen.transform.DOScale(0.75f, 0));
        m_Credit.gameObject.SetActive(false);


    }

    public void LoadCredit()
    {
        Sequence l_MySequence = DOTween.Sequence();
       
        l_MySequence.Insert(0, m_End.DOFade(1, 0.4f));
        l_MySequence.Insert(0, m_ObjCredit.transform.DOScale(1, 0));
        l_MySequence.Insert(0, m_BlackScreen.transform.DOScale(1, 0));

        l_MySequence.Insert(0.7f, m_End.DOFade(1, 1.5f));
        l_MySequence.Insert(0.7f, m_Raw.DOFade(1, 1.5f));
        m_Credit.gameObject.SetActive(true);

    }

    public void EndCreditClick()
    {
        Sequence l_MySequence = DOTween.Sequence();

        l_MySequence.Insert(0, m_End.DOFade(0, 2));
        l_MySequence.Insert(0, m_Raw.DOFade(0, 2));
        l_MySequence.Insert(2f, m_ObjCredit.transform.DOScale(0.75f, 0));
        l_MySequence.Insert(2f, m_BlackScreen.transform.DOScale(0.75f, 0));
        m_Credit.gameObject.SetActive(false);
    }

}
