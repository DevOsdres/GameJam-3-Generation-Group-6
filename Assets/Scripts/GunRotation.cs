using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;  // Velocidad de rotación
    [SerializeField] private float minYAngle = -90f;    // Límite mínimo del ángulo en el eje Y (horizontal)
    [SerializeField] private float maxYAngle = 90f;     // Límite máximo del ángulo en el eje Y (horizontal)
    [SerializeField] private float minXAngle = -45f;    // Límite mínimo del ángulo en el eje X (vertical)
    [SerializeField] private float maxXAngle = 45f;     // Límite máximo del ángulo en el eje X (vertical)
    [SerializeField] private Transform pitchObject; // Objeto que se rotará en el eje X (pitch)

    void Start()
    {
        // Establecer la rotación inicial del arma
        transform.rotation = Quaternion.Euler(-90f, 0f, 180f);
    }

    void Update()
    {
        // Obtener la posición del ratón en la pantalla
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        // Calcular los ángulos de rotación en base a la posición del ratón
        float normalizedMouseX = (mouseX / Screen.width) * 2 - 1;
        float normalizedMouseY = (mouseY / Screen.height) * 2 - 1;

        // Mapear los valores normalizados a los ángulos de rotación correspondientes
        float targetYAngle = Mathf.Lerp(minYAngle, maxYAngle, (normalizedMouseX + 1) / 2);
        float targetXAngle = Mathf.Lerp(minXAngle, maxXAngle, (-normalizedMouseY + 1) / 2);

        // Crear la rotación objetivo
        Quaternion targetRotation = Quaternion.Euler(targetXAngle - 90f, targetYAngle, 180f);

        // Interpolar suavemente hacia la rotación objetivo
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}