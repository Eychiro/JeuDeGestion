using System.Collections.Generic;
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

        if (!_blocsPlantation[rdmBlocDePlantation].GetComponent<BlocDePlantation>().ButterfliesEvent)
        {
            GameObject bonusButterflies = Instantiate(bonusPrefab, _blocsPlantation[rdmBlocDePlantation].transform.position + Vector3.up * 1.5f, Quaternion.identity);
            bonusButterflies.transform.SetParent(_blocsPlantation[rdmBlocDePlantation].transform);

            _blocsPlantation[rdmBlocDePlantation].GetComponent<BlocDePlantation>().ButterfliesEvent = true;

            Destroy(bonusButterflies, 30f);
        }
    }

    public void SpawnEventMalus()
    {
        VerifierListe();

        int rdmBlocDePlantation = Random.Range(0, _blocsPlantation.Count);

        if (!_blocsPlantation[rdmBlocDePlantation].GetComponent<BlocDePlantation>().ButterfliesEvent)
        {
            GameObject malusButterflies = Instantiate(malusPrefab, _blocsPlantation[rdmBlocDePlantation].transform.position + Vector3.up * 1.5f, Quaternion.identity);
            malusButterflies.transform.SetParent(_blocsPlantation[rdmBlocDePlantation].transform);

            _blocsPlantation[rdmBlocDePlantation].GetComponent<BlocDePlantation>().ButterfliesEvent = true;

            Destroy(malusButterflies, 30f);
        }
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
