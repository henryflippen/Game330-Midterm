using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDetection : MonoBehaviour
{
    public Image InfoPanel;
    public Text InfoText;
    public Text InfoName;

    public Camera fpsCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Shoot();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                InfoName.enabled = true;
                InfoText.enabled = true;
                InfoPanel.enabled = true;
                InfoName.text = target.ObjectName;
                InfoText.text = target.ObjectDescription;
            }
            else
            {
                InfoPanel.enabled = false;
                InfoText.enabled = false;
                InfoName.enabled = false;
            }
        }
    }
}
