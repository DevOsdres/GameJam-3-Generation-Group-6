using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVillager : MonoBehaviour
{
    public Transform point1; 
    public Transform point2; 
    public float speed = 10.0f;
    public float x, y;
    private Vector3 targetPosition; 
    private Animator anim;
    private bool isShooting = false;
    private Transform playerCamera; 
    private Quaternion initialRotation; 
    private void Start()
    {
          // Inicia moviéndose hacia el punto B
        targetPosition = point2.position;
        anim = GetComponent<Animator>();

        // Obtener la referencia a la cámara del jugador
        playerCamera = Camera.main.transform;

        // Guardar la rotación inicial del aldeano
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isShooting && !anim.GetBool("isDead")) // Solo moverse si no está disparando
        {
            MoveVillager();
        }
    }

    private void MoveVillager()
    {
        // Mueve el aldeano hacia la posición objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el aldeano alcanza la posición objetivo, cambia la dirección
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", y);

            if (targetPosition == point1.position)
            {
                targetPosition = point2.position;
            }
            else
            {
                targetPosition = point1.position;
            }

            // Reproduce la animación de disparo si llega al point2
            if (targetPosition == point1.position)
            {
                PlayShootAnimation();
            }
            else
            {
                RotateVillager();
            }
        }
    }

        private void RotateVillager()
    {
        // Calcula la nueva rotación sumando 180 grados al ángulo actual en el eje Y
        float currentRotationY = transform.eulerAngles.y;
        float newRotationY = currentRotationY + 180f;

        transform.rotation = Quaternion.Euler(0f, newRotationY, 0f);
    }

    private void PlayShootAnimation()
    {
        FaceCamera();

        isShooting = true; // Detiene el movimiento mientras dispara
        anim.SetBool("IsShootingAnim", true); // Activa la animación de disparo
        Debug.Log("Started shooting animation"); // Depuración

        // Después de 2 segundos, continua el ciclo (ajusta el tiempo según la duración de tu animación)
        Invoke("ContinueMovement", 2f);
    }

    private void ContinueMovement()
    {
        isShooting = false;
        anim.SetBool("IsShootingAnim", false); // Desactiva la animación de disparo
        Debug.Log("Stopped shooting animation"); // Depuración
        transform.rotation = initialRotation;

        RotateVillager();
    }
     private void FaceCamera()
    {
        // Calcula la dirección hacia la cámara
        Vector3 directionToCamera = playerCamera.position - transform.position;
        directionToCamera.y = 0; // Mantiene la rotación en el plano horizontal

        // Calcula la rotación deseada
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // Aplica la rotación
        transform.rotation = targetRotation;
    }
}