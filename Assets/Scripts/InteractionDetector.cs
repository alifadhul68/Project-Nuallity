using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();

    // Update is called once per frame
    void Update()
    {
        // Remove destroyed interactables from the list
        _interactablesInRange.RemoveAll(item => item == null);

        if (Input.GetButtonDown("Interact") && _interactablesInRange.Count > 0)
        {
            var closestInteractable = FindClosestInteractable();
            if (closestInteractable != null && closestInteractable.CanInteract())
            {
                closestInteractable.Interact();
                _interactablesInRange.Remove(closestInteractable);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract())
        {
            _interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (_interactablesInRange.Contains(interactable))
        {
            _interactablesInRange.Remove(interactable);
        }
    }

    private IInteractable FindClosestInteractable()
    {
        float closestDistance = float.MaxValue;
        IInteractable closestInteractable = null;

        foreach (var interactable in _interactablesInRange)
        {
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, (interactable as MonoBehaviour).transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
}
