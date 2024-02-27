using UnityEngine;

public class estado2 : State<Agente>
{
    public static estado2 instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("edo2 ya no nulo");
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
    }

    public override void Excute(Agente entidad)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            entidad.mEstados.ChangeState( rotarZPlus.instance);
        }
        entidad.incremento -= Time.deltaTime * 15;
        entidad.transform.rotation *= Quaternion.Euler(-0.2f, 0f, 0f);
    }

    public override void Exit(Agente entidad)
    {

    }
}