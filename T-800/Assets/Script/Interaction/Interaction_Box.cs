using UnityEngine;
using UnityEngine.Events;

public class Interaction_Box : InteractionMother
{
    [System.Serializable]
    private class BoxEvent
    {
        public UnityEvent m_OnMove = new UnityEvent();
    }
    [SerializeField]
    private BoxEvent m_BoxEvent = new BoxEvent();

    private Vector3 InitialPos = Vector3.zero;
    public override void Start()
    {
        base.Start();
        InitialPos = transform.position;
    }

    private void Update()
    {
        Vector3 l_CurrentPos = InitialPos;
        if(l_CurrentPos != transform.position)
        {
            m_BoxEvent.m_OnMove.Invoke();
            InitialPos = transform.position;
        }
    }

    public override void RecuperationAniamtorOnPlayer()
    {
        base.RecuperationAniamtorOnPlayer();
    }

    public override void Use()
    {
        if(m_AnimPlayer == null)
        {
            RecuperationAniamtorOnPlayer();
            transform.parent = GlobalInteractionRef.transform;
            CharacterController.Speed = m_Speed / 2;
            m_AnimPlayer.SetTrigger("StartTakeBox");
        }
        else
        {
            transform.parent = GlobalInteractionRef.transform;
            CharacterController.Speed = m_Speed / 2;
            m_AnimPlayer.SetTrigger("StartTakeBox");
        }
    }

    public override void StopUse()
    {
        base.StopUse();
        if (GlobalInteractionRef.UseObject)
        {
            GlobalInteractionRef.UseObject = false;
            transform.parent = null;
            CharacterController.Speed = m_Speed;
            m_AnimPlayer.SetTrigger("StopTakeBox");
            PlayerControllerSO.InputAsset.FindAction("Player/Jump").Enable();
        }
    }
}
