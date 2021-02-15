using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastController : MonoBehaviour
{
    private GameObject selectedTank = null;
    private int tankLayerA = 1 << 8;
    private int tankLayerE = 1 << 9;
    [SerializeField] private Material tankTextureBasic;
    [SerializeField] private Material tankTextureSelected;
    private bool dragMotion;
    public static Action<GameObject,GameObject> onTankDrag;

    // Start is called before the first frame update
    void Start()
    {
        dragMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastSelect();
    }

    public void RaycastSelect()
    {
        if (Input.touchCount > 0)
        {
            Ray ray;
            RaycastHit hit = new RaycastHit();
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Debug.Log("Touch");
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayerA))
                {
                    selectedTank?.GetComponent<Tank>().ToggleSelect();
                    hit.transform.gameObject.GetComponent<Tank>().ToggleSelect();
                    selectedTank = hit.transform.gameObject;
                    dragMotion = true;
                }
                else
                {
                    selectedTank?.GetComponent<Tank>().ToggleSelect();
                    selectedTank = null;
                }
            }
            
            if(Input.touches[0].phase == TouchPhase.Ended)
            {
                if (dragMotion)
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit2;

                    if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, tankLayerA))
                    {
                        onTankDrag.Invoke(selectedTank, hit2.transform.gameObject);
                    }
                }
                dragMotion = false;
            }
        }
    }

    private void OnDestroy()
    {
        onTankDrag = null;
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
