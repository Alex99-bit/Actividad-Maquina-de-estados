using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotarZPlus : State<Agente>
{
    public static rotarZPlus instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Debug.Log("edo1 ya no nulo");
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
        Debug.Log("Entrando a rotarZPLus");
    }

    public override void Excute(Agente entidad)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            entidad.mEstados.ChangeState(rotarZMinus.instance);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            entidad.mEstados.ChangeState(estado1.instance);
        }
        entidad.incremento += Time.deltaTime * 15;
        entidad.transform.rotation *= Quaternion.Euler(0f, 0f, 0.2f);
    }


    public override void Exit(Agente entidad)
    {
        Debug.Log("Rotar Z plus, saliendo  ");
    }
}
