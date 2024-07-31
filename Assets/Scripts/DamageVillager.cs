using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVillager : MonoBehaviour
{
    public float maxHealth = 5f; // Vida máxima del aldeano
    private float currentHealth; // Vida actual del aldeano
    private Animator anim;
    private void Start()
    {
        // Inicializa la vida actual
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Obtiene el tipo de bala
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
            {
                // Calcula el daño basado en el tipo de bala
                float damage = CalculateDamage(bullet.bulletType);
                ApplyDamage(damage);
            }

            // Destruye la bala al impactar
            Destroy(other.gameObject);
        }
    }

        private float CalculateDamage(BulletType bulletType)
    {
        float damage = 0f;

        // Determina el daño basado en el tipo de bala y tipo de aldeano
        switch (bulletType)
        {
            case BulletType.Plant:
                if (CompareTag("WaterVillager"))
                {
                    damage = 1f; // Daño completo para WaterVillager
                }
                else if (CompareTag("FireVillager"))
                {
                    damage = 0.5f; // Daño medio para FireVillager
                }
                else if (CompareTag("PlantVillager"))
                {
                    damage = 0f; // Daño nulo para PlantVillager
                }
                Debug.Log("Ha recibido " + damage + " de daño. Le restan " + (currentHealth - damage) + " de vida.");
                break;

            case BulletType.Water:
                if (CompareTag("FireVillager"))
                {
                    damage = 1f; // Daño completo para FireVillager
                }
                else if (CompareTag("PlantVillager"))
                {
                    damage = 0.5f; // Daño medio para PlantVillager
                }
                else if (CompareTag("WaterVillager"))
                {
                    damage = 0f; // Daño nulo para WaterVillager
                }
                Debug.Log("Ha recibido " + damage + " de daño. Le restan " + (currentHealth - damage) + " de vida.");
                break;

            case BulletType.Fire:
                if (CompareTag("PlantVillager"))
                {
                    damage = 1f; // Daño completo para PlantVillager
                }
                else if (CompareTag("WaterVillager"))
                {
                    damage = 0.5f; // Daño medio para WaterVillager
                }
                else if (CompareTag("FireVillager"))
                {
                    damage = 0f; // Daño nulo para FireVillager
                }
                Debug.Log("Ha recibido " + damage + " de daño. Le restan " + (currentHealth - damage) + " de vida.");
                break;
        }

        return damage;
    }

  private void ApplyDamage(float damage)
    {
        // Resta el daño de la vida actual
        currentHealth -= damage;

        // Verifica si el aldeano ha muerto
        if (currentHealth <= 0f)
        {
            anim.SetBool("isDead", true);
            StartCoroutine(Die()); // Inicia la corutina para la animación de muerte
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);

        // Destruye el objeto del aldeano
        Destroy(gameObject);
    }
}
