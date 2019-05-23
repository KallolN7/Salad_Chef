using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;                                                                                                                   //reference to rigidbody
    NavMeshAgent agent;                                                                                                  // reference to navmesh agent component

    [HideInInspector]
   public GameObject[] vegetablesCarryingArray = new GameObject[2];                     //array for holding 2 vegetable
    [HideInInspector]
    public int[] orderID = new int[2];                                                                                       // array to store order id of each vegetable
    [HideInInspector]
    public GameObject saladBowl;                                                                               //salad bowl carried by player
    [HideInInspector]
    public string saladCombination;                                                                                //salad combination player is carrying at a time
    [HideInInspector]
    public int saladCombinationID;                                                                                 //salad id to calculate andmatch with customer order later
    [HideInInspector]
    public bool canServe;                                                                                    //if a player  can serve a customer or not
    [HideInInspector]
    public bool isChopping;                                                                                  //if player is chopping or not
    [HideInInspector] 
    public bool reachedDestination;                                                                 //if player has reached destination or not
    [HideInInspector]
    public int player1Points;                                                                                   //points earned by player

    public ManagerScript manager;
    public GameObject[] vegetablePrefabs;                                                              //all vegetable prefabs
    public GameObject saladBowlPrefab;                                                                   // salad bowl prefab
    public GameObject choppingBoardFirst, choppingBoardSecond;                  //reference to chopping boards
    public GameObject[] VegetableStops;                                                              //all destinations specific to vegetables
    public GameObject[] Player1Buttons;                                                             //all ui buttons
    public GameObject Player1ActionButton;
    private GameObject selectedChoppingBoard;                                            //chopping board used by player
    bool canpickup = true;

    [HideInInspector]
    public Transform destination;                                                                          //current destination to travel to 
    public Transform bowlHoldingPos;                                                                 
    public Transform servingPos1, servingPos2, servingPos3, servingPos4, servingPos5;                 //all customer positions to go to

   //to display vegetables carried by player
    public Text vegetableSlot1;                                                                               
    public Text vegetableSlot2;
    public GameObject canvas;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        CloseVegButtons();
        Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Space";
        Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Start";
        Player1ActionButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        canvas.transform.eulerAngles = new Vector3(0,-90,0);

        if (manager.isGameOn)
        {
            //keeping vegetable display canvas at 90 all the time so that player can look at it without any problem

            if (vegetablesCarryingArray[0] != null)
                vegetableSlot1.text = vegetablesCarryingArray[0].name;
            else
                vegetableSlot1.text = "";


            if (vegetablesCarryingArray[1] != null)
                vegetableSlot2.text = vegetablesCarryingArray[1].name;
            else
                vegetableSlot2.text = "";


            //go to pickup a particular vegetable and the buttons to show all options on reaching destination
            if (Input.GetKeyDown(KeyCode.Q) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[2].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }
            //go to pickup a particular vegetable and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.W) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[3].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }
            //go to pickup a particular vegetable and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.E) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[4].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }
            //go to pickup a particular vegetable and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.Z) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[5].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }
            //go to pickup a particular vegetable and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.X) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[6].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }
            //go to pickup a particular vegetable and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.C) && !isChopping)
            {
                reachedDestination = false;
                destination = VegetableStops[7].transform;
                Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
                MoveToTarget();
            }

            //go to chopping board and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.S))
            {
                reachedDestination = false;
                destination = VegetableStops[0].transform;
                MoveToTarget();
            }


            //go to serve a customer and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.Alpha1) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[8].transform;
                MoveToTarget();
            }
            //go to serve a customer and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.Alpha2) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[9].transform;
                MoveToTarget();
            }
            //go to serve a customer and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.Alpha3) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[10].transform;
                MoveToTarget();
            }
            //go to serve a customer and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.Alpha4) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[10].transform;
                MoveToTarget();

            }
            //go to serve a customer and the buttons to show all options on reaching destination
            else if (Input.GetKeyDown(KeyCode.Alpha5) && canServe)
            {
                reachedDestination = false;
                destination = VegetableStops[10].transform;
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

    //using navmesh agent to move to a particular location 
    void MoveToTarget()
    {       
        Vector3 target = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        agent.SetDestination(target);      
    }

    //function to select particular vegetable
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



    //pickup vegetable and instantiating it
    public void PickupItem(GameObject veg, int orderCalculationID)
    {
        if (vegetablesCarryingArray[0] == null)
        {           
           GameObject obj =  Instantiate(veg, bowlHoldingPos.position, bowlHoldingPos.rotation);
            obj.name = veg.name;
            obj.transform.SetParent(transform);
            vegetablesCarryingArray[0] = obj;
            orderID[0] = orderCalculationID;
            Debug.Log("picked up vegetable in slot 1: " + veg.name);
            Debug.Log("orderID1: " + orderID[0]);
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
            return;
        }
        else
            return;
    }


    public void DropVegetables()
    {
        if (vegetablesCarryingArray[0] != null)
        {
            Destroy(vegetablesCarryingArray[0]);
        }
        else
        {
            Destroy(vegetablesCarryingArray[1]);
        }
    }

    //pickup salad bowl
    public void PickUpSaladBowl(GameObject bowl)
    {
        bowl.transform.position = bowlHoldingPos.position;
        bowl.transform.parent = transform;
        saladBowl = bowl;
    }

    //serve a customer
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

    //reset all ui buttons to show all vegetable options
    public void ResetVegetableButtons()
    {
        Player1Buttons[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Q";
        Player1Buttons[0].transform.GetChild(1).GetComponent<Text>().text = "Pickup Onion";
        Player1Buttons[0].SetActive(true);

        Player1Buttons[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "W";
        Player1Buttons[1].transform.GetChild(1).GetComponent<Text>().text = "Pickup Carrot";
        Player1Buttons[1].SetActive(true);

        Player1Buttons[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "E";
        Player1Buttons[2].transform.GetChild(1).GetComponent<Text>().text = "Pickup Tomato";
        Player1Buttons[2].SetActive(true);

        Player1Buttons[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Z";
        Player1Buttons[3].transform.GetChild(1).GetComponent<Text>().text = "Pickup Cucumber";
        Player1Buttons[3].SetActive(true);

        Player1Buttons[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X";
        Player1Buttons[4].transform.GetChild(1).GetComponent<Text>().text = "Pickup Mushrooms";
        Player1Buttons[4].SetActive(true);

        Player1Buttons[5].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "C";
        Player1Buttons[5].transform.GetChild(1).GetComponent<Text>().text = "Pickup Broccoli";
        Player1Buttons[5].SetActive(true);
    }

    public void CloseVegButtons()
    {
        for(int i = 0; i < Player1Buttons.Length; i ++)
        {
            Player1Buttons[i].SetActive(false);
        }
    }

    //show all buttons to serve customer
    public void OpenServeButtons()
    {
        Player1Buttons[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "1";
        Player1Buttons[0].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer1";
        Player1Buttons[0].SetActive(true);

        Player1Buttons[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "2";
        Player1Buttons[1].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer2";
        Player1Buttons[1].SetActive(true);

        Player1Buttons[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "3";
        Player1Buttons[2].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer3";
        Player1Buttons[2].SetActive(true);

        Player1Buttons[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "4";
        Player1Buttons[3].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer4";
        Player1Buttons[3].SetActive(true);

        Player1Buttons[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "5";
        Player1Buttons[4].transform.GetChild(1).GetComponent<Text>().text = "Serve Customer5";
        Player1Buttons[4].SetActive(true);
    }
}



