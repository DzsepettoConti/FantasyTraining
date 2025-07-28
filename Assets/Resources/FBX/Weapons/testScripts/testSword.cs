using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSword : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private testPlayerController playercontroller;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
           Enemy enemy = other.GetComponent<Enemy>();
           enemy.TakeDamage(damage);   
        }
    }
}
