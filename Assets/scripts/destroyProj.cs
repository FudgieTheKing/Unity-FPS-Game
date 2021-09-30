using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProj : MonoBehaviour
{
    public float desTime = 5f;
    public GameObject me;
    playHP playerHP;
    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "big_robo" && collision.gameObject.name != "player")
        {
            Destroy(me);
        }

       
            
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(DestroyGameObject), desTime);
    }

    void DestroyGameObject()
    {
        Destroy(me);
    }
}
