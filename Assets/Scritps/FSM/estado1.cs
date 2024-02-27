using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estado1 : State<Agente>
{
    public static estado1 instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("edo1 ya no nulo");
        }
            
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void Enter(Agente entidad)
    {
        //cosas que hace entidad al entrar al estado
        Debug.Log("Entrando a estado 1");
    }

    public override void Excute(Agente entidad)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            entidad.mEstados.ChangeState( estado2.instance);
        }
        entidad.incremento += Time.deltaTime*15;
        entidad.transform.rotation *= Quaternion.Euler(0.2f, 0f, 0f);
    }


    public override void Exit(Agente entidad)
    {

    }
}
