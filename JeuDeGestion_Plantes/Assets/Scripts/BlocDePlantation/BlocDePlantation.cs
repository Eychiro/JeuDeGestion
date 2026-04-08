using UnityEngine;
using UnityEngine.VFX;

public class BlocDePlantation : MonoBehaviour, IInteractible
{
    public ArgentManager argentManager;
    public AffichageEcran affichageEcran;
    public InteractionCollider interactionCollider;

    [Header("Paramètres de Plantation")]
    public Transform pointApparition;

    [HideInInspector] public bool estOccupe = false;
    [HideInInspector] public bool ButterfliesEvent = false;

    private float _bonusButterflies = 0.8f;
    private int _malusButterflies = 10;

    private AudioSource sonPlantation;
    private VisualEffect _vfxPlantation;

    private float timerButterfliesEvent = 0f;

    void Start()
    {
        sonPlantation = GetComponent<AudioSource>();
        _vfxPlantation = GetComponent<VisualEffect>();
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

            _vfxPlantation.enabled = true;
            _vfxPlantation.Play();

            float angleY = Random.Range(0f, 360f);
            Quaternion randomRotationY = Quaternion.Euler(0f, angleY, 0f);

            GameObject nouvellePlante = Instantiate(graineChoisie.prefabPlante, pointApparition.position, randomRotationY, transform);
            Plante scriptPlante = nouvellePlante.GetComponent<Plante>();
            
            if (scriptPlante != null)
            {
                if (!ButterfliesEvent)
                {
                    scriptPlante.tempsTotal = graineChoisie.CalculerTempsMaturation();
                    scriptPlante.gainFinal = graineChoisie.CalculerGainFinMaturation();
                }
                else
                {
                    scriptPlante.tempsTotal = graineChoisie.CalculerTempsMaturation() * _bonusButterflies;
                    scriptPlante.gainFinal = graineChoisie.CalculerGainFinMaturation() - _malusButterflies;
                }
            }
            estOccupe = true;
        }
    }

    public void Setup(ArgentManager argentManagerRef, AffichageEcran affichageEcranRef)
    {
        argentManager = argentManagerRef;
        affichageEcran = affichageEcranRef;
    }

    public void Interact() 
    {
        if (interactionCollider.joueurProche)
            PlanterGraine();
    }

    void Update()
    {
        if (ButterfliesEvent)
        {
            timerButterfliesEvent += Time.deltaTime;

            if (timerButterfliesEvent > 30f)
            {
                ButterfliesEvent = false;
                timerButterfliesEvent = 0f;
            }
        }
    }
}