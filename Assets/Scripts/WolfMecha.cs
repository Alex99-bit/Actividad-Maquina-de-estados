using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class WolfMecha : MonoBehaviour
{
    public WolfState currentState;
    public float timeDriver, timeLapse;

    public float radioBusqueda = 20f;

    float comida, resitencia, estres;

    NavMeshAgent agent;
    bool comiendo;

    // Start is called before the first frame update
    void Start()
    {
        if(timeLapse == 0){
            timeLapse = 3f;
        }

        timeDriver = 0;
        agent = GetComponent<NavMeshAgent>();

        comida = 100;
        estres = 0;
        resitencia = 100;

        comiendo = false;
    }

    // Update is called once per frame
    void Update()
    {

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


        if(comida >= 100){
            comida = 100;
        }else if(comida <= 0){
            comida = 0;
        }

        if(estres >= 100){
            estres = 100;
        }else if(estres <= 0){
            estres = 0;
        }

        if(resitencia >= 100){
            resitencia = 100;
        }else if(resitencia <= 0){
            resitencia = 0;
        }

        switch (currentState)
        {
            case WolfState.idle:
                if(timeDriver >= timeLapse){
                    comida -= 2;
                    estres++;
                    resitencia--;
                }

                if(comida < 30){
                    SetNewWolfState(WolfState.asechar);
                }

                if(estres > 60){
                    SetNewWolfState(WolfState.jugar);
                }

                if(resitencia < 30){
                    SetNewWolfState(WolfState.descanso);
                }
            break;
            case WolfState.asechar:
                if(timeDriver >= timeLapse){
                    comida -= 3;
                    estres += 4;
                    resitencia -= 6;
                }

                if(comida <= 0){
                    SetNewWolfState(WolfState.muere);
                }

                if(estres > 80){
                    SetNewWolfState(WolfState.muere);
                }

                if(resitencia < 10){
                    SetNewWolfState(WolfState.muere);
                }

                if(comiendo){
                    SetNewWolfState(WolfState.comer);
                }
            break;
            case WolfState.comer:
                if(timeDriver >= timeLapse){
                    comida += 3;
                    //comida = 80;
                    resitencia++;
                    estres--;
                }

                if(comida > 70){
                    SetNewWolfState(WolfState.jugar);
                }

                if(estres < 50){
                    SetNewWolfState(WolfState.idle);
                }
            break;
            case WolfState.descanso:
                if(timeDriver >= timeLapse){
                    resitencia += 3;
                    estres--;
                    comida -= 4;
                }

                if(comida < 40){
                    SetNewWolfState(WolfState.asechar);
                }

                if(estres < 20){
                    SetNewWolfState(WolfState.idle);
                }

                if(resitencia > 80){
                    SetNewWolfState(WolfState.descanso);
                }
            break;
            case WolfState.jugar:
                if(timeDriver >= timeLapse){
                    comida -= 4;
                    estres -= 5;
                    resitencia -= 3;
                }

                if(comida < 30){
                    SetNewWolfState(WolfState.asechar);
                }

                if(estres < 20){
                    SetNewWolfState(WolfState.idle);
                }

                if(resitencia < 30){
                    SetNewWolfState(WolfState.descanso);
                }
            break;
        }     
        

        timeDriver += Time.deltaTime;
    }

    public void SetNewWolfState(WolfState newState)
    {
        comiendo = false;

        switch (newState)
        {
            case WolfState.idle:

            break;
            case WolfState.asechar:

            break;
            case WolfState.comer:

            break;
            case WolfState.descanso:

            break;
            case WolfState.jugar:

            break;
            case WolfState.muere:
                Destroy(this.gameObject);
            break;
        }

        currentState = newState;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("cow")){
            comiendo = true;
        }
    }
}

public enum WolfState{
    idle,
    asechar,
    jugar,
    comer,
    descanso,
    muere
}
