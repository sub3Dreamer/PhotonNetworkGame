using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerDataScriptableObject : ScriptableObject
{
    public string ModelName;
    public int Hp;
    public int AttackDamage;
    public string Desc;
}
