using System.Collections;
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

        else if (Input.GetKeyDown(KeyCode.LeftControl) && canDrop)
        {
            DropAtSidePlate();
            canDrop = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && canDrop && canPickupFromSideTable)
        {
            PickUpFromSidePlate();
            canDrop = false;
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) && choppingDone)
        {
            Trash();
            canDrop = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            canDrop = true;
            Debug.Log("player entered zone " + gameObject.name);
            player1 = other.GetComponent<PlayerController>();

            if(player1.vegetablesCarryingArray!= null)
            {
                
                if (other.GetComponent<PlayerController>().vegetablesCarryingArray[1] != null)
                {
                    PlayerVeg2 = other.GetComponent<PlayerController>().vegetablesCarryingArray[1];
                    Debug.Log(PlayerVeg2.name);
                }
                else if(other.GetComponent<PlayerController>().vegetablesCarryingArray[0] != null)
                {
                    PlayerVeg1 = other.GetComponent<PlayerController>().vegetablesCarryingArray[0];
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
            Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LAlt";
            Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Keep at Side Plate";
            Player1ChopButton.SetActive(true);
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
        Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Chop";
        Player1ChopButton.SetActive(true);

        Player1TrashButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCntrl";
        Player1TrashButton.transform.GetChild(1).GetComponent<Text>().text = "Trash";
        Player1TrashButton.SetActive(true);
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
                sidePlateVeg = player1.vegetablesCarryingArray[0];
   
                for( int i=0; i< player1.vegetablePrefabs.Length; i++)
                {
                    if(player1.vegetablePrefabs[i].name == player1.vegetablesCarryingArray[0].name)
                    {
                        Instantiate(player1.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        Destroy(player1.vegetablesCarryingArray[0]);
                    }
                }
                canPickupFromSideTable = true;
                player1.vegetablesCarryingArray[0] = null;
                Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LAlt";
                Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Take from side plate";
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
            else if (player1.vegetablesCarryingArray[1] != null)
            {
                sidePlateVeg = player1.vegetablesCarryingArray[1];
                for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == player1.vegetablesCarryingArray[1].name)
                    {
                        Instantiate(player1.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        Destroy(player1.vegetablesCarryingArray[1]);
                    }
                }
                canPickupFromSideTable = true;
                player1.vegetablesCarryingArray[1] = null;
                Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LAlt";
                Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Take from side plate";
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
            if (player1.vegetablesCarryingArray[0] == null)
            {
                player1.vegetablesCarryingArray[0] = sidePlateVeg;
                 for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                        Instantiate(player1.vegetablePrefabs[i], player1.bowlHoldingPos.transform.position, player1.bowlHoldingPos.transform.rotation);
                    }
                }
                canPickupFromSideTable = false;
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
            else if (player1.vegetablesCarryingArray[1] == null)
            {
                player1.vegetablesCarryingArray[1] = sidePlateVeg;
                for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                        Instantiate(player1.vegetablePrefabs[i], player1.bowlHoldingPos.transform.position, player1.bowlHoldingPos.transform.rotation);
                    }
                }
                canPickupFromSideTable = false;
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

    public void SetActiveActionButtonsForPlyer1()
    {
        Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Space";
        Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Chop";
        Player1ChopButton.SetActive(true);

        Player1TrashButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCntrl";
        Player1TrashButton.transform.GetChild(1).GetComponent<Text>().text = "Trash";
        Player1TrashButton.SetActive(true);

        //Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LAlt";
        //Player1ChopButton.transform.GetChild(0).GetComponent<Text>().text = "Keep at Side Plate";

        //Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LAlt";
        //Player1ChopButton.transform.GetChild(0).GetComponent<Text>().text = "Take from side plate";
    }

}
