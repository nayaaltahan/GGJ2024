using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Feature.NPC.Scripts;
using UnityEngine;

public class NpcTriggerAggroer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPCHitCollider"))
        {
            var stateController = other.gameObject.GetComponentInParent<NpcStateController>();
            if (stateController == null)
                return;

            if (stateController.CurrentState == NpcState.Chase || stateController.CurrentState == NpcState.Attack) 
                return;
            
            stateController.SetState(NpcState.Chase);
            AudioManager.instance.Play3DOneShot("event:/NPC Aggro", other.gameObject.transform.position);
        }
    }
}