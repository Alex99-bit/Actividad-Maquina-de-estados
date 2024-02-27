using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class checkPress1 : Node
{
    
    public checkPress1()
    {

    }

    public override NodeState Evaluate()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("Presionado 111");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }

}
