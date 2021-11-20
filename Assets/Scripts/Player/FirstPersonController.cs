using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public Camera fpsCam;

    public LayerMask interactable;

    public int detectionRange;

    Vector3 rayOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rayOrigin = fpsCam.ViewportToWorldPoint(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, detectionRange, interactable))
            {
                hit.transform.gameObject.GetComponent<Interactable>().Interact();
            }
        }
    }
}
