using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class Player2Controller : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;

    [HideInInspector]
    public GameObject[] vegetablesCarryingArray = new GameObject[2];
    [HideInInspector]
    public int[] orderID = new int[2];
    [HideInInspector]
    public GameObject saladBowl;
    [HideInInspector]
    public string saladCombination;
    [HideInInspector]
    public int saladCombinationID;
    [HideInInspector]
    public bool canServe;
    [HideInInspector]
    public bool isChopping;
    [HideInInspector]
    public bool reachedDestination;
    [HideInInspector]
    public int player1Points;

    public ManagerScript manager;
    public GameObject[] vegetablePrefabs;
    public GameObject saladBowlPrefab;
    public GameObject choppingBoardFirst, choppingBoardSecond;
    public GameObject[] VegetableStops;
    public GameObject[] Player1Buttons;
    public GameObject Player1ActionButton;
    private GameObject selectedChoppingBoard;
    bool canpickup = true;

    [HideInInspector]
    public Transform destination;
    public Transform bowlHoldingPos;
    public Transform servingPos1, servingPos2, servingPos3, servingPos4, servingPos5;
    public Text vegetableSlot1;
    public Text vegetableSlot2;
    public GameObject canvas;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        ResetVegetableButtons();
        CloseVegButtons();
        Player1ActionButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        canvas.transform.eulerAngles = new Vector3(0, -90, 0);

        if (manager.isGameOn)
        {

            if (vegetablesCarryingArray[0] != null)
                vegetableSlot1.text = vegetablesCarryingArray[0].name;
            else
                vegetableSlot1.text = "";


            if (vegetablesCarryingArray[1] != null)
                vegetableSlot2.text = vegetablesCarryingArray[1].name;
            else
                vegetableSlot2.text = "";





            if (Input.GetKeyDown(KeyCode.P) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[2].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "L";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.LeftBracket) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[3].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "L";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.RightBracket) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[4].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "L";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.Comma) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[5].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "L";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.Period) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[6].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "L";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.Slash) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[7].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "L";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                reachedDestination = false;
                destination = VegetableStops[0].transform;
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.Quote))
            {
                reachedDestination = false;
                destination = VegetableStops[1].transform;
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha6) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[8].transform;
                MoveToTarget();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha7) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[9].transform;
                MoveToTarget();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[10].transform;
                MoveToTarget();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[11].transform;
                MoveToTarget();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[12].transform;
                MoveToTarget();
            }

            if (destination == null || agent.transform.position.x != destination.position.x)
            {
                return;
            }
            else if (agent.transform.position.x == destination.position.x && !reachedDestination)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, destination.transform.eulerAngles.y, transform.eulerAngles.z);
                reachedDestination = true;
                //Player1ActionButton.SetActive(true);
                //CloseVegButtons();
                //Chopping board options appear.
            }
            else
                return;

        }


    }

    void MoveToTarget()
    {
        Vector3 target = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        agent.SetDestination(target);
    }


    public void SelectVegetable(int VegID, int orderCalculationID)
    {
        switch (VegID)
        {
            case 0:
                PickupItem(vegetablePrefabs[0], orderCalculationID);
                break;
            case 1:
                PickupItem(vegetablePrefabs[1], orderCalculationID);
                break;
            case 2:
                PickupItem(vegetablePrefabs[2], orderCalculationID);
                break;
            case 3:
                PickupItem(vegetablePrefabs[3], orderCalculationID);
                break;
            case 4:
                PickupItem(vegetablePrefabs[4], orderCalculationID);
                break;
            case 5:
                PickupItem(vegetablePrefabs[5], orderCalculationID);
                break;
            case 6:
                PickupItem(vegetablePrefabs[6], orderCalculationID);
                break;
            case 7:
                PickupItem(vegetablePrefabs[7], orderCalculationID);
                break;
            case 8:
                PickupItem(vegetablePrefabs[8], orderCalculationID);
                break;
            case 9:
                PickupItem(vegetablePrefabs[9], orderCalculationID);
                break;
            default:
                break;
        }
    }




    public void PickupItem(GameObject veg, int orderCalculationID)
    {
        if (vegetablesCarryingArray[0] == null)
        {
            GameObject obj = Instantiate(veg, bowlHoldingPos.position, bowlHoldingPos.rotation);
            obj.name = veg.name;
            obj.transform.SetParent(transform);
            vegetablesCarryingArray[0] = obj;
            orderID[0] = orderCalculationID;
            Debug.Log("picked up vegetable in slot 1: " + veg.name);
            Debug.Log("orderID1: " + orderID[0]);
            vegetableSlot1.text = veg.name;
            return;
        }
        else if (vegetablesCarryingArray[1] == null)
        {
            GameObject obj = Instantiate(veg, bowlHoldingPos.position, bowlHoldingPos.rotation);
            obj.name = veg.name;
            obj.transform.SetParent(transform);
            vegetablesCarryingArray[1] = obj;
            orderID[1] = orderCalculationID;
            Debug.Log("picked up vegetable in slot 2: " + veg.name);
            Debug.Log("orderID2: " + orderID[1]);
            vegetableSlot2.text = veg.name;
            return;
        }
        else
            return;
    }


    public void DropVegetables()
    {
        if (vegetablesCarryingArray[0] != null)
        {
            // Instantiate(vegetablesCarryingArray[0], selectedChoppingBoard.transform);
            Destroy(vegetablesCarryingArray[0]);
        }
        else
        {
            //Instantiate(vegetablesCarryingArray[1], selectedChoppingBoard.transform);
            Destroy(vegetablesCarryingArray[1]);
        }
    }


    public void PickUpSaladBowl(GameObject bowl)
    {
        bowl.transform.position = bowlHoldingPos.position;
        bowl.transform.parent = transform;
        saladBowl = bowl;
    }

    public void ServeCustomer(int customer)
    {
        switch (customer)
        {
            case 1:
                saladBowl.transform.parent = null;
                saladBowl.transform.position = servingPos1.position;
                Debug.Log("served: " + saladBowl.name);
                canServe = false;
                break;
            case 2:
                saladBowl.transform.SetParent(servingPos2);
                saladBowl.transform.position = servingPos2.position;
                Debug.Log("served: " + saladBowl.name);
                canServe = false;
                break;
            case 3:
                saladBowl.transform.SetParent(servingPos3);
                saladBowl.transform.position = servingPos3.position;
                Debug.Log("served: " + saladBowl.name);
                canServe = false;
                break;
            case 4:
                saladBowl.transform.SetParent(servingPos4);
                saladBowl.transform.position = servingPos4.position;
                Debug.Log("served: " + saladBowl.name);
                canServe = false;
                break;
            case 5:
                saladBowl.transform.SetParent(servingPos5);
                saladBowl.transform.position = servingPos5.position;
                Debug.Log("served: " + saladBowl.name);
                canServe = false;
                break;
        }
    }

    public void ResetVegetableButtons()
    {
        Player1Buttons[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "P";
        Player1Buttons[0].transform.GetChild(1).GetComponent<Text>().text = "Pickup Onion";
        Player1Buttons[0].SetActive(true);

        Player1Buttons[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "[";
        Player1Buttons[1].transform.GetChild(1).GetComponent<Text>().text = "Pickup Carrot";
        Player1Buttons[1].SetActive(true);

        Player1Buttons[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "]";
        Player1Buttons[2].transform.GetChild(1).GetComponent<Text>().text = "Pickup Tomato";
        Player1Buttons[2].SetActive(true);

        Player1Buttons[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ",";
        Player1Buttons[3].transform.GetChild(1).GetComponent<Text>().text = "Pickup Cucumber";
        Player1Buttons[3].SetActive(true);

        Player1Buttons[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ".";
        Player1Buttons[4].transform.GetChild(1).GetComponent<Text>().text = "Pickup Mushrooms";
        Player1Buttons[4].SetActive(true);

        Player1Buttons[5].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "/";
        Player1Buttons[5].transform.GetChild(1).GetComponent<Text>().text = "Pickup Broccoli";
        Player1Buttons[5].SetActive(true);
    }

    public void CloseVegButtons()
    {
        for (int i = 0; i < Player1Buttons.Length; i++)
        {
            Player1Buttons[i].SetActive(false);
        }
    }

    public void OpenServeButtons()
    {
        Player1Buttons[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "6";
        Player1Buttons[0].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer1";
        Player1Buttons[0].SetActive(true);

        Player1Buttons[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "7";
        Player1Buttons[1].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer2";
        Player1Buttons[1].SetActive(true);

        Player1Buttons[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "8";
        Player1Buttons[2].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer3";
        Player1Buttons[2].SetActive(true);

        Player1Buttons[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "9";
        Player1Buttons[3].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer4";
        Player1Buttons[3].SetActive(true);

        Player1Buttons[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
        Player1Buttons[4].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer5";
        Player1Buttons[4].SetActive(true);
    }
}
