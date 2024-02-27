using System.Collections.Generic;
using BehaviorTree;

public class agenteCubo : Tree
{
    int controlVal = 1;
    protected override Node SetupTree()
    {
        
        Node root= new Selector(new List<Node> { 
            new Sequence(new List<Node>{new checkPress1(), new taskControl() }),
            new Sequence(new List<Node>{new checkControl1(), new taskRotaXPlus(transform) }),
            new Sequence(new List<Node>{new checkControl2(), new taskRotaXMin(transform) }),
            new Sequence(new List<Node>{new checkControl3(),
                new Selector(new List<Node>{ new Sequence(new List<Node>{new checkPress2(), new taskControl2Dos()} ),
                    new taskRotarZPlus(transform)})
                }),
            new Sequence(new List<Node>{new checkControl4(), new taskRotaZMin(transform) }),
        });
        root.SetData("controlador", controlVal);


        return root;
    }
}

