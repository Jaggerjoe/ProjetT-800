using UnityEngine;
using UnityEngine.Events;

public class SimpleGrabSystem : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController m_Controller;
    // Référence au point sur lequel se rend l'objet 
    [SerializeField]
    private Transform m_Slot;
    // Ref de l'objet
    private PickableItem m_PickableItem;


    private bool m_IsArming = false;
    private bool m_IsThrowing = false;

    [SerializeField]
    private GameObject m_Arm;

    [SerializeField]
    private GameObject m_Automaton;
    [SerializeField]
    private GameObject m_AutomatonArmless;

    [Header("Throw")]
    // 
    [SerializeField]
    private Vector3 m_ThrowVelocity = new Vector3(0, 0, 5);


    /// Event class which will be displayed in the inspector.
    [System.Serializable]
    public class LocationChanged : UnityEvent<Vector3, Vector3> { }

    // Event for location change. Used to update ballistic trajectory.
    public LocationChanged OnLocationChanged;


    private void Start()
    {
        m_Arm.transform.position = m_Slot.position;
    }
    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update()
    {


        //    // Execute logic only on button pressed
        //    if (m_Controller.GrabAndThrow)
        //    {

        //        //else
        //        //{
        //        // If no, try to pick item in front of the player
        //        // Create ray from center of the screen
        //        Vector3 position = transform.position + transform.forward * 2;
        //        RaycastHit hit;
        //        // Shot ray to find object to pick
        //        if (Physics.Raycast(transform.position, position, out hit, 3))
        //        {

        //            // Check if object is pickable
        //            var pickable = hit.transform.GetComponent<PickableItem>();
        //            // If object has PickableItem class
        //            if (pickable)
        //            {
        //                // Pick it
        //                PickItem(pickable);
        //                Debug.Log("pick:" + m_Controller.Aiming);
        //            }
        //        }
        //        //}
        //    }
        //    else
        //    {
        //        //Check if player picked some item already
        //        if (m_PickableItem
        //)
        //        {
        //            // If yes, drop picked item
        //            DropItem(m_PickableItem);
        //            Debug.Log("drop:" + m_Controller.Aiming);
        //        }

        //    }
        // Broadcast location change

        if (m_IsArming && m_IsThrowing)
        {
           
            DropItem(m_Arm.GetComponent<PickableItem>());
            Debug.Log("drop:" + m_Controller.Aiming);
        }

        if (m_IsArming && !m_IsThrowing)
        {
            Debug.Log("It's OK");
            SetArmThrow(/*m_Arm.GetComponent<PickableItem>()*/);


        }
        else
        {
            //if (Vector3.Distance(m_Arm.position, slot.position) <= 0.1f)
            //{
            SetArmPos(/*m_Arm.GetComponent<PickableItem>()*/);
            //}
        }
            if (m_Controller.GrabAndThrow)
        {
            m_IsThrowing = true;
        }
        else
        {
            m_IsThrowing = false;

        }


        OnLocationChanged?.Invoke(m_Slot.position, m_Slot.rotation * m_ThrowVelocity);
    }



    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    //private void PickItem(PickableItem item)
    //{
    //    // Assign reference
    //    m_PickableItem = item;
    //    // Disable rigidbody and reset velocities
    //    item.Rb.isKinematic = true;
    //    item.Rb.velocity = Vector3.zero;
    //    item.Rb.angularVelocity = Vector3.zero;
    //    // Set Slot as a parent
    //    item.transform.SetParent(m_Slot);
    //    // Reset position and rotation
    //    item.transform.position = m_Slot.position - new Vector3(0,0.5f,0);
    //    item.transform.localEulerAngles = Vector3.zero;
    //}
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        // Remove reference
        m_PickableItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward  , ForceMode.VelocityChange);
        // Add velocity to throw the item
        item.Rb.velocity = m_Slot.rotation * m_ThrowVelocity * 0.63f;
    }
    public void SetArmThrow(/*PickableItem item*/)
    {

        m_Automaton.SetActive(false);
        m_AutomatonArmless.SetActive(true);

        //Instantiate(m_Arm,m_Slot);
   
        ////    //// Assign reference
        ////    //pickedItem = item;

        ////    //item.transform.SetParent(slot);
        ////    //item.transform.position = slot.position - new Vector3(0, 0, 0);

        ////    //item.transform.localEulerAngles = Vector3.zero;

        ////    //// Disable rigidbody and reset velocities
        ////    //item.Rb.isKinematic = true;
        ////    //item.Rb.velocity = Vector3.zero;
        ////    //item.Rb.angularVelocity = Vector3.zero;
    }

    public void SetArmPos(/*PickableItem item*/)
    {
        m_AutomatonArmless.SetActive(false);
        m_Automaton.SetActive(true);
        //Destroy(m_Arm, 0.3f);
       
        ////    // Assign reference
        ////    pickedItem = item;

        ////    item.transform.SetParent(transform);
        ////    item.transform.position = m_ArmPos.position;

        ////    // Disable rigidbody and reset velocities
        ////    item.Rb.isKinematic = true;
        ////    item.Rb.velocity = Vector3.zero;
        ////    item.Rb.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position + transform.forward * 3;
        Gizmos.DrawLine(transform.position,position);
    }

    public bool IsArming { get { return m_IsArming; } set { m_IsArming = value; } }
}

