using System;
using System.Collections;
using System.Collections.Generic;
using Feature.NPC.Scripts;
using Feature.NPC.Scripts.PatrolNodes;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NPCSpawner : MonoBehaviour
{
    public PatrolNodeController PatrolNodeController;
    public NPCSpawningRuleset ruleset;
    public static int NPCCounter = 0;

    private void Awake()
    {
        if (PatrolNodeController == null)
        {
            NPCCounter = 0;
            var node = FindClosestPatrolNode();
            PatrolNodeController = node.GetComponentInParent<PatrolNodeController>();
        }
    }

    private DrawPatrolNodeGizmo FindClosestPatrolNode()
    {
        DrawPatrolNodeGizmo[] gos;
        gos = GameObject.FindObjectsOfType<DrawPatrolNodeGizmo>();
        DrawPatrolNodeGizmo closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (DrawPatrolNodeGizmo go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Start()
    {
        SpawnNPC();
        StartCoroutine(SpawnNpcOnTimer());
    }

    private void SpawnNPC()
    {
        var npcGameObject = Instantiate(ruleset.NPCPrefab, transform.position, Quaternion.identity);
        npcGameObject.GetComponent<NpcStateController>().PatrolNodeController = PatrolNodeController;
        NPCCounter++;
    }
    
    private IEnumerator SpawnNpcOnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(ruleset.spawningInterval - ruleset.randomFactor, ruleset.spawningInterval + ruleset.randomFactor));
            if (NPCCounter < ruleset.maxNPCCount)
            {
                SpawnNPC();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
