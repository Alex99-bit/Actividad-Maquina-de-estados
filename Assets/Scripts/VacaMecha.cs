using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacaMecha : MonoBehaviour
{
    public static VacaMecha instance;
    public CowStates currentCowState;

    [SerializeField]
    int hambre,resitencia,lactancia,estres;

    Animator animator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (hambre == 0)
        {
            hambre = 100;
        }

        if (resitencia == 0)
        {
            resitencia = 100;
        }

        if (lactancia == 0)
        {
            lactancia = 30;
        }

        // El estres se va a modificar unicamente directo del inspector

        animator = GetComponent<Animator>();
        SetNewState(CowStates.idle);
    }

    // Update is called once per frame
    void Update()
    {
        // Aqui se evalua el estado en caso de que se tenga que ejecutar de forma variable o constante algo dependiendo del estado
        switch (currentCowState)
        {
            case CowStates.pastar:

                break;
            case CowStates.ordenia:

                break;
            case CowStates.jugar:

                break;
            case CowStates.idle:
                
                break;
            case CowStates.estallar:

                break;
            case CowStates.escapar:

                break;
            case CowStates.descanso:

                break;
        }
    }
    
    // Funcion que se llama en caso especial para cambiar de estado y ejecutar algo una sola vez
    public void SetNewState(CowStates newState)
    {
        switch (newState)
        {
            case CowStates.pastar:

                break;
            case CowStates.ordenia:

                break;
            case CowStates.jugar:

                break;
            case CowStates.idle:

                break;
            case CowStates.estallar:

                break;
            case CowStates.escapar:

                break;
            case CowStates.descanso:

                break;
        }

        currentCowState = newState;
    }
}

public enum CowStates
{
    pastar,
    jugar,
    escapar,
    ordenia,
    estallar,
    descanso,
    idle
}
