using UnityEngine;

public class ChangeRespawn : MonoBehaviour
{
    public RespawnPlayerOnSpawn respawnPlayerOnSpawn;
    public bool spawnPlantation;

    void OnTriggerEnter(Collider other)
    {
        respawnPlayerOnSpawn._inPlantation = spawnPlantation;
    }
}
