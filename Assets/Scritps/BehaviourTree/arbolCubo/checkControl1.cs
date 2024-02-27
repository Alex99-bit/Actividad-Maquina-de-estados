using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class checkControl1 : Node
{
    int valControl;
    
    public checkControl1()
    {
        //valControl = (int)GetData("controlador");
    }

    public override NodeState Evaluate()
    {
        valControl = (int)GetData("controlador");
        if (valControl == 1)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }

}
