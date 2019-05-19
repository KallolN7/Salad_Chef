using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingScript : MonoBehaviour
{
    public PlayerController player;
    bool canDrop;
    float currCountdownValue;
    public GameObject saladBowl;
    public GameObject spawnPos;
    private GameObject PlayerVeg1, PlayerVeg2;

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
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            canDrop = true;
            Debug.Log("player entered zone " + gameObject.name);
           PlayerVeg1 = other.GetComponent<PlayerController>().vegetablesCarryingArray[0];
            Debug.Log(PlayerVeg1.name);
            if(other.GetComponent<PlayerController>().vegetablesCarryingArray[1] != null)
            PlayerVeg2 = other.GetComponent<PlayerController>().vegetablesCarryingArray[1];
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
        StartCoroutine(Countdown(10));
        //combine vegetable if any previous chopped vegetables present
    }


    public IEnumerator Countdown(float countdownValue= 10)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            Debug.Log(currCountdownValue);
            currCountdownValue--;
        }
        Instantiate(saladBowl, spawnPos.transform.position, spawnPos.transform.rotation);
        Debug.Log("chopped: " + PlayerVeg1.name);
        player.vegetablesCarryingArray[0] = null;
    }


}
