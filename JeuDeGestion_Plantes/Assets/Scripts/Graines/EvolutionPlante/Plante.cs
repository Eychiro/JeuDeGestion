using UnityEngine;
using System.Collections;

public class Plante : MonoBehaviour
{
    [Header("Étapes de croissance")]
    public GameObject[] etapesVisuelles;

    public GameObject prefabPiece;
    private AudioSource sonGrowing;
    
    private int etapeActuelle = 0;

    public float tempsTotal;
    public int gainFinal;

    void Start()
    {
        ActualiserVisuel();
        StartCoroutine(Croissance());

        sonGrowing = GetComponent<AudioSource>();
    }

    void SpawnGold()
    {
        int nombreDePieces = Random.Range(1, 4); 
        int valeurParPiece = gainFinal / nombreDePieces;

        int reste = gainFinal % nombreDePieces;

        for (int i = 0; i < nombreDePieces; i++)
        {
            Vector3 positionAlea = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0.5f, Random.Range(-0.2f, 0.2f));
            GameObject piece = Instantiate(prefabPiece, positionAlea, Quaternion.identity);
            
            if (piece.TryGetComponent(out Piece scriptCoin))
            {
                // On donne la valeur de base, si c'est la DERNIÈRE pièce alors on ajoute le reste
                if (i == nombreDePieces - 1)
                {
                    scriptCoin.valeur = valeurParPiece + reste;
                }
                else
                {
                    scriptCoin.valeur = valeurParPiece;
                }
            }

            if (piece.TryGetComponent(out Rigidbody rb))
            {
                Vector3 forceSaut = new Vector3(Random.Range(-2f, 2f), 5f, Random.Range(-2f, 2f));
                rb.AddForce(forceSaut, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
        BlocDePlantation blocDePlantation = GetComponentInParent<BlocDePlantation>();
        blocDePlantation.estOccupe = false;
    }

    IEnumerator Croissance()
    {
        float tempsParEtape = tempsTotal / (etapesVisuelles.Length - 1);

        while (etapeActuelle < etapesVisuelles.Length - 1)
        {
            yield return new WaitForSeconds(tempsParEtape);
            etapeActuelle++;

            sonGrowing.pitch = Random.Range(0.9f, 1.1f);
            sonGrowing.Play();

            ActualiserVisuel();
        }
        
        Debug.Log("La plante est prête !");
        yield return new WaitForSeconds(2f);
        SpawnGold();
    }
    
    void ActualiserVisuel()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (etapesVisuelles == null || etapesVisuelles.Length == 0)
            return;

        for (int i = 0; i < etapesVisuelles.Length; i++)
        {
            if (etapesVisuelles[i] != null)
            {
                etapesVisuelles[i].SetActive(i == etapeActuelle);
            }
        }
    }
}