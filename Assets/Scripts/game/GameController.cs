using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int MaxHeight;
    public int MinHeight;
    public int MaxWidthRight;
    public GameObject[] Rooms;
    public Transform RoomsTransform;
    public Transform PeopleTrasfrom;
    public GameObject Customer;
    public GameObject Roof;
    public Text ScoreText;
    public Text GameOverText;
    public int StartMoney;

    private SceneController sceneController;
    private GridController gridController;
    private List<PersonController> Customers = new List<PersonController>();
    private bool DeliveryEmpty;
    private int Money;

    private void Start()
    {
        gridController = FindObjectOfType<GridController>();
        Customers.AddRange(GetComponentsInChildren<PersonController>());
        sceneController = FindObjectOfType<SceneController>();
        DeliveryEmpty = true;
        GameOverText.enabled = false;
        Money = StartMoney;
        AddCost(0);
        SpawnCustomer();
    }

    private void Update()
    {
        DeliveryEmpty = true;
        OpenRoof();

        for(int j = 0;j<gridController.blockParents.Length; j++)
        {
            float x = gridController.blockParents[j].transform.position.x;

            if (x > MaxWidthRight)
            {
                DeliveryEmpty = false;
                CloseRoof();
            }
        }

        for(int i = 0;i<gridController.blockParents.Length;i++)
        {
            BlockParentController Block = gridController.blockParents[i];
            PersonController person = Block.GetComponentInChildren<PersonController>();
            float y = Block.transform.position.y;

            if (y >= MaxHeight && DeliveryEmpty)
            {
                //spawn roon
                int index = Random.Range(0, Rooms.Length);

                Destroy(gridController.blockParents[i].gameObject);

                AddCost(-10);

                gridController.blockParents[i] = Instantiate(Rooms[index], RoomsTransform).GetComponent<BlockParentController>();

                if(person != null)
                {
                    AddCost(person.CurrentCost);
                }

                DeliveryEmpty = false;
                CloseRoof();
                Invoke("SpawnCustomer", Random.Range(0, 3));
            }

            else if(y <= MinHeight)
            {

                if (person == null && Customers.Count > 0)
                {
                    //fill romm
                    person = Customers[0];
                    Customers.RemoveAt(0);

                    person.transform.parent = gridController.blockParents[i].transform;
                    person.GetComponent<Rigidbody>().isKinematic = true;

                    person.transform.position = Block.CustomerPos + Block.transform.position;
                    person.StartPriceDecrease();
                }   
            }
        }
    }

    void AddCost(int amount)
    {
        Money += amount;
        ScoreText.text = "Space Dosh : "+Money.ToString();

        if(Money <= 0)
        {
            Time.timeScale = 0.5f;
            GameOverText.enabled = true;

            sceneController.Invoke("LoadMenu", 3);
        }
    }

    void SpawnCustomer()
    {
        Customers.Add(Instantiate(Customer, PeopleTrasfrom).GetComponent<PersonController>());
    }
    void OpenRoof()
    {
        Debug.Log("open");

        Roof.transform.position = new Vector3(0, 10, 0);
    }
    void CloseRoof()
    {
        Debug.Log("close");

        Roof.transform.position = new Vector3(0, 5, 0);
    }

}
