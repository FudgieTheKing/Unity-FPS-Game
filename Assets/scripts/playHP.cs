using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class playHP : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI HP;



    private void Update()
    {
        HP.text = "   HP:  " + health.ToString();
    }




    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

            damage(1f);
        }
    }
    public void damage(float amount)
    {
        
        health -= amount;
        if (health <= 0)
        {
            Restart();
        }
    }


    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
