using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;

public class Piece : MonoBehaviour
{
    public int valeur;
    public AudioClip sonRamassage;
    public VisualEffect particleRamassage;

    private float vitesseRotation = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VisualEffect particleEffect = Instantiate(particleRamassage, transform.position, quaternion.identity);

            particleEffect.Play();

            ArgentManager.Instance.GiveMoney(valeur);

            AudioSource.PlayClipAtPoint(sonRamassage, transform.position);

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0f, vitesseRotation * Time.deltaTime, 0f);
    }
}