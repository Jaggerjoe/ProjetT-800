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

    [Header("Throw")]
    // 
    [SerializeField]
    private Vector3 m_ThrowVelocity = new Vector3(0, 0, 5);


    /// Event class which will be displayed in the inspector.
    [System.Serializable]
    public class LocationChanged : UnityEvent<Vector3, Vector3> { }

    // Event for location change. Used to update ballistic trajectory.
    public LocationChanged OnLocationChanged; 

    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update()
    {
       
    
        // Execute logic only on button pressed
        if (m_Controller.GrabAndThrow)
        {

            //else
            //{
            // If no, try to pick item in front of the player
            // Create ray from center of the screen
            Vector3 position = transform.position + transform.forward * 2;
            RaycastHit hit;
            // Shot ray to find object to pick
            if (Physics.Raycast(transform.position, position, out hit, 3))
            {
               
                // Check if object is pickable
                var pickable = hit.transform.GetComponent<PickableItem>();
                // If object has PickableItem class
                if (pickable)
                {
                    // Pick it
                    PickItem(pickable);
                    Debug.Log("pick:" + m_Controller.Aiming);
                }
            }
            //}
        }
        else
        {
            //Check if player picked some item already
            if (m_PickableItem
    )
            {
                // If yes, drop picked item
                DropItem(m_PickableItem
        );
                Debug.Log("drop:" + m_Controller.Aiming);
            }

        }
        // Broadcast location change
        OnLocationChanged?.Invoke(m_Slot.position, m_Slot.rotation * m_ThrowVelocity);
    }



    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item)
    {
        // Assign reference
        m_PickableItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(m_Slot);
        // Reset position and rotation
        item.transform.position = m_Slot.position - new Vector3(0,0.5f,0);
        item.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        // Remove reference
        m_PickableItem
 = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward  , ForceMode.VelocityChange);
        // Add velocity to throw the item
        item.Rb.velocity = m_Slot.rotation * m_ThrowVelocity * 0.91f;
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position + transform.forward * 3;
        Gizmos.DrawLine(transform.position,position);
    }
}

