using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class WolfTree : MonoBehaviour
{
    private BTNode behaviorTreeRoot;
    public WolfState currentState;
    // Otros campos y métodos permanecen igual

    private void Start()
    {
        // Construir el árbol de comportamiento
        behaviorTreeRoot = BuildBehaviorTree();
        currentState = WolfState.idle;
        // Otro código de inicialización
    }

    private void Update()
    {
        // Evaluar el árbol de comportamiento en cada actualización
        currentState = behaviorTreeRoot.Evaluate(this);
        // Otro código de actualización
    }

    private BTNode BuildBehaviorTree()
    {
        var root = new BTSelector();

        var eatSequence = new BTSequence();
        eatSequence.AddNode(new BTAction(EatAction));
        eatSequence.AddNode(new BTAction(IdleAction));

        var chaseSequence = new BTSequence();
        chaseSequence.AddNode(new BTAction(ChaseAction));
        chaseSequence.AddNode(new BTAction(IdleAction));

        root.AddNode(eatSequence);
        root.AddNode(chaseSequence);
        root.AddNode(new BTAction(IdleAction));

        return root;
    }

    // Métodos de acción para el árbol de comportamiento
    private WolfState EatAction(WolfTree wolf)
    {
        // Lógica para comer
        return WolfState.comer;
    }

    private WolfState ChaseAction(WolfTree wolf)
    {
        // Lógica para perseguir
        return WolfState.asechar;
    }

    private WolfState IdleAction(WolfTree wolf)
    {
        // Lógica para estar inactivo
        return WolfState.idle;
    }
}


public abstract class BTNode
{
    public abstract WolfState Evaluate(WolfTree wolf);
}

public class BTSelector : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();

    public override WolfState Evaluate(WolfTree wolf)
    {
        foreach (var node in nodes)
        {
            var newState = node.Evaluate(wolf);
            if (newState != WolfState.idle)
            {
                return newState;
            }
        }

        return WolfState.idle;
    }

    public void AddNode(BTNode node)
    {
        nodes.Add(node);
    }
}

public class BTSequence : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();

    public override WolfState Evaluate(WolfTree wolf)
    {
        foreach (var node in nodes)
        {
            var newState = node.Evaluate(wolf);
            if (newState == WolfState.idle)
            {
                return WolfState.idle;
            }
        }

        return nodes.Count > 0 ? nodes[nodes.Count - 1].Evaluate(wolf) : WolfState.idle;
    }

    public void AddNode(BTNode node)
    {
        nodes.Add(node);
    }
}

public class BTAction : BTNode
{
    private Func<WolfTree, WolfState> action;

    public BTAction(Func<WolfTree, WolfState> action)
    {
        this.action = action;
    }

    public override WolfState Evaluate(WolfTree wolf)
    {
        return action(wolf);
    }
}
