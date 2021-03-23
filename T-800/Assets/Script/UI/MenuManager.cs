using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController m_controller;
    [SerializeField]
    private GameObject m_PauseMenu = null;
    [SerializeField]
    private GameObject m_MainMenu = null;

    [SerializeField]
    private Image[] m_PauseImageArray;
    [SerializeField]
    private Image[] m_MainMenuImageArray;

    private bool m_IsOnMenu = true;

   


    // Start is called before the first frame update
    void Start()
    {
        m_PauseImageArray = m_PauseMenu.GetComponentsInChildren<Image>();
      

       m_MainMenuImageArray = m_MainMenu.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_controller.OnPause && !m_IsOnMenu)
        {
            foreach (Image image in m_PauseImageArray)
            {
                image.DOFade(1, 1f);

            }
            //Time.timeScale = 0f;
         

        }
        
    }

    public void PlayGame()
    {
        m_IsOnMenu = false;
        Sequence l_MySequence = DOTween.Sequence();

        foreach (Image image in m_MainMenuImageArray)
        {
          l_MySequence.Insert( 0, image.DOFade(0, 1.5f));
        }

        l_MySequence.Insert(1.5f, m_MainMenu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 1167, 0), 1f));
       
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void QuitPause()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Continue()
    {
        m_controller.OnPause = false;
        Time.timeScale = 1f;
       
        foreach(Image image in m_PauseImageArray)
        {
            image.DOFade(0, 1.5f);

         
        }
        
      
      
    }

    public void Credits()
    {
        
        

    }

}
