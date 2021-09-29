using UnityEngine;
using System.Collections;
using TMPro;

public class gun : MonoBehaviour
{
    public float damage =  10f;
    public float range = 100f;
    public Camera FPScam;
    public ParticleSystem muzzleflash;
    public GameObject impact;
    public float firerate = 40f;
    public int totAmmo = 30;
    public int totAmmoCount;
    public int clip = 10;
    public int currentAmmo;
    public float reloadTime = 1.7f;
    public Animator anima;
    public float force = 10f;
    public float nextFire = 0f;
    private bool reloading = false;
    public TextMeshProUGUI ammoCount;

    // Update is called once per frame

    void Start()
    {
        currentAmmo = clip;
        
    }

    void OnEnable(){
        reloading = false;
        anima.SetBool("reloading", false);
    }

    void Update()
    {



        if(totAmmo > clip)
        {
            ammoCount.text = "   Ammo: " + currentAmmo.ToString() + "/" + (totAmmo - clip).ToString();
        }
        else
        {
            ammoCount.text = "   Ammo: " + currentAmmo.ToString() + "/0";
        }


        if (totAmmo!=0)
        { 
            if (reloading)
            {
                return;
            }
            if (currentAmmo <= 0)
            {
                StartCoroutine(reload());
                return;
            }
            if (Input.GetButton("Fire1") && Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / firerate;
                shoot();
            }
        }


    }



    IEnumerator reload()
    {
        totAmmo -= clip;
        reloading = true;
        if (totAmmo >= clip)
        {
            currentAmmo = clip;
        }
        else
        {
            currentAmmo = totAmmo;
        }
        anima.SetBool("reloading", true);
        yield return new WaitForSeconds(reloadTime);
        anima.SetBool("reloading", false);


        reloading = false;


    }

    void shoot()
    {
        currentAmmo--;
        muzzleflash.Play();
        RaycastHit hit;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            target target = hit.transform.GetComponent<target>();
            if (target != null)
            {
                target.damage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(FPScam.transform.forward * force);
            }

            GameObject  imapctgo = Instantiate(impact, hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(imapctgo, 2f);
        }
    }
}
