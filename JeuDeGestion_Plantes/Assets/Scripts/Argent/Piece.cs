using UnityEngine;

public class Piece : MonoBehaviour
{
    public int valeur;

    private float vitesseRotation = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ArgentManager.Instance.GiveMoney(valeur);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0f, vitesseRotation * Time.deltaTime, 0f);
    }
}