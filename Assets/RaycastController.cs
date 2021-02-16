using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastController : MonoBehaviour
{
    public bool playerTurn;
    private bool targetMode;
    private int counter;
    private GameObject selectedTank = null;
    private GameObject targetedTank = null;
    private int uiLayer = 1 << 5;
    private int tankLayerA = 1 << 8;
    private int tankLayerE = 1 << 9;
    [SerializeField] private Material tankTextureBasic;
    [SerializeField] private Material tankTextureSelected;
    [SerializeField] private Camera arCam;
    [SerializeField] private GameObject effectUI;
    [SerializeField] private GameObject targetUI;
    private bool dragMotion;
    public static Action<GameObject,GameObject> onTankDrag;

    // Start is called before the first frame update
    void Start()
    {
        Pass.switchTurn += setPlayerTurn;
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
            effectUI.GetComponent<RectTransform>().position =
                arCam.WorldToScreenPoint(selectedTank.GetComponent<Transform>().position);
            effectUI.SetActive(selectedTank.GetComponent<Renderer>().isVisible);
        }
    }

    public void RaycastSelect()
    {
        if (Input.touchCount > 0 && playerTurn)
        {
            Ray ray;
            RaycastHit hit = new RaycastHit();
            if (!targetMode) {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    counter = 0;
                    Debug.Log("Touch");
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayerA))
                    {
                        selectedTank?.GetComponent<Tank>().Unselect();
                        hit.transform.gameObject.GetComponent<Tank>().Select();
                        selectedTank = hit.transform.gameObject;
                        counter++;
                        dragMotion = true;
                    }

                }

                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    if (dragMotion)
                    {
                        Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hit2;

                        if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, tankLayerA)
                            && !GameObject.ReferenceEquals(selectedTank, hit2.transform.gameObject))
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

                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (dragMotion && targetedTank != null)
                    {
                        onTankDrag.Invoke(selectedTank, targetedTank);
                    }
                    if (counter == 0 && targetMode != true)
                    {
                        effectUI.SetActive(false);
                        selectedTank?.GetComponent<Tank>().Unselect();
                        selectedTank = null;
                    }
                    
                    dragMotion = false;
                }
            }else//Target stuff
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    Debug.Log("Touch");
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, tankLayerA))
                    {//selected sometimes empty
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

    public void setPlayerTurn(bool b)
    {
        playerTurn = b;
    }

    public void setTargetMode(bool b)
    {
        targetMode = b;
    }

    public void AntiDeselect()
    {
        counter++;
    }
}
