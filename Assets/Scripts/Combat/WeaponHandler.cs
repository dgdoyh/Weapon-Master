using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private GameObject weapon;

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>().gameObject;

        weapon.SetActive(false);
    }

    public void TurnOnCollider()
    {
        weapon.SetActive(true);
    }

    public void TurnOffCollider()
    {
        weapon.SetActive(false);
    }
}
