using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] private int weaponDamage = 10;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private int damage;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }
        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health target)) 
        {
            target.GetDamage(damage);
        }
    }

    public void SetDamage(int attackDamage)
    {
        damage = weaponDamage + attackDamage;
    }
}
