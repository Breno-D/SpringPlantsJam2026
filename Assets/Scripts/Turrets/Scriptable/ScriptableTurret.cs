using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObjects/TurretScriptable", order = 1)]
public class ScriptableTurret : ScriptableObject
{
    public float range;
    public int damage;
    public float timeToShoot;
    public bool isAreaAttack;
    public int turretCost;
}
