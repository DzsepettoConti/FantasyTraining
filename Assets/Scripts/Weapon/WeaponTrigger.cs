using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    public int damage = 1;
    public AudioClip hitSound;
    public GameObject hitEffect;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(damage);

                if (hitEffect != null)
                    Instantiate(hitEffect, other.ClosestPoint(transform.position), Quaternion.identity);

                if (hitSound != null && audioSource != null)
                    audioSource.PlayOneShot(hitSound);
            }
        }
    }
}
