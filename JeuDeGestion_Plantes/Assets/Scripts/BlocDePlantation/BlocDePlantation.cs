using UnityEngine;

public class BlocDePlantation : MonoBehaviour, IInteractible
{
    public ArgentManager argentManager;
    public AffichageEcran affichageEcran;

    [Header("Paramètres de Plantation")]
    public Transform pointApparition;

    [HideInInspector] public bool estOccupe = false;

    private AudioSource sonPlantation;

    void Start()
    {
        sonPlantation = GetComponent<AudioSource>();
    }

    public void PlanterGraine()
    {
        if (estOccupe) 
        {
            Debug.Log("Il y a déjà quelque chose ici !");
            return;
        }

        Graine graineChoisie = GraineManager.Instance.hotbar.GetSelectedSlot();

        if (GraineManager.Instance.RetirerGraineSelectionnee(1))
        {
            sonPlantation.pitch = Random.Range(0.9f, 1.1f);
            sonPlantation.Play();

            float angleY = Random.Range(0f, 360f);
            Quaternion randomRotationY = Quaternion.Euler(0f, angleY, 0f);

            GameObject nouvellePlante = Instantiate(graineChoisie.prefabPlante, pointApparition.position, randomRotationY, transform);
            Plante scriptPlante = nouvellePlante.GetComponent<Plante>();
            
            if (scriptPlante != null)
            {            
                scriptPlante.tempsTotal = graineChoisie.CalculerTempsMaturation();
                scriptPlante.gainFinal = graineChoisie.CalculerGainFinMaturation();
            }
            estOccupe = true;
        }
    }

    public void Interact() 
    { 
        PlanterGraine();
    }
}