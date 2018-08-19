using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractionHandler : InteractionHandler {

    private Rigidbody rb;

    public override void Activate()
    {
        rb.useGravity = true;
        rb.AddTorque(new Vector3(
            Random.Range(-40.0f, 40.0f), 
            Random.Range(-40.0f, 40.0f), 
            Random.Range(-40.0f, 40.0f)));
    }

    // Use this for initialization
    void Start () {
        rb = GetComponentInParent<Rigidbody>();
	}
}
