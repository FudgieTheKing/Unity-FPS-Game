using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gunSwitch : MonoBehaviour
{
    public TextMeshProUGUI ammoCount;
    public int selectedWeap = 0;
    // Start is called before the first frame update
    void Start()
    {
        select();
    }



    // Update is called once per frame
    void Update()
    {

        int previousweap = selectedWeap;

        if (Input.GetAxis("Mouse ScrollWheel") >0f)
        {
            if (selectedWeap >= transform.childCount - 1)
                selectedWeap = 0;
            else
                selectedWeap++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeap <= 0)
                selectedWeap = transform.childCount-1;
            else
                selectedWeap--;
        }

        if(selectedWeap == 0)
        {
            ammoCount.text = "";
        }

        if (Input.GetKey(KeyCode.E))
        {
            selectedWeap = 0;
        }

        if (previousweap != selectedWeap)
        {
            select();
        }
    }

    void select()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeap)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
