using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image bar;
    [SerializeField] private Health health;

    private void Start()
    {
        #region Event Subscription
        health.OnTakeDamage += UpdateHpBar;
        health.OnDie += TurnOffHpBar;
        #endregion

        UpdateHpBar();
    }

    private void OnDisable()
    {
        #region Event Unsubscription
        health.OnTakeDamage -= UpdateHpBar;
        health.OnDie -= TurnOffHpBar;
        #endregion
    }

    public void UpdateHpBar()
    {
        bar.fillAmount = health.CurrHp / health.MaxHp;

        Debug.Log("bar updated");
    }

    public void TurnOffHpBar()
    {
        if (health.CompareTag("Enemy"))
        {
            background.enabled = false;
            bar.enabled = false;
        }
    }
}
