﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingScript : MonoBehaviour
{
    private PlayerController player1;
    bool canDrop;
    bool choppingDone;
    bool canPickupFromSideTable;
    float currCountdownValue;
    public GameObject saladBowl;
    public GameObject spawnPos;
    public GameObject sidePlatePos;
    public GameObject Player1ChopButton;
    public GameObject Player1SidePlateButton;
    public GameObject Player1TrashButton;
    public GameObject Player1ServeButton;
    private GameObject PlayerVeg1, PlayerVeg2;
    private GameObject spawnedBowl;
    private string combination;
    GameObject sidePlateVeg;
    GameObject ChoppingVeg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space) && canDrop)
        {
            Chop();
            canDrop = false;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && choppingDone)
        {
            PickUpSaladBowl();
            choppingDone = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt) && choppingDone)
        {
            Chop();
            canDrop = false;
        }

        else if (Input.GetKeyDown(KeyCode.LeftControl) && canDrop && !canPickupFromSideTable)
        {
            DropAtSidePlate();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && canPickupFromSideTable)
        {
            PickUpFromSidePlate();
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) && choppingDone)
        {
            Trash();

        }
        else if (Input.GetKeyDown(KeyCode.Tab) && choppingDone)
        {
            player1.OpenServeButtons();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            canDrop = true;
            Debug.Log("player entered zone " + gameObject.name);
            player1 = other.GetComponent<PlayerController>();

            if(player1.vegetablesCarryingArray!= null && player1.destination == transform)
            {
                player1.CloseVegButtons();
                player1.Player1ActionButton.SetActive(false);

                Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Space";
                Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Chop";
                Player1ChopButton.SetActive(true);

                if (other.GetComponent<PlayerController>().vegetablesCarryingArray[1] != null)
                {
                    PlayerVeg2 = other.GetComponent<PlayerController>().vegetablesCarryingArray[1];

                    Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                    Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Keep at Side Plate";
                    Player1SidePlateButton.SetActive(true);
                    Debug.Log(PlayerVeg2.name);
                }
                else if(other.GetComponent<PlayerController>().vegetablesCarryingArray[0] != null)
                {
                    PlayerVeg1 = other.GetComponent<PlayerController>().vegetablesCarryingArray[0];
                    Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                    Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Keep at Side Plate";
                    Player1SidePlateButton.SetActive(true);
                    Debug.Log(PlayerVeg1.name);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            canDrop = false;
           
            player1.CloseVegButtons();
            Player1ChopButton.SetActive(false);
            player1 = null;
            Debug.Log("player left zone " + gameObject.name);
        }
    }

    public void Chop()
    {
        if (player1.vegetablesCarryingArray != null)
        {
            if (player1.vegetablesCarryingArray[0] != null)
            {
                player1.isChopping = true;
                ChoppingVeg = player1.vegetablesCarryingArray[0];
                combination = combination + ChoppingVeg.name;
                Destroy(player1.vegetablesCarryingArray[0]);
                StartCoroutine(Countdown(5));
                player1.vegetablesCarryingArray[0] = null;
            }
            else if (player1.vegetablesCarryingArray[1] != null)
            {
                player1.isChopping = true;
                ChoppingVeg = player1.vegetablesCarryingArray[1];
                combination = combination + ChoppingVeg.name;
                Destroy(player1.vegetablesCarryingArray[1]);
                StartCoroutine(Countdown(5));
                player1.vegetablesCarryingArray[1] = null;
            }
        }          
            else
                return;
        //combine vegetable if any previous chopped vegetables present
    }


    public IEnumerator Countdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        Debug.Log(combination);
        Destroy(spawnedBowl);
       spawnedBowl = Instantiate(saladBowl, spawnPos.transform.position, spawnPos.transform.rotation);
        spawnedBowl.name = combination;
        choppingDone = true;
        player1.isChopping = false;

        Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Space";
        Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup Bowl";
        Player1ChopButton.SetActive(true);

        Player1TrashButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LShift";
        Player1TrashButton.transform.GetChild(1).GetComponent<Text>().text = "Trash";
        Player1TrashButton.SetActive(true);

        Player1ServeButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Esc";
        Player1ServeButton.transform.GetChild(1).GetComponent<Text>().text = "Serve";
        Player1ServeButton.SetActive(true);

        if(sidePlateVeg == null)
        {
            Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCntrl";
            Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at side plate";
            Player1SidePlateButton.SetActive(true);
        }
        else
        {
            Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCntrl";
            Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "pickup from side plate";
            Player1SidePlateButton.SetActive(true);
        }
        

       
    }

    public void PickUpSaladBowl()
    {
        player1.PickUpSaladBowl(spawnedBowl);
        //player1.saladBowl = spawnedBowl;
        player1.saladCombination = combination;
        player1.canServe = true;
        saladBowl = null;
        combination = null;
    }


    public void DropAtSidePlate()
    {
        if (sidePlateVeg == null)
        {
            if (player1.vegetablesCarryingArray[0] != null)
            {
                //sidePlateVeg = player1.vegetablesCarryingArray[0];
                //sidePlateVeg.name = player1.vegetablesCarryingArray[0].name;

                for ( int i=0; i< player1.vegetablePrefabs.Length; i++)
                {
                    if(player1.vegetablePrefabs[i].name == player1.vegetablesCarryingArray[0].name)
                    {
                        sidePlateVeg = Instantiate(player1.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        sidePlateVeg.name = player1.vegetablePrefabs[i].name;
                        Destroy(player1.vegetablesCarryingArray[0]);
                    }
                }
                canPickupFromSideTable = true;
                player1.vegetablesCarryingArray[0] = null;
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup from Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
            else if (player1.vegetablesCarryingArray[1] != null)
            {
                //sidePlateVeg = player1.vegetablesCarryingArray[1];
                //sidePlateVeg.name = player1.vegetablesCarryingArray[1].name;

                for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == player1.vegetablesCarryingArray[1].name)
                    {
                        sidePlateVeg = Instantiate(player1.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        sidePlateVeg.name = player1.vegetablePrefabs[i].name;
                        Destroy(player1.vegetablesCarryingArray[1]);
                    }
                }
                canPickupFromSideTable = true;
                player1.vegetablesCarryingArray[1] = null;
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup from Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
        }
        else
            return;
    }

    public void PickUpFromSidePlate()
    {
        if(sidePlateVeg != null)
        {
            Debug.Log(sidePlateVeg.name);
            if (player1.vegetablesCarryingArray[0] == null)
            {
                 for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                       GameObject obj = Instantiate(player1.vegetablePrefabs[i], player1.bowlHoldingPos.transform.position, player1.bowlHoldingPos.transform.rotation);
                        obj.transform.SetParent(player1.transform);
                        obj.name = player1.vegetablePrefabs[i].name;
                        player1.vegetablesCarryingArray[0] = obj;
                    }
                }
                canPickupFromSideTable = false;
                Destroy(sidePlateVeg);
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
            else if (player1.vegetablesCarryingArray[1] == null)
            {
                for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                        GameObject obj = Instantiate(player1.vegetablePrefabs[i], player1.bowlHoldingPos.transform.position, player1.bowlHoldingPos.transform.rotation);
                        obj.transform.SetParent(player1.transform);
                        obj.name = player1.vegetablePrefabs[i].name;
                        player1.vegetablesCarryingArray[1] = obj;
                    }
                }
                canPickupFromSideTable = false;
                Destroy(sidePlateVeg);
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
        }
    }

    public void Trash()
    {
        //destination = VegetableStops[11].transform;
        //Vector3 target = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        //agent.SetDestination(target);
        Destroy(spawnedBowl);
        player1.canServe = false;
    }

}
