using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacaMecha : MonoBehaviour
{
    public static VacaMecha instance;
    public CowStates currentCowState;

    [SerializeField]
    float hambre, resitencia, lactancia, estres;

    float timeDriver, timeLapse;

    Animator animator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /* Valores predeterminados */
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

        timeLapse = 3f;

        animator = GetComponent<Animator>();
        SetNewState(CowStates.idle);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hambre: " + hambre + " Resitencia: " + resitencia + " Lactancia: " + lactancia + " Estres: " + estres);

        // hambre, resitencia, lactancia, estres
        // Aqui se asegura de que no sobre pasen los limites
        if (lactancia >= 100)
        {
            lactancia = 100;
        }

        if (hambre >= 100)
        {
            hambre = 100;
        }

        if (resitencia >= 100)
        {
            resitencia = 100;
        }

        if (estres >= 100)
        {
            estres = 100;
        }

        // Y aqui que no sean mas bajos
        if (lactancia <= 0)
        {
            lactancia = 0;
        }

        if (hambre <= 0)
        {
            hambre = 0;
        }

        if (resitencia <= 0)
        {
            resitencia = 0;
        }

        if (estres <= 0)
        {
            estres = 0;
        }

        // Aqui se evalua el estado en caso de que se tenga que ejecutar de forma variable o constante algo dependiendo del estado
        switch (currentCowState)
        {
            case CowStates.pastar:
                if (timeDriver >= timeLapse)
                {
                    timeDriver = 0;
                    hambre += 7;
                    estres -= 0.3f;

                    if (hambre > 77)
                    {
                        lactancia += 3;
                    }
                    else if (hambre > 40)
                    {
                        lactancia++;
                    }
                }
                break;
            case CowStates.ordenia:
                if (timeDriver >= timeLapse)
                {
                    timeDriver = 0;
                    lactancia--;
                    hambre -= 2;
                }
                break;
            case CowStates.jugar:
                if (timeDriver >= timeLapse)
                {
                    timeDriver = 0;
                    estres -= 5;
                    hambre -= 3;
                    resitencia--;

                    if (hambre > 77)
                    {
                        lactancia += 3;
                    }
                    else if (hambre > 40)
                    {
                        lactancia++;
                    }
                }
                break;
            case CowStates.idle:
                if (timeDriver >= timeLapse)
                {
                    timeDriver = 0;
                    hambre -= 3;
                    estres++;

                    if (hambre > 77)
                    {
                        lactancia += 3;
                    }
                    else if (hambre > 40)
                    {
                        lactancia++;
                    }
                }
                break;
            case CowStates.estallar:
                hambre = 0;
                resitencia = 0;
                estres = 0;
                lactancia = 0;
                break;
            case CowStates.escapar:
                if (timeDriver >= timeLapse)
                {
                    timeDriver = 0;
                    estres += 5;
                    hambre -= 2;
                }
                break;
            case CowStates.descanso:
                if (timeDriver >= timeLapse)
                {
                    timeDriver = 0;
                    resitencia += 7;
                    estres--;
                    hambre--;

                    if (hambre > 77)
                    {
                        lactancia += 3;
                    }
                    else if (hambre > 40)
                    {
                        lactancia++;
                    }
                }
                break;
        }

        timeDriver += Time.deltaTime;
    }
    
    // Funcion que se llama en caso especial para cambiar de estado y ejecutar algo una sola vez
    public void SetNewState(CowStates newState)
    {
        timeDriver = 0;

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
