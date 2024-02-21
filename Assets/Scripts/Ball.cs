using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 direction; // Dirección aleatoria para mover la pelota

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Llamamos a la función para mover la pelota inicialmente
        MoverAleatoriamente();
    }

    void FixedUpdate()
    {
        // Verificamos si la pelota sale del área de la pantalla
        if (transform.position.y < -5f)
        {
            // Si sale, invertimos la velocidad en el eje y para hacerla rebotar hacia arriba
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Abs(rb.velocity.y), rb.velocity.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Rebotamos la pelota en la dirección opuesta al vector normal de la colisión
        direction = Vector3.Reflect(direction, collision.contacts[0].normal);
        rb.velocity = direction * rb.velocity.magnitude;

        // Llamamos a la función para mover la pelota en una nueva dirección aleatoria
        MoverAleatoriamente();
    }

    void MoverAleatoriamente()
    {
        // Generamos una dirección aleatoria para mover la pelota
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Aplicamos una fuerza en la dirección aleatoria para mover la pelota
        rb.AddForce(direction * 2000f);
    }
}
