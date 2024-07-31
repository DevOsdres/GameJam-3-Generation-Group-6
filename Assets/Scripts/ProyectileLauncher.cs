using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileLauncher : MonoBehaviour
{
    public Transform gun;
    public Transform launchPoint;
    public GameObject projectile;
    [SerializeField] private float launchSpeed = 10f;

    [Header("***Trajectory Display***")]
    public LineRenderer lineRenderer;
    public int LinePoints = 200;
    public float timeIntervalinPoints = 0.05f;

    private List<GameObject> activeProjectiles = new List<GameObject>();
     [SerializeField]  private int maxProjectiles = 3;
      [SerializeField] private float projectileLifetime = 4f;

    void Update()
    {
        // Handle trajectory drawing
        if (Input.GetMouseButton(1))
        {
            DrawTrajectory();
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        // Handle projectile launching
        if (Input.GetMouseButtonDown(0) && activeProjectiles.Count < maxProjectiles)
        {
            LaunchProjectile();
        }
        
        // Clean up inactive projectiles
        CleanUpProjectiles();
    }

    void LaunchProjectile()
    {
        var _projectile = Instantiate(projectile, launchPoint.position, gun.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = launchSpeed * launchPoint.up;

        // Add the new projectile to the list
        activeProjectiles.Add(_projectile);

        // Schedule projectile destruction
        Destroy(_projectile, projectileLifetime);
    }

    void CleanUpProjectiles()
    {
        // Remove destroyed projectiles from the list
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
            var x = (startVelocity.x * time ) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time)  + (Physics.gravity.y / 2 * time * time);
            var z = (startVelocity.z * time ) + (Physics.gravity.z / 2 * time * time);

            Vector3 point = new Vector3(x, y, z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}
