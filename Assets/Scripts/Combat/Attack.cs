using System;
using UnityEngine;


[Serializable]
public class Attack
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public int NextComboIndex { get; private set; } = -1;
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public float ForwardForce { get; set; }
    [field: SerializeField] public float ForceTime { get; set; }
}
