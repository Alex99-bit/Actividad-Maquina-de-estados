using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class taskControl : Node
{ 
    public taskControl()
    {

    }

    public override NodeState Evaluate()
    {
        int valControl;
        valControl = (int)GetData("controlador");
        valControl++;
        //Debug.Log("cambiando valort de control:   " + valControl);
        if(valControl >= 5)
        {
            valControl = 1;
        }
        parent.parent.SetData("controlador", valControl);

        state = NodeState.RUNNING;
        return state;

    }
}
