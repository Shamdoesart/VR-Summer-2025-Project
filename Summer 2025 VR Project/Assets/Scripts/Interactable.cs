using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRInteractor : MonoBehaviour
{
    public Transform InteractSource;
    public float InteractRange = 5f;
    public LayerMask interactionMask;
    public XRController controller;

    void Update()
    {
        if (CheckForTriggerInput())
        {
            Ray ray = new Ray(InteractSource.position, InteractSource.forward);
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
        if (controller != null && controller.inputDevice.IsPressed(InputHelpers.Button.Trigger, out bool isPressed))
        {
            return isPressed;
        }

        return Input.GetKeyDown(KeyCode.E); // fallback
    }
}
