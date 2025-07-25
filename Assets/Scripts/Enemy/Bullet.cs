using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private bool hasHit = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        Transform hitTransform = collision.transform;

        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");

            PlayerHealth playerHealth = hitTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Hit Ground - splash");

            hasHit = true;

            // Állítsuk meg a lövedéket
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            // Forgassuk el a childot X irányban 90 fokkal
            Transform child = transform.Find("slimeKopetSplash");
            Vector3 newRotation = child.localEulerAngles;
            newRotation.x = 0;
            newRotation.y = 0;
            newRotation.z = 0;
            child.localEulerAngles = newRotation;

            // Játssza le az animációt
            if (animator != null)
            {
                animator.SetTrigger("splashTrigger");
            }

            // Animáció után törlés
            StartCoroutine(DestroyAfterDelay(5f)); // állítsd az idõt az anim hosszához
        }
        else
        {
            Destroy(gameObject); // minden másra simán törlõdik
        }

    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
