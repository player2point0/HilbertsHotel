using System.Collections;
using System.Collections.Generic;
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

    private GridController gridController;
    private List<PersonController> Customers = new List<PersonController>();
    private bool DeliveryEmpty;

    private void Start()
    {
        gridController = FindObjectOfType<GridController>();
        Customers.AddRange(GetComponentsInChildren<PersonController>());
        DeliveryEmpty = true;
        SpawnCustomer();
    }

    private void Update()
    {
        DeliveryEmpty = true;
        OpenRoof();

        for(int j = 0;j<gridController.blockParents.Length; j++)
        {
            float x = gridController.blockParents[j].transform.position.x;

            if (x > MaxWidthRight) DeliveryEmpty = false;
        }


        for(int i = 0;i<gridController.blockParents.Length;i++)
        {

            BlockParentController Block = gridController.blockParents[i];
            float y = Block.transform.position.y;

            if (y >= MaxHeight && DeliveryEmpty)
            {
                //spawn roon
                int index = Random.Range(0, Rooms.Length);

                Destroy(gridController.blockParents[i].gameObject);

                gridController.blockParents[i] = Instantiate(Rooms[index], RoomsTransform).GetComponent<BlockParentController>();

                DeliveryEmpty = false;
                CloseRoof();
                SpawnCustomer();
            }

            else if(y <= MinHeight)
            {
                PersonController person = Block.GetComponentInChildren<PersonController>();

                if (person == null && Customers.Count > 0)
                {
                    //fill romm
                    person = Customers[0];
                    Customers.RemoveAt(0);

                    person.transform.parent = gridController.blockParents[i].transform;
                    person.GetComponent<Rigidbody>().isKinematic = true;

                    person.transform.position = Block.CustomerPos + Block.transform.position;
                }   
            }
        }
    }

    void SpawnCustomer()
    {
        Customers.Add(Instantiate(Customer, PeopleTrasfrom).GetComponent<PersonController>());
    }

    void OpenRoof()
    {
        Roof.transform.position = new Vector3(0, 10, 0);
    }

    void CloseRoof()
    {
        Roof.transform.position = new Vector3(0, 5, 0);
    }

}
