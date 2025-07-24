using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRInteractor : MonoBehaviour
{
    public Transform InteractSource;     // Assign to your VR hand/controller transform
    public float InteractRange = 5f;
    public LayerMask interactionMask;    // Set to "Default" or your interactables layer

    void Update()
    {
        if (CheckForTriggerInput())
        {
            Ray ray = new Ray(InteractSource.position, InteractSource.forward);
            Debug.DrawRay(ray.origin, ray.direction * InteractRange, Color.red, 0.5f);

            if (Physics.Raycast(ray, out RaycastHit hit, InteractRange, interactionMask))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }

    bool CheckForTriggerInput()
    {
        // ✅ Try common VR trigger input using InputDevices
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            return true;
        }

        // ✅ Fallback for testing in editor
        return Input.GetKeyDown(KeyCode.E);
    }
}