using UnityEngine;

public class Piece : MonoBehaviour
{
    public int valeur;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ArgentManager.Instance.GiveMoney(valeur);
            Destroy(gameObject);
        }
    }
}