﻿using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Simple example of Grabbing system.
/// </summary>
public class SimpleGrabSystem : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController Controller;
    // Reference to the character camera.
    [SerializeField]
    private Camera characterCamera;
    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;
    // Reference to the currently held item.
    private PickableItem pickedItem;

    [Header("Throw")]
    // Velocity which which object will be thrown.
    [SerializeField]
    private Vector3 throwVelocity = new Vector3(0, 0, 5);


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
        if (Controller.GrabAndThrow)
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
                    Debug.Log("pick:" + Controller.Aiming);
                }
            }
            //}
        }
        else
        {
            //Check if player picked some item already
            if (pickedItem)
            {
                // If yes, drop picked item
                DropItem(pickedItem);
                Debug.Log("drop:" + Controller.Aiming);
            }

        }
        // Broadcast location change
        OnLocationChanged?.Invoke(slot.position, slot.rotation * throwVelocity);
    }



    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item)
    {
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(slot);
        // Reset position and rotation
        item.transform.position = slot.position;
        item.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
        // Add velocity to throw the item
        item.Rb.velocity = slot.rotation * throwVelocity;
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position + transform.forward * 3;
        Gizmos.DrawLine(transform.position,position);
    }
}
