using System;
using UnityEngine;

namespace Feature.NPC.Scripts
{
    public class NpcSetup : MonoBehaviour
    {
        [SerializeField]
        private NpcModelList _modelList;
        [SerializeField]
        private Renderer _renderer;
        
        
        private void Awake()
        {
            // Set texture to a random one from the model list
            _renderer.material.mainTexture = _modelList.Textures[UnityEngine.Random.Range(0, _modelList.Textures.Count)];
        }
    }
}