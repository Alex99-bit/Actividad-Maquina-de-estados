using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class taskRotarZPlus : Node
{
    Transform agenteTransform;


    public taskRotarZPlus(Transform transform)
    {
        agenteTransform = transform;
    }

    public override NodeState Evaluate()
    {
        agenteTransform.rotation *= Quaternion.Euler(0f, 0f, 0.2f);
        state = NodeState.RUNNING;
        return state;
    }
}
