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

        interactor.UpdateActiveObject(gameObject);

    }

    // Use this for initialization
    void Start () {
        rb = GetComponentInParent<Rigidbody>();
	}
}
