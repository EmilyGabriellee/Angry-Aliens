using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Atributtes")]
    [SerializeField] float health;

    [Header("Feedback")]
    [SerializeField] GameObject deathEffect;

    private void Start()
    {
        //Para cada inimigo na cena sobe o número de inimigos vivos
        GameManager.EnemiesAlive++;
        //Velocidade máxima de impacto
        health = 4f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Velocidade relativa na colisão entre dois objetos
        if((collision.relativeVelocity.magnitude) > health)
        {
            Die();
        }
    }

    void Die()
    {
        //Feedback visual particle system
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }

        GameManager.EnemiesAlive--;

        Destroy(gameObject);
    }
}
