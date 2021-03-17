using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CallAnimEvent : MonoBehaviour
{
  [SerializeField]
  private SO_PlayerController m_PlayerController;

  [SerializeField]
  private CharacterController m_PlayerCharacterController;
    #region Subclass
    [System.Serializable]
    private class MovementEvents
    {
      public MovementInfosEvent m_OnMove = new MovementInfosEvent();
      public UnityEvent m_OnStopMove = new UnityEvent();
    }
    [SerializeField]
    private MovementEvents m_MovementEvent = new MovementEvents();

    #endregion
    public void OnMovement()
    {
      m_MovementEvent.m_OnMove.Invoke(new MovementInfo { entity = this.gameObject, currentPosition = transform.position, orientation = transform.position });
    }

    // public void StopMove()
    // {
    //   Debug.Log("coucou");
    //   m_PlayerController.BindInputMovement(false);
    //   m_PlayerCharacterController.Velocity = 0;
    // }

    // public void OnBeginingMove()
    // {
    //   m_PlayerController.BindInputMovement(true);
    // }
}
