using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IInteractable
{
    private Animator animator;

    [SerializeField]
    private bool activated;

    [SerializeField]
    private int HP;

    [SerializeField]
    Transform player;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        activated = true;

        var targetVector = Vector3.Normalize(player.position - this.transform.position);
        var dotProduct = Vector3.Dot(this.transform.right, targetVector);

        if (dotProduct >= 0)
        {
            animator.SetTrigger(name: "flip_forward");
        }
        else
        {
            animator.SetTrigger(name: "flip_backward");
        }
    }

    public bool CanInteract()
    {
        return !activated;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated && other.gameObject.tag == "Projectile") 
        {
            Destroy(other.gameObject);
            HP -= 1;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
