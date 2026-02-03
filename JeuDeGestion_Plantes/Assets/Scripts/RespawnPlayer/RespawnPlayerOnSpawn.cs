using UnityEngine;

public class RespawnPlayerOnSpawn : MonoBehaviour
{
    public bool _inPlantation = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_inPlantation)
                other.transform.position = transform.GetChild(0).transform.position;
            else
                other.transform.position = transform.GetChild(1).transform.position;
        }
    }
}
