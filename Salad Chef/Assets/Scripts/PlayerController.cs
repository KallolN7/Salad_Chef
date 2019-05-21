using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;

    [HideInInspector]
   public GameObject[] vegetablesCarryingArray = new GameObject[2];
    [HideInInspector]
    public GameObject saladBowl;
    [HideInInspector]
    public string saladCombination;
    [HideInInspector]
    public bool canServe;
    [HideInInspector]
    public bool isChopping;
    [HideInInspector]
    public bool reachedDestination;

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
    public Transform servingPos1, servingPos2, servingPos3;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Q) && !isChopping)
        {
            reachedDestination = false;
            destination = VegetableStops[2].transform;
            Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
            Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
            MoveToTarget();
        }

       else if (Input.GetKeyDown(KeyCode.W) && !isChopping)
        {
            reachedDestination = false;
            destination = VegetableStops[3].transform;
            Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
            Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.E) && !isChopping)
        {
            reachedDestination = false;
            destination = VegetableStops[4].transform;
            Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
            Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.Z) && !isChopping)
        {
            reachedDestination = false;
            destination = VegetableStops[5].transform;
            Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
            Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.X) && !isChopping)
        {
            reachedDestination = false;
            destination = VegetableStops[6].transform;
            Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
            Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.C) && !isChopping)
        {
            reachedDestination = false;
            destination = VegetableStops[7].transform;
            Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
            Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Pick Up";
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            reachedDestination = false;
            destination = VegetableStops[0].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            reachedDestination = false;
            destination = VegetableStops[1].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && canServe)
        {
            reachedDestination = false;
            destination = VegetableStops[8].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && canServe)
        {
            reachedDestination = false;
            destination = VegetableStops[9].transform;
            MoveToTarget();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canServe)
        {
            reachedDestination = false;
            destination = VegetableStops[10].transform;
            MoveToTarget();
        }

        if (destination == null || agent.transform.position.x != destination.position.x)
        {
            return;
        }
        else if(agent.transform.position.x == destination.position.x && !reachedDestination)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, destination.transform.eulerAngles.y, transform.eulerAngles.z);
            reachedDestination = true;
            //Player1ActionButton.SetActive(true);
            //CloseVegButtons();
            //Chopping board options appear.
        }

    }

    void MoveToTarget()
    {       
        Vector3 target = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        agent.SetDestination(target);      
    }


    public void SelectVegetable(int VegID)
    {
        switch (VegID)
        {
            case 0:
                PickupItem(vegetablePrefabs[0]);
                break;
            case 1:
                PickupItem(vegetablePrefabs[1]);
                break;
            case 2:
                PickupItem(vegetablePrefabs[2]);
                break;
            case 3:
                PickupItem(vegetablePrefabs[3]);
                break;
            case 4:
                PickupItem(vegetablePrefabs[4]);
                break;
            case 5:
                PickupItem(vegetablePrefabs[5]);
                break;
            case 6:
                PickupItem(vegetablePrefabs[6]);
                break;
            case 7:
                PickupItem(vegetablePrefabs[7]);
                break;
            case 8:
                PickupItem(vegetablePrefabs[8]);
                break;
            case 9:
                PickupItem(vegetablePrefabs[9]);
                break;
            default:
                break;
        }
    }




    public void PickupItem(GameObject veg)
    {
        if (vegetablesCarryingArray[0] == null)
        {           
           GameObject obj =  Instantiate(veg, bowlHoldingPos.position, bowlHoldingPos.rotation);
            obj.name = veg.name;
            obj.transform.SetParent(transform);
            vegetablesCarryingArray[0] = obj;
            Debug.Log("picked up vegetable in slot 1: " + veg.name);
            return;
        }
        else if (vegetablesCarryingArray[1] == null)
        {
            GameObject obj = Instantiate(veg, bowlHoldingPos.position, bowlHoldingPos.rotation);
            obj.name = veg.name;
            obj.transform.SetParent(transform);
            vegetablesCarryingArray[1] = obj;
            Debug.Log("picked up vegetable in slot 2: " + veg.name);
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
        }   
    }

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



