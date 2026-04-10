using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RdmEventBlocPlantation : MonoBehaviour
{
    public float intervalle = 5f;

    [Header("Event Bonus/Malus")]
    public GameObject bonusPrefab;
    public GameObject malusPrefab;

    private List<GameObject> _blocsPlantation;
    private float _timerSpawningEvent = 0f;

    private void VerifierListe()
    {
        if (_blocsPlantation == null || _blocsPlantation.Count == 0)
        {
            _blocsPlantation = new List<GameObject>(SpawnRdmBlocDePlantation.instance.allBlocPlantations);
        }
    }

    public void SpawnEventBonus()
    {
        VerifierListe();

        int rdmBlocDePlantation = Random.Range(0, _blocsPlantation.Count);
        BlocDePlantation bloc = _blocsPlantation[rdmBlocDePlantation].GetComponent<BlocDePlantation>();

        if (!bloc.ButterfliesEvent)
        {
            GameObject bonusButterflies = Instantiate(bonusPrefab, bloc.transform.position + Vector3.up * 1.5f, Quaternion.identity);
            bonusButterflies.transform.SetParent(bloc.transform);

            bloc.ButterfliesEvent = true;
            bloc._isBonus = true;

            bloc._bonusButterflies = RdmBonusEvent(bonusButterflies);

            Destroy(bonusButterflies, 30f);
        }
    }

    public void SpawnEventMalus()
    {
        VerifierListe();

        int rdmBlocDePlantation = Random.Range(0, _blocsPlantation.Count);
        BlocDePlantation bloc = _blocsPlantation[rdmBlocDePlantation].GetComponent<BlocDePlantation>();

        if (!bloc.ButterfliesEvent)
        {
            GameObject malusButterflies = Instantiate(malusPrefab, bloc.transform.position + Vector3.up * 1.5f, Quaternion.identity);
            malusButterflies.transform.SetParent(bloc.transform);

            bloc.ButterfliesEvent = true;
            bloc._isBonus = false;

            bloc._malusButterflies = RdmMalusEvent(malusButterflies);

            Destroy(malusButterflies, 30f);
        }
    }

    public float RdmBonusEvent(GameObject cible)
    {
        float chance = Random.value; 
        float valeurRetournee;
        string texteAffichage;

        TextMeshProUGUI texteUI = cible.GetComponentInChildren<TextMeshProUGUI>();

        if (chance < 0.5f)
        {
            float[] choixTemps = { 0.7f, 0.8f, 0.9f };
            float bonusSelectionne = choixTemps[Random.Range(0, choixTemps.Length)];
            
            int pourcentage = Mathf.RoundToInt((1f - bonusSelectionne) * 100f);
            texteAffichage = "-" + pourcentage + "% de temps de pousse";
            
            valeurRetournee = bonusSelectionne;
        }
        else
        {
            int[] choixArgent = { 10, 12, 15 };
            int bonusArgent = choixArgent[Random.Range(0, choixArgent.Length)];
            
            texteAffichage = "+" + bonusArgent + " pièces d'or";
            
            valeurRetournee = (float)bonusArgent;
        }

        if (texteUI != null)
        {
            texteUI.text = texteAffichage;
        }

        return valeurRetournee;
    }

    public float RdmMalusEvent(GameObject cible)
    {
        float chance = Random.value; 
        float valeurRetournee;
        string texteAffichage;

        TextMeshProUGUI texteUI = cible.GetComponentInChildren<TextMeshProUGUI>();

        if (chance < 0.5f)
        {
            float[] choixTemps = { 1.1f, 1.2f, 1.3f };
            float bonusSelectionne = choixTemps[Random.Range(0, choixTemps.Length)];

            int pourcentage = Mathf.RoundToInt((bonusSelectionne - 1f) * 100f);
            texteAffichage = "+" + pourcentage + "% de temps de pousse";

            valeurRetournee = bonusSelectionne;
        }
        else
        {
            int[] choixArgent = { 10, 12, 15 };
            int bonusArgent = choixArgent[Random.Range(0, choixArgent.Length)];
            
            texteAffichage = "-" + bonusArgent + " pièces d'or";
            
            valeurRetournee = (float)bonusArgent;
        }

        if (texteUI != null)
        {
            texteUI.text = texteAffichage;
        }

        return valeurRetournee;
    }

    void Update()
    {
        _timerSpawningEvent += Time.deltaTime;

        if (_timerSpawningEvent > intervalle)
        {
            _timerSpawningEvent = 0f;

            int rdmEvent = Random.Range(0, 2);

            if (rdmEvent == 1)
                SpawnEventBonus();
            else
                SpawnEventMalus();
        }
    }
}
