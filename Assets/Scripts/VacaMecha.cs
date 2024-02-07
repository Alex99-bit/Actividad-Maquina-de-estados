using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacaMecha : MonoBehaviour
{
    public static VacaMecha instance;
    public CowStates currentCowState;

    Animator animator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SetNewState(CowStates.idle);
    }

    // Update is called once per frame
    void Update()
    {
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
