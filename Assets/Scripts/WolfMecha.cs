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

    float comida, resitencia, estres;

    NavMeshAgent agent;

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
    }

    // Update is called once per frame
    void Update()
    {
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

            break;
        }

        timeDriver += Time.deltaTime;
    }

    public void SetNewWolfState(WolfState newState)
    {
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

            break;
        }

        currentState = newState;
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
