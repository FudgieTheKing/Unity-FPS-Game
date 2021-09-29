using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class playHP : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI HP;

    private void Update()
    {
        HP.text = "   HP:  " + health.ToString();
    }
    public void damage(float amount)
    {
        
        health -= amount;
        if (health <= 0)
        {
            
        }
    }
}
