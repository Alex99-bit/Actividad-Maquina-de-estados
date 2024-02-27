using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbolLobo : MonoBehaviour
{
    // Enum para los estados del comportamiento
    public enum BehaviorState
    {
        Idle,
        Asechar,
        Jugar,
        Comer,
        Descansar,
        Muere
    }

    // Clase base para los nodos del árbol
    public abstract class Node
    {
        public abstract BehaviorState Evaluate();
    }

    // Clase para los nodos de condición
    public class ConditionNode : Node
    {
        private System.Func<bool> condition;

        public ConditionNode(System.Func<bool> condition)
        {
            this.condition = condition;
        }

        public override BehaviorState Evaluate()
        {
            return condition() ? BehaviorState.Idle : BehaviorState.Muere;
        }
    }

    // Clase para los nodos de acción
    public class ActionNode : Node
    {
        private System.Action action;

        public ActionNode(System.Action action)
        {
            this.action = action;
        }

        public override BehaviorState Evaluate()
        {
            action();
            return BehaviorState.Idle;
        }
    }

    // Clase para los nodos de secuencia
    public class SequenceNode : Node
    {
        private Node[] nodes;

        public SequenceNode(params Node[] nodes)
        {
            this.nodes = nodes;
        }

        public override BehaviorState Evaluate()
        {
            foreach (var node in nodes)
            {
                BehaviorState result = node.Evaluate();
                if (result != BehaviorState.Idle)
                {
                    return result;
                }
            }
            return BehaviorState.Idle;
        }
    }

    // Variables para el comportamiento del objeto
    private int comida = 100;
    private int estrés = 50;
    private int resistencia = 50;
    private bool comiendo = false;
    private bool zonaSegura = false;

    // Raíz del árbol de comportamiento
    private Node behaviorTreeRoot;

    void Start()
    {
        // Construye el árbol de comportamiento
        Node idle = new SequenceNode(
            new ConditionNode(() => comida < 30),
            new ActionNode(() => CambiarEstado(BehaviorState.Asechar)),
            new ConditionNode(() => estrés > 60),
            new ActionNode(() => CambiarEstado(BehaviorState.Jugar)),
            new ConditionNode(() => resistencia < 30),
            new ActionNode(() => CambiarEstado(BehaviorState.Descansar)),
            new ActionNode(() => ComidaDisminuye()),
            new ActionNode(() => EstrésAumenta()),
            new ActionNode(() => ResistenciaDisminuye())
        );

        Node asechar = new SequenceNode(
            new ConditionNode(() => comida <= 0 || estrés > 80 || resistencia < 10 || comiendo || zonaSegura),
            new ActionNode(() => CambiarEstado(BehaviorState.Muere)),
            new ActionNode(() => ComidaDisminuye()),
            new ActionNode(() => EstrésAumenta()),
            new ActionNode(() => ResistenciaDisminuye())
        );

        Node jugar = new SequenceNode(
            new ConditionNode(() => comida < 30 || resistencia < 30 || estrés < 20),
            new ActionNode(() => CambiarEstado(BehaviorState.Asechar)),
            new ActionNode(() => ComidaDisminuye()),
            new ActionNode(() => EstrésDisminuye()),
            new ActionNode(() => ResistenciaDisminuye())
        );

        Node comer = new SequenceNode(
            new ConditionNode(() => comida > 70 || estrés < 50),
            new ActionNode(() => CambiarEstado(BehaviorState.Jugar)),
            new ActionNode(() => ComidaAumenta()),
            new ActionNode(() => EstrésDisminuye())
        );

        Node descansar = new SequenceNode(
            new ConditionNode(() => comida < 40 || estrés < 20 || resistencia > 80),
            new ActionNode(() => CambiarEstado(BehaviorState.Asechar)),
            new ActionNode(() => ResistenciaAumenta()),
            new ActionNode(() => EstrésDisminuye()),
            new ActionNode(() => ComidaDisminuye())
        );

        behaviorTreeRoot = idle;
    }

    // Métodos para cambiar el estado del comportamiento
    private void CambiarEstado(BehaviorState newState)
    {
        Debug.Log("Cambiando a estado: " + newState);
    }

    private void ComidaAumenta()
    {
        comida += 3;
    }

    private void ComidaDisminuye()
    {
        comida -= 2;
    }

    private void EstrésAumenta()
    {
        estrés += 4;
    }

    private void EstrésDisminuye()
    {
        estrés -= 3;
    }

    private void ResistenciaAumenta()
    {
        resistencia += 3;
    }

    private void ResistenciaDisminuye()
    {
        resistencia -= 6;
    }

    void Update()
    {
        // Evalúa el árbol de comportamiento en cada actualización
        behaviorTreeRoot.Evaluate();
    }
}
