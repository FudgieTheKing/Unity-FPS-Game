using UnityEngine;

public class target : MonoBehaviour
{
    public float health = 50f;

    public void damage( float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            die();
        }
    }
    void die()
    {
        Destroy(gameObject);
    }
}
