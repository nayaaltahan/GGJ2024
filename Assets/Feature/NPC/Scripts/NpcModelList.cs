using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GGJ2024", menuName = "ScriptableObjects/NPCModelList", order = 1)]
public class NpcModelList : ScriptableObject
{
    public List<Texture> Textures;
}