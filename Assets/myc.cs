
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myc : MonoBehaviour
{

    int M = 3;  //misioneros en la orilla inicial
    int C = 3;  //Canibales en la orilla inicial
    bool B = false;  //falso: barco en inicial - true: barco en final
    List<int[]> movimientos = new List<int[]>();

    // Start is called before the first frame update
    void Start()
    {
        
        int max = 0;
        int iteras = 0;
        int[] t;

        while (max < 5000 && ( M > 0 || C > 0) )
        {
            iteras++;
            
            int accion = Random.Range(0, 10);
            switch (accion)
            {
                case 0:
                    if (mover1M())
                    {
                        if (misioneroComido())
                            regresar1M();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);

                        }
                    }
                    break;
                case 1:
                    if (mover1C())
                    {
                        if (misioneroComido())
                            regresar1C();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;
                case 2:
                    if (mover2M())
                    {
                        if (misioneroComido())
                            regresar2M();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;

                case 3:
                    if (mover1M1C())
                    {
                        if (misioneroComido())
                            regresar1M1C();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;

                case 4:
                    if ( mover2C())
                    {
                        if ( misioneroComido())
                        {
                            regresar2C();
                        }

                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }

                    }

                    break;
                    
                case 5:
                    if (regresar1M())
                    {
                        if (misioneroComido())
                            mover1M();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;

                case 6:
                    if (regresar1C())
                    {
                        if (misioneroComido())
                            mover1C();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;

                case 7:
                    if (regresar2M())
                    {
                        if (misioneroComido())
                            mover2M();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;
                case 8:
                    if (regresar2C())
                    {
                        if (misioneroComido())
                            mover2C();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;

                case 9:
                    if (regresar1M1C())
                    {
                        if (misioneroComido())
                            mover1M1C();
                        else
                        {
                            t = new int[] { M, C, System.Convert.ToInt32(B) };
                            movimientos.Add(t);
                        }
                    }
                    break;

            }

            //Debug.Log("iteraNum:   "+ iteras);

        }
        //Debug.Log("Fin de del juego --- M:  " + M + "- C:  " + C);

        t = new int[] { M, C, System.Convert.ToInt32(B) };
        movimientos.Add(t);

        Debug.Log("Secuencia de decisiones aleatorias");
        for (int i = 0; i< movimientos.Count;  i++ )
        {
            Debug.Log(" " + movimientos[i][0] + ", " + movimientos[i][1] + ", " + movimientos[i][2]);
        }
        

        Debug.Log("Se llego a la solucion en:  " + iteras+" iteraciones" );

    }

    // Update is called once per frame
    void Update()
    {

    }

    bool misioneroComido()
    {
        if (C > M || (C - 3) > (M - 3))
            return true;
        return false;
    }

    bool mover1M()
    {

        if (M >= 1 && B == false)
        {
            M--;  //pasar 1 misionero a la orilla final
            B = true; //el barco pasa a la orilla final
            return true;
        }
        return false;
    }

    bool mover1C()
    {

        if (C >= 1 && B == false)
        {
            C--;  //pasar 1 misionero a la orilla final
            B = true; //el barco pasa a la orilla final
            return true;
        }
        return false;
    }

    bool mover2M()
    {

        if (M >= 2 && B == false)
        {
            M -= 2;  //pasar 2 misionero a la orilla final
            B = true; //el barco pasa a la orilla final
            return true;
        }
        return false;
    }
    bool mover2C()
    {

        if (C >= 2 && B == false)
        {
            //Debug.Log("exito regla 3- M:  " + M + "- C:  " + C);
            C -= 2;  //pasar 1 misionero a la orilla final
            B = true; //el barco pasa a la orilla final
            //Debug.Log("exito regla 3- M:  "+ M + "- C:  "+C);
            return true;
        }
        //Debug.Log("exitoNT regla 3- M:  " + M + "- C:  " + C);
        return false;
    }

    bool mover1M1C()
    {

        if (M >= 1 && C >= 1 && B == false)
        {
            M--;
            C--;
            B = true;
            return true;
        }
        return false;
    }

    bool regresar1M()
    {
        if (M <= 2 && B == true)
        {
            M++;    //regresar 1 misionero de la orilla final a la inicial
            B = false;
            return true;
        }
        return false;
    }

    bool regresar1C()
    {
        if (C <= 2 && B == true)
        {
            C++;
            B = false;
            return true;
        }
        return false;
    }

    bool regresar2M()
    {
        if (M <= 1 && B == true)
        {
            M += 2;
            B = false;
            return true;
        }
        return false;
    }

    bool regresar2C()
    {
        if (C <= 1 && B == true)
        {
            C += 2;
            B = false;
            return true;
        }
        return false;
    }

    bool regresar1M1C()
    {
        if (M <= 2 && C <= 2 && B == true)
        {
            M++;
            C++;
            B = false;
            return true;
        }

        return false;
    }
}