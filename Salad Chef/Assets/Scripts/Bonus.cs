using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bonus : MonoBehaviour
{
    public int bonusID;
    private PlayerController player1;
    private Player2Controller player2;
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
        else if (other.tag == "Player2")
        {
            Debug.Log("player2 entered bonus area");
            player2 = other.GetComponent<Player2Controller>();

            switch (bonusID)
            {
                case 1:
                    player2.player1Points = player2.player1Points + 10;
                    break;
                case 2:
                    manager.player2CurrCountdownValue = manager.player2CurrCountdownValue + 10;
                    break;
                case 3:
                    StartCoroutine(IncreaseSpeedTemporarilyForPlayer2());
                    break;

            }
            player2.CloseVegButtons();
            player2.Player1ActionButton.SetActive(false);
            player2.ResetVegetableButtons();
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

    IEnumerator IncreaseSpeedTemporarilyForPlayer2()
    {
        float s = player2.GetComponent<NavMeshAgent>().speed;
        player2.GetComponent<NavMeshAgent>().speed = player2.GetComponent<NavMeshAgent>().speed + 3;

        yield return new WaitForSeconds(5);
        player2.GetComponent<NavMeshAgent>().speed = s;

    }
}
