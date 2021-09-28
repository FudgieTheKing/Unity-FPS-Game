using UnityEngine;

public class target : MonoBehaviour
{
    public float health = 50f;
    // Start is called before the first frame update
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
