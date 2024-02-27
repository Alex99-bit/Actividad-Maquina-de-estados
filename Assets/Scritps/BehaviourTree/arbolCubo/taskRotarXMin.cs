using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class taskRotaXMin : Node
{
    Transform agenteTransform;


    public taskRotaXMin(Transform transform)
    {
        agenteTransform = transform;
    }

    public override NodeState Evaluate()
    {
        agenteTransform.rotation *= Quaternion.Euler(-0.2f, 0f, 0f);
        state = NodeState.RUNNING;
        return state;
    }
}
