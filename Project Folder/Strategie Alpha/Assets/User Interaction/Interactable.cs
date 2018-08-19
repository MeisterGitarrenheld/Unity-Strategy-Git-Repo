using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable {
    
    void Activate(UserInteraction interactor);
    void setTarget(WalkType newTarget);
    
}
