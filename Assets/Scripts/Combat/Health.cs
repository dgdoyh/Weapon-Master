using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float MaxHp = 100f;
    [SerializeField] public float CurrHp;

    public event Action OnTakeDamage;
    public event Action OnDie;

    private void Awake()
    {
        CurrHp = MaxHp;
    }

    public void GetDamage(int damage)
    {
        CurrHp -= damage;

        if (CurrHp <= 0)
        {
            CurrHp = 0;

            Die();
        }

        OnTakeDamage?.Invoke();

        // test code //
        Debug.Log("-" + damage + " / curr hp: " + CurrHp); 
    }

    public void Heal(int healAmount)
    {
        CurrHp += healAmount;

        if (CurrHp > MaxHp)
        {
            CurrHp = MaxHp;
        }
    }

    public void Die()
    {
        // + Play sfx
        // + Play animation
        // + Turn off controls

        OnDie?.Invoke();

        // Test Code //
        GetComponent<RagdollToggler>().ToggleRagdoll(true);
    }
}
