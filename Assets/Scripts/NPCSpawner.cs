using System.Collections;
using System.Collections.Generic;
using Feature.NPC.Scripts;
using Feature.NPC.Scripts.PatrolNodes;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCSpawner : MonoBehaviour
{
    public PatrolNodeController PatrolNodeController;
    public NPCSpawningRuleset ruleset;
    void Start()
    {
        SpawnNPC();
        StartCoroutine(SpawnNpcOnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNPC()
    {
        var npcGameObject = Instantiate(ruleset.NPCPrefab, transform.position, Quaternion.identity);
        npcGameObject.GetComponent<NpcStateController>().PatrolNodeController = PatrolNodeController;
    }
    
    private IEnumerator SpawnNpcOnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(ruleset.spawningInterval - 5f, ruleset.spawningInterval + 5f));
            SpawnNPC();
        }
    }
}
