//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class InteractionLevier : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject m_Target;

//    [SerializeField]
//    private InputAction m_TestActionMap = new InputAction("TestACtion", InputActionType.Button, "Keyboard/E");

//    private void Start()
//    {
//        m_TestActionMap.Enable();
//    }

//    private void Update()
//    {
//        ActionLevier();
//    }

//    public void ActionLevier()
//    {
//        m_TestActionMap.performed += (ctx) =>
//        {
//            Debug.Log("coucouc");
//            m_Target.transform.rotation = Quaternion.Slerp(m_Target.transform.rotation, Quaternion.Euler(90,0,0), Time.deltaTime);
//        };
//    }
//}
