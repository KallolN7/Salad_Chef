using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingScript : MonoBehaviour
{
    private PlayerController player1;
    bool canDrop;
    bool choppingDone;
    float currCountdownValue;
    public GameObject saladBowl;
    public GameObject spawnPos;
    public GameObject sidePlatePos;
    private GameObject PlayerVeg1, PlayerVeg2;
    private GameObject spawnedBowl;
    private string combination;
    GameObject sidePlateVeg;

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
            Debug.Log("player left zone " + gameObject.name);
        }
    }

    public void Chop()
    {

        if (player1.vegetablesCarryingArray != null)
        {
            if (player1.vegetablesCarryingArray[0] != null)
            {
                StartCoroutine(Countdown(5, player1.vegetablesCarryingArray[0]));
                player1.vegetablesCarryingArray[0] = null;
            }
            else if (player1.vegetablesCarryingArray[1] != null)
            {
                StartCoroutine(Countdown(5, player1.vegetablesCarryingArray[1]));
                player1.vegetablesCarryingArray[1] = null;
            }
        }          
            else
                return;
        //combine vegetable if any previous chopped vegetables present
    }


    public IEnumerator Countdown(float countdownValue, GameObject obj )
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        combination = combination+ " + "+ obj.name;
        Debug.Log(combination);
        Destroy(spawnedBowl);
       spawnedBowl = Instantiate(saladBowl, spawnPos.transform.position, spawnPos.transform.rotation);
        spawnedBowl.name = combination;
        choppingDone = true;
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
                player1.vegetablesCarryingArray[0] = null;
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
            else if (player1.vegetablesCarryingArray[1] != null)
            {
                sidePlateVeg = player1.vegetablesCarryingArray[1];
                player1.vegetablesCarryingArray[1] = null;
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
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
            else if (player1.vegetablesCarryingArray[1] == null)
            {
                player1.vegetablesCarryingArray[1] = sidePlateVeg;
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
        }
    }


}
