﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public bool playerTurn; //public cause I made a terrible mistake
    private bool targetMode;
    private int selectedThisLoop;
    private GameObject selectedTank = null;
    private GameObject targetedTank = null;

    private int tankLayerP = 1 << 8;
    private int tankLayerE = 1 << 9;

    [SerializeField] private Camera arCam;
    [SerializeField] private GameObject effectUI;
    [SerializeField] private GameObject targetUI;
    [SerializeField] private Text endGame;

    private bool dragMotion; //Checks if selection is done in the same motion as movement for attacking

    public static Action<GameObject,GameObject> onTankDrag;

    // Start is called before the first frame update
    void Start()
    {
        Tank.on0HP += EndGame;
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
        {
            effectUI.GetComponent<RectTransform>().position = new Vector3(200,0,0) + arCam.WorldToScreenPoint(selectedTank.GetComponent<Transform>().position);
            effectUI.SetActive(selectedTank.GetComponent<Renderer>().isVisible);
        }
    }

    public void RaycastSelect()
    {
        if (Input.touchCount > 0) //no interaction when not your turn
        {
            Ray ray;
            RaycastHit hit = new RaycastHit();
            if (!targetMode) {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    selectedThisLoop = 0; 
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayerP)) //Searches for ally tanks
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
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit2;
                    if (dragMotion && Physics.Raycast(ray2, out hit2, Mathf.Infinity, tankLayerE) //if we're in the same loop
                        && !GameObject.ReferenceEquals(selectedTank, hit2.transform.gameObject))
                    {
                            targetedTank = hit2.transform.gameObject;
                            targetedTank.GetComponent<Tank>().Target(); //Attack feedback
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
                        targetedTank.GetComponent<Tank>().Untarget();
                        targetedTank = null;
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

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayerP))
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
        if (b)
        {
            tankLayerP = 1 << 8;
            tankLayerE = 1 << 9;
        }
        else
        {
            tankLayerP = 1 << 9;
            tankLayerE = 1 << 8;
        }
    }

    public void SetTargetMode(bool b)
    {
        targetMode = b;
    }

    public void AntiDeselect()
    {
        selectedThisLoop++;
    }

    private void EndGame()
    {
        if (!playerTurn) {
            endGame.text = "YOU\nLOST";
            endGame.color = Color.red;
        }
        else
        {
            endGame.text = "GGEZ";
            endGame.color = Color.green;
        }
            
    }
}
