using System.Collections.Generic;
using UnityEngine;

public class SpawnRdmBlocDePlantation : MonoBehaviour
{
    public List<Transform> RdmPosition;
    public int totalRdmPositions = 3;
    public GameObject prefabBlocDePlantation;
    public ArgentManager argentManager;
    public AffichageEcran affichageEcran;
    public GameObject parlerCollider;

    void Start()
    {
        while(totalRdmPositions != 0)
        {
            int position = Random.Range(0, RdmPosition.Count);
            Transform selectedPosition = RdmPosition[position];

            GameObject blocDePlantation = Instantiate(prefabBlocDePlantation, selectedPosition.position, Quaternion.identity);
            blocDePlantation.GetComponent<BlocDePlantation>().Setup(argentManager, affichageEcran);
            blocDePlantation.transform.GetChild(1).GetComponent<InteractionCollider>().Setup(parlerCollider);

            totalRdmPositions--;
            RdmPosition.Remove(selectedPosition);
        }
    }
}
