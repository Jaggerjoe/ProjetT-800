using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimEvent : MonoBehaviour
{

      #region Subclass
    [System.Serializable]
    private class MovementEvents
    {
        public MovementInfosEvent m_OnMove = new MovementInfosEvent();
    }
      [SerializeField]
    private MovementEvents m_MovementEvent = new MovementEvents();
    
    #endregion
   public void OnMovement()
   {
       m_MovementEvent.m_OnMove.Invoke(new MovementInfo{entity = this.gameObject, currentPosition = transform.position, orientation = transform.position});
   }
}
