using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VacaMecha : MonoBehaviour
{
    public static VacaMecha instance;
    public CowStates currentCowState;
    public Transform zonaSegura, milki, pasto;

    public float radioBusqueda = 20f;

    private NavMeshAgent agent;

    [SerializeField]
    float hambre, resitencia, lactancia, estres;

    public float timeDriver, timeLapse;

    [SerializeField]
    bool asustarse, pastar, ordenia, segura;

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
        asustarse = false;
        segura = false;
        ordenia = false;
        pastar = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        SetNewState(CowStates.idle);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hambre: " + hambre + " Resitencia: " + resitencia + " Lactancia: " + lactancia + " Estres: " + estres);


        // Verifica si hay lobos dentro del radio de búsqueda
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);

        // Itera a través de todos los colliders encontrados
        foreach (Collider collider in colliders)
        {
            // Verifica si el collider pertenece a un objeto con la etiqueta "lobo"
            if (collider.CompareTag("wolf"))
            {
                // Obtiene la transformación del lobo
                Transform transformDelLobo = collider.transform;

                // Calcula la distancia entre este objeto y el lobo
                float distancia = Vector3.Distance(transform.position, transformDelLobo.position);

                // Ahora puedes hacer lo que necesites con la distancia, como imprimirlo en la consola
                Debug.Log("Distancia entre este objeto y el lobo: " + distancia);
            }
        }

        // hambre, resitencia, lactancia, estres
        // Aqui se asegura de que no sobre pasen los limites
        if (lactancia >= 100)
        {
            lactancia = 100;
        }
        else if (lactancia <= 0)
        {
            lactancia = 0;
        }

        if (hambre >= 100)
        {
            hambre = 100;
        }
        else if (hambre <= 0)
        {
            hambre = 0;
        }

        if (resitencia >= 100)
        {
            resitencia = 100;
        }
        else if (resitencia <= 0)
        {
            resitencia = 0;
        }

        if (estres >= 100)
        {
            estres = 100;
        }
        else if (estres <= 0)
        {
            estres = 0;
        }


        // Aqui se evalua el estado en caso de que se tenga que ejecutar de forma variable o constante algo dependiendo del estado
        switch (currentCowState)
        {
            case CowStates.pastar:
                if (timeDriver >= timeLapse && pastar)
                {
                    //agent.isStopped = true;
                    
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

                if (hambre > 95)
                {
                    SetNewState(CowStates.idle);
                }

                if (asustarse)
                {
                    SetNewState(CowStates.escapar);
                }

                if (lactancia > 80)
                {
                    SetNewState(CowStates.ordenia);
                }
                break;
            case CowStates.ordenia:
                if (timeDriver >= timeLapse && ordenia)
                {
                    //agent.isStopped = true;
                    
                    timeDriver = 0;
                    lactancia--;
                    hambre -= 2;
                }

                if (lactancia < 30)
                {
                    SetNewState(CowStates.idle);
                }

                if (asustarse)
                {
                    SetNewState(CowStates.escapar);
                }

                if (hambre < 40)
                {
                    SetNewState(CowStates.pastar);
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

                if (hambre < 40)
                {
                    SetNewState(CowStates.pastar);
                }

                if (estres > 80)
                {
                    SetNewState(CowStates.idle);
                }

                if (resitencia < 30)
                {
                    SetNewState(CowStates.descanso);
                }

                if (asustarse)
                {
                    SetNewState(CowStates.escapar);
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

                if (hambre < 30)
                {
                    SetNewState(CowStates.pastar);
                }

                if (estres > 70)
                {
                    SetNewState(CowStates.jugar);
                }

                if (asustarse)
                {
                    SetNewState(CowStates.escapar);
                }

                if (lactancia > 80)
                {
                    SetNewState(CowStates.ordenia);
                }
                break;
            case CowStates.escapar:
                if (timeDriver >= timeLapse)
                {
                    timeDriver = 0;
                    estres += 5;
                    hambre -= 2;
                    agent.SetDestination(zonaSegura.position);
                }

                if (!asustarse)
                {
                    SetNewState(CowStates.descanso);
                }

                if (estres > 90)
                {
                    SetNewState(CowStates.estallar);
                }

                if (estres > 60 && hambre < 50)
                {
                    SetNewState(CowStates.estallar);
                }
                break;
            case CowStates.descanso:
                
                if (timeDriver >= timeLapse && !asustarse && segura)
                {
                    //agent.isStopped = true;
                    
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

                if (resitencia > 85)
                {
                    SetNewState(CowStates.idle);
                }

                if (hambre < 30)
                {
                    SetNewState(CowStates.pastar);
                }

                if (lactancia > 80)
                {
                    SetNewState(CowStates.ordenia);
                }

                if (asustarse && resitencia > 50)
                {
                    SetNewState(CowStates.escapar);
                }

                if (estres > 60)
                {
                    SetNewState(CowStates.jugar);
                }
                break;
        }

        timeDriver += Time.deltaTime;
    }
    
    // Funcion que se llama en caso especial para cambiar de estado y ejecutar algo una sola vez
    public void SetNewState(CowStates newState)
    {
        timeDriver = 0;
        agent.ResetPath();
        //agent.isStopped = false;

        switch (newState)
        {
            case CowStates.pastar:
                agent.SetDestination(pasto.position);
                break;
            case CowStates.ordenia:
                agent.SetDestination(milki.position);
                break;
            case CowStates.jugar:

                break;
            case CowStates.idle:

                break;
            case CowStates.estallar:
                Destroy(this.gameObject);
                break;
            case CowStates.escapar:

                break;
            case CowStates.descanso:
                agent.SetDestination(zonaSegura.position);
                break;
        }

        currentCowState = newState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wolf"))
        {
            SetNewState(CowStates.estallar);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        agent.ResetPath();
        if (other.gameObject.CompareTag("zona"))
        {
            segura = true;
            asustarse = false;
        }
        else if (other.gameObject.CompareTag("milki"))
        {
            ordenia = true;
        }
        else if (other.gameObject.CompareTag("pasto"))
        {
            pastar = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("zona"))
        {
            segura = false;
        }
        else if (other.gameObject.CompareTag("milki"))
        {
            ordenia = false;
        }
        else if (other.gameObject.CompareTag("pasto"))
        {
            pastar = false;
        }
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
