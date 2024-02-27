 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente : MonoBehaviour
{

    public StateMachine<Agente> mEstados;

    public float incremento;
    // Start is called before the first frame update
    void Start()
    {
        mEstados = new StateMachine<Agente>(this);
        incremento = 0;

        mEstados.SetCurrentState(estado1.instance);
    }

    // Update is called once per frame
    void Update()
    {
        mEstados.Updating();
    }
}
