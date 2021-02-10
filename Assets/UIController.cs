using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    private GameObject selectedTank = null;
    private int tankLayer = 1 << 8;
    [SerializeField] private Material tankTextureBasic;
    [SerializeField] private Material tankTextureSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayer))
            {
                ToggleSelectTank(hit.transform.gameObject);
            }
            else
            {
                selectedTank.GetComponent<MeshRenderer>().material = tankTextureBasic;
                selectedTank = null;
            }
        }        
    }

    private void ToggleSelectTank(GameObject o)
    {
        if(selectedTank != null)
            selectedTank.GetComponent<MeshRenderer>().material = tankTextureBasic;
        selectedTank = o;
        selectedTank.GetComponent<MeshRenderer>().material = tankTextureSelected;
    }

    public void ActivateUI(GameObject o)
    {
        o.SetActive(true);
    }

    public void DeactivateUI(GameObject o)
    {
        o.SetActive(false);
    }
}
