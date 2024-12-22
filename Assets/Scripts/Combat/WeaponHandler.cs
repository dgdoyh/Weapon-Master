using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    private void Start()
    {
        weapon.SetActive(false);
    }

    // Attack animation event
    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }

    // Attack animation event
    public void DisableWeapon()
    {
        weapon.SetActive(false);
    }
}
