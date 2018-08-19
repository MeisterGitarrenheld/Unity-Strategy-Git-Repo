using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractionHandler : MonoBehaviour, Interactable {

    private Rigidbody rb;

    
    public void Activate(UserInteraction interactor)
    {
        rb.useGravity = true;
        rb.AddTorque(new Vector3(
            Random.Range(-40.0f, 40.0f), 
            Random.Range(-40.0f, 40.0f), 
            Random.Range(-40.0f, 40.0f)));
        

    }

    public void setTarget(WalkType newTarget)
    {
        transform.parent.position = newTarget.getTargetPosition() + new Vector3(
            Random.Range(-3.0f, 3.0f),
            Random.Range(-3.0f, 3.0f),
            Random.Range(0.0f, 5.0f));
    }

    // Use this for initialization
    void Start()
    {
        GameMaster.Instance.RegisterInteractable(transform, 0);
        rb = GetComponentInParent<Rigidbody>();
    }
}
