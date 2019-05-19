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

    public GameObject[] vegetablePrefabs;
    public GameObject saladBowlPrefab;
    public GameObject choppingBoardFirst, choppingBoardSecond;
    public GameObject[] VegetableStops;
    private GameObject selectedChoppingBoard;
    bool canpickup = true;
    private Transform destination;
    public ManagerScript manage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Q))
        {
            destination = VegetableStops[2].transform;
            MoveToTarget();
        }

       else if (Input.GetKeyDown(KeyCode.W))
        {
            destination = VegetableStops[3].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            destination = VegetableStops[4].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.Z))
        {
            destination = VegetableStops[5].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            destination = VegetableStops[6].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            destination = VegetableStops[7].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            destination = VegetableStops[0].transform;
            MoveToTarget();
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            destination = VegetableStops[1].transform;
            MoveToTarget();
        }

        if (destination == null || agent.transform.position.x != destination.position.x)
        {
            return;
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, destination.transform.eulerAngles.y, transform.eulerAngles.z);
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
            vegetablesCarryingArray[0] = veg;
            Debug.Log("picked up vegetable in slot 1: " + veg.name);
        }
        else if (vegetablesCarryingArray[1] == null)
        {
            vegetablesCarryingArray[1] = veg;
            Debug.Log("picked up vegetable in slot 2: " + veg.name);
        }
        else
            return;
    }


    public void DropVegetables()
    {
        if (vegetablesCarryingArray[0] != null)
        {
            Instantiate(vegetablesCarryingArray[0], selectedChoppingBoard.transform);
        }
        else
        {
            Instantiate(vegetablesCarryingArray[1], selectedChoppingBoard.transform);
        }
    }



    public void DropAtSidePlate()
    {
        //drop item at side plate
    }

    public void PickupFromSidePlate()
    {
        //pickup item from side plate
    }

    public void ServeCustomer()
    {
        //take plate and serve  customer
    }




}
