﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastController : MonoBehaviour
{
    public bool playerTurn;
    private GameObject selectedTank = null;
    private GameObject targetedTank = null;
    private int uiLayer = 1 << 5;
    private int tankLayerA = 1 << 8;
    private int tankLayerE = 1 << 9;
    [SerializeField] private Material tankTextureBasic;
    [SerializeField] private Material tankTextureSelected;
    [SerializeField] private Camera arCam;
    [SerializeField] private GameObject effectUI;
    private bool dragMotion;
    public static Action<GameObject,GameObject> onTankDrag;

    // Start is called before the first frame update
    void Start()
    {
        Pass.switchTurn += setPlayerTurn;
        playerTurn = true;
        dragMotion = false;
        effectUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        RaycastSelect();
        if (selectedTank != null) 
        {
            effectUI.GetComponent<RectTransform>().position =
                new Vector3(0, -100, 0) + arCam.WorldToScreenPoint(selectedTank.GetComponent<Transform>().position);
            effectUI.SetActive(selectedTank.GetComponent<Renderer>().isVisible);
        }
    }

    public void RaycastSelect()
    {
        if (Input.touchCount > 0 && playerTurn)
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
                    effectUI.SetActive(true);
                }
                else
                {
                    Debug.Log("Enter else");
                    if(!Physics.Raycast(ray, out hit, Mathf.Infinity, uiLayer))
                    {
                        Debug.Log("Not clicking UI");
                        effectUI.SetActive(false);
                        selectedTank?.GetComponent<Tank>().ToggleSelect();
                        selectedTank = null;
                    }
                }
                
            }
            
            if(Input.touches[0].phase == TouchPhase.Moved)
            {
                if (dragMotion)
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit2;

                    if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, tankLayerA)
                        && !GameObject.ReferenceEquals(selectedTank,hit2.transform.gameObject))
                    {
                        targetedTank = hit2.transform.gameObject;
                        targetedTank.GetComponent<Tank>().Target();
                    }
                    else
                    {
                        targetedTank?.GetComponent<Tank>().Untarget();
                        targetedTank = null;
                    }
                }
            }

            if(Input.touches[0].phase == TouchPhase.Ended)
            {
                if (dragMotion && targetedTank != null)
                {
                    onTankDrag.Invoke(selectedTank, targetedTank);
                }
                dragMotion = false;
            }
        }
    }

    private void OnDestroy()
    {
        onTankDrag = null;
    }

    public void setPlayerTurn(bool b)
    {
        playerTurn = b;
    }
}