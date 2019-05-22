using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bonus : MonoBehaviour
{
    public int bonusID;
    private PlayerController player1;
    public ManagerScript manager;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager == null)
            manager = FindObjectOfType<ManagerScript>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1")
        {
            Debug.Log("player entered bonus area");
            player1 = other.GetComponent<PlayerController>();
          
            switch(bonusID)
            {
                case 1:
                    player1.player1Points = player1.player1Points + 10;
                    break;
                case 2:
                    manager.player1CurrCountdownValue = manager.player1CurrCountdownValue + 10;
                    break;
                case 3:
                    StartCoroutine(IncreaseSpeedTemporarily());
                    break;
      
            }
            player1.CloseVegButtons();
            player1.Player1ActionButton.SetActive(false);
            player1.ResetVegetableButtons();
            Destroy(gameObject);

        }
    }


    IEnumerator IncreaseSpeedTemporarily()
    {
        float s = player1.GetComponent<NavMeshAgent>().speed;
        player1.GetComponent<NavMeshAgent>().speed = player1.GetComponent<NavMeshAgent>().speed + 3;

        yield return new WaitForSeconds(5);
        player1.GetComponent<NavMeshAgent>().speed = s;

    }
}
