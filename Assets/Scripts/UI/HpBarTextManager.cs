using UnityEngine;
using UnityEngine.UI;


public class HpBarTextManager : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Health health;


    private void OnEnable()
    {
        health.OnTakeDamage += UpdateText;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= UpdateText;
    }

    public void UpdateText()
    {
        text.text = health.CurrHp + " / " + health.MaxHp;
    }
}
