using TMPro;
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

    [HideInInspector] public float _bonusButterflies;
    [HideInInspector] public float _malusButterflies;

    private AudioSource sonPlantation;
    private VisualEffect _vfxPlantation;

    private float timerButterfliesEvent = 0f;
    [HideInInspector] public bool _isBonus = true;

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
                    if (_isBonus)
                    {
                        float bonus = _bonusButterflies;

                        if (bonus < 1.0f) 
                        {
                            scriptPlante.tempsTotal = graineChoisie.CalculerTempsMaturation() * bonus;
                            scriptPlante.gainFinal = graineChoisie.CalculerGainFinMaturation();
                        }
                        else 
                        {
                            scriptPlante.tempsTotal = graineChoisie.CalculerTempsMaturation();
                            scriptPlante.gainFinal = scriptPlante.gainFinal = graineChoisie.CalculerGainFinMaturation() + Mathf.RoundToInt(bonus); 
                        }
                    }
                    else
                    {
                        float malus = _malusButterflies;

                        if (malus < 2.0f) 
                        {
                            scriptPlante.tempsTotal = graineChoisie.CalculerTempsMaturation() * malus;
                            scriptPlante.gainFinal = graineChoisie.CalculerGainFinMaturation();
                        }
                        else 
                        {
                            scriptPlante.tempsTotal = graineChoisie.CalculerTempsMaturation();
                            scriptPlante.gainFinal = scriptPlante.gainFinal = graineChoisie.CalculerGainFinMaturation() - Mathf.RoundToInt(malus); 
                        }
                    }
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