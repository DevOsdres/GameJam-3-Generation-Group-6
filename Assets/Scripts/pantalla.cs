using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pantalla : MonoBehaviour
{
    public LevelTimer levelTimer; // Referencia al script del timer
    public float timeReduction = 2.5f; // Tiempo a reducir en segundos por cada colisión

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo colisionó con el detector.");
        if (other.CompareTag("EnemyBullet"))
        {
            //Debug.Log("Colisionó una bala.");
            levelTimer.ReduceTime(timeReduction);
            Destroy(other.gameObject);
        }
    }
}
