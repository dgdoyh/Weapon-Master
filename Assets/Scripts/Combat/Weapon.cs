using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int weaponDamage = 10;

    private int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Health>(out Health target)) { return; }

        target.GetDamage(damage);
    }

    public void SetDamage(int attackDamage)
    {
        damage = weaponDamage + attackDamage;
    }
}
