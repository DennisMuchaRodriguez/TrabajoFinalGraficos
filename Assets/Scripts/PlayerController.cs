using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;           // Velocidad de movimiento
    public float jumpForce = 5f;      // Fuerza del salto
    public float rotationSpeed = 200f; // Velocidad de rotación
    public float gravity = -9.81f;    // Gravedad personalizada
    public Transform cameraTarget;    // Punto que la cámara seguirá (opcional)

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Comprobar si está en el suelo
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mantener el personaje pegado al suelo
        }

        // Movimiento hacia adelante y atrás
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveZ;

        controller.Move(move * speed * Time.deltaTime);

        // Rotación izquierda/derecha
        float rotate = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotate * rotationSpeed * Time.deltaTime);

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
