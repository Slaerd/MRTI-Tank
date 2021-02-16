﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastController : MonoBehaviour
{
    public bool playerTurn; //public cause I made a terrible mistake
    private bool targetMode;
    private int selectedThisLoop;
    private GameObject selectedTank = null;
    private GameObject targetedTank = null;

    private int tankLayerA = 1 << 8;
    private int tankLayerE = 1 << 9;

    [SerializeField] private Camera arCam;
    [SerializeField] private GameObject effectUI;
    [SerializeField] private GameObject targetUI;

    private bool dragMotion; //Checks if selection is done in the same motion as movement for attacking

    public static Action<GameObject,GameObject> onTankDrag;

    // Start is called before the first frame update
    void Start()
    {
        Pass.switchTurn += SetPlayerTurn;
        playerTurn = true;
        targetMode = false;
        dragMotion = false;
        effectUI.SetActive(false);
        targetUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastSelect();
        if (selectedTank != null) 
        { //TODO
            effectUI.GetComponent<Transform>().position =
               new Vector3(200,0,0) + arCam.WorldToScreenPoint(selectedTank.GetComponent<Transform>().position);
            effectUI.SetActive(selectedTank.GetComponent<Renderer>().isVisible);
        }
    }

    public void RaycastSelect()
    {
        if (Input.touchCount > 0 && playerTurn) //no interaction when not your turn
        {
            Ray ray;
            RaycastHit hit = new RaycastHit();
            if (!targetMode) {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    selectedThisLoop = 0; 
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayerA)) //Searches for ally tanks
                    {
                        selectedTank?.GetComponent<Tank>().Unselect();              //swap out selected tank
                        hit.transform.gameObject.GetComponent<Tank>().Select();     //for the new one

                        selectedTank = hit.transform.gameObject;
                        selectedThisLoop++;
                        dragMotion = true;
                    }

                }

                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    if (dragMotion) //if we're in the same loop
                    {
                        Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hit2;

                        if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, tankLayerA) //Look for enemy tanks 
                            && !GameObject.ReferenceEquals(selectedTank, hit2.transform.gameObject))
                        {
                            targetedTank = hit2.transform.gameObject;
                            targetedTank.GetComponent<Tank>().Target(); //Attack feedback
                        }
                    }
                    else
                    {
                        targetedTank?.GetComponent<Tank>().Untarget();
                        targetedTank = null;
                    }
                }

                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (dragMotion && targetedTank != null) //If there's a target
                    {
                        onTankDrag.Invoke(selectedTank, targetedTank); //Attack
                    }
                    if (selectedThisLoop == 0 && targetMode != true)    //Only unselect when clicking off the tank
                    {                                                   //AND we're not touching the effect UI
                        effectUI.SetActive(false);
                        selectedTank?.GetComponent<Tank>().Unselect();
                        selectedTank = null;
                    }
                    
                    dragMotion = false;
                }
            }else
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayerA))
                    {
                        selectedTank.GetComponent<Tank>().Effect(hit.transform.GetComponent<Tank>());
                        targetMode = false;
                        targetUI.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        onTankDrag = null;
    }

    public void SetPlayerTurn(bool b)
    {
        playerTurn = b;
    }

    public void SetTargetMode(bool b)
    {
        targetMode = b;
    }

    public void AntiDeselect()
    {
        selectedThisLoop++;
    }
}
