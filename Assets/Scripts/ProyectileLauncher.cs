using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileLauncher : MonoBehaviour
{
    public Transform gun;
    public Transform launchPoint;
    public GameObject projectile;
    [SerializeField] private float launchSpeed = 20f; // Velocidad de lanzamiento aumentada

    [Header("***Trajectory Display***")]
    public LineRenderer lineRenderer;
    public int LinePoints = 300; // Aumentado para mayor precisión
    public float timeIntervalinPoints = 0.04f; // Reducido para más detalle

    private List<GameObject> activeProjectiles = new List<GameObject>();
    [SerializeField] private int maxProjectiles = 3;
    [SerializeField] private float projectileLifetime = 4f;

    void Update()
    {
        // Manejar el dibujo de la trayectoria
        if (Input.GetMouseButton(1))
        {
            DrawTrajectory();
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        // Manejar el lanzamiento de proyectiles
        if (Input.GetMouseButtonDown(0) && activeProjectiles.Count < maxProjectiles)
        {
            LaunchProjectile();
        }

        // Limpiar proyectiles inactivos
        CleanUpProjectiles();
    }

    void LaunchProjectile()
    {
        var _projectile = Instantiate(projectile, launchPoint.position, gun.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = launchSpeed * launchPoint.up;

        // Agregar el nuevo proyectil a la lista
        activeProjectiles.Add(_projectile);

        // Programar la destrucción del proyectil
        Destroy(_projectile, projectileLifetime);
    }

    void CleanUpProjectiles()
    {
        // Eliminar proyectiles destruidos de la lista
        activeProjectiles.RemoveAll(p => p == null);
    }

    void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = LinePoints;
        float time = 0;

        for (int i = 0; i < LinePoints; i++)
        {
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            var z = (startVelocity.z * time) + (Physics.gravity.z / 2 * time * time);

            Vector3 point = new Vector3(x, y, z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}