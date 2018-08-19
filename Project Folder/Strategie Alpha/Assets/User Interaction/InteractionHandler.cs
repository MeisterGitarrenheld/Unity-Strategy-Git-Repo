using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionHandler : MonoBehaviour {

    public abstract void Activate(UserInteraction interactor);
}
