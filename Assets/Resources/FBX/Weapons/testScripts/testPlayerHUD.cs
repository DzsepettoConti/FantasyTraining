using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class testPlayerHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text currentHealthText;
    [SerializeField] private TMP_Text maxHealthText;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }
}
