using System;
using TMPro;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{

    [Header("Audio Section")]
    [SerializeField] private AudioClip _collectSound;

    [Header("Text Section")]
    [SerializeField] private TextMeshProUGUI _coinText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (_collectSound != null)
            {
                GameObject audioObj = new GameObject("TempAudio");
                AudioSource source = audioObj.AddComponent<AudioSource>();
                source.clip = _collectSound;
                source.volume = 2.0f;
                source.Play();
                Destroy(audioObj, _collectSound.length);
            }

            _coinText.text = Convert.ToString(Convert.ToUInt16(_coinText.text) + 1);

            Destroy(gameObject);
        }
       
    }
}
