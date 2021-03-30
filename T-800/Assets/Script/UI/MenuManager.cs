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
    private Image m_FondNoir = null;

    [SerializeField]
    private Image[] m_PauseImageArray;
    [SerializeField]
    private Image[] m_MainMenuImageArray;

    private bool m_IsOnMenu = true;

   


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        m_PauseImageArray = m_PauseMenu.GetComponentsInChildren<Image>();
     

        m_MainMenuImageArray = m_MainMenu.GetComponentsInChildren<Image>();

        Sequence l_MySequence = DOTween.Sequence();

        l_MySequence.Insert(0, m_FondNoir.DOFade(0, 6));
        foreach (Image image in m_MainMenuImageArray)
        {
            l_MySequence.Insert(0.75f, image.DOFade(1, 3f));
        }
        m_controller.InputAsset.FindAction("Player/Movements").Disable();
        m_controller.InputAsset.FindAction("Player/Look").Disable();

    }

    // Update is called once per frame
    void Update()
    {
        if(m_controller.OnPause && !m_IsOnMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            m_controller.InputAsset.FindAction("Player/Movements").Disable();
            m_controller.InputAsset.FindAction("Player/Look").Disable();
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Sequence l_MySequence = DOTween.Sequence();

        foreach (Image image in m_MainMenuImageArray)
        {
          l_MySequence.Insert( 0, image.DOFade(0, 1.5f));
          l_MySequence.Insert(1.5f, m_MainMenu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 1167, 0), 1f));
        }

        m_controller.InputAsset.FindAction("Player/Movements").Enable();
        m_controller.InputAsset.FindAction("Player/Look").Enable();

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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_controller.InputAsset.FindAction("Player/Movements").Enable();
        m_controller.InputAsset.FindAction("Player/Look").Enable();
        foreach (Image image in m_PauseImageArray)
        {
            image.DOFade(0, 1.5f);

         
        }
        
      
      
    }

    public void Credits()
    {
        
        

    }

}
