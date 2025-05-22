using UnityEngine;

public class CollectibleItem : MonoBehaviour
{

    [Header("Audio Section")]
    [SerializeField] private AudioClip _collectSound;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")) 
        {
            if (_collectSound != null)
                AudioSource.PlayClipAtPoint(_collectSound, transform.position);
            Destroy(gameObject);
        }
       
    }
}
