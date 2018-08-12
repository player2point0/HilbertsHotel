using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int MaxHeight;
    public int MinHeight;
    public GameObject[] Rooms;
    public Transform RoomsTransform;

    private GridController gridController;
    private List<PersonController> people = new List<PersonController>();

    private void Start()
    {
        gridController = FindObjectOfType<GridController>();
        people.AddRange(GetComponentsInChildren<PersonController>());

        Debug.Log(people.Count);
    }

    private void Update()
    {
        for(int i = 0;i<gridController.blockParents.Length;i++)
        {
            float y = gridController.blockParents[i].transform.position.y;

            if (y >= MaxHeight)
            {
                int index = Random.Range(0, Rooms.Length);

                Destroy(gridController.blockParents[i].gameObject);

                gridController.blockParents[i] = Instantiate(Rooms[index], RoomsTransform).GetComponent<BlockParentController>();
            }

            else if(y <= MinHeight)
            {
                PersonController person = gridController.blockParents[i].GetComponent<PersonController>();

                if (person == null && people.Count > 0)
                {
                    person = people[0];

                    people.RemoveAt(0);

                    person.transform.parent = gridController.blockParents[i].transform;

                    //person.transform.position = Vector3.zero;
                }
                
            }

        }



    }


    void RemoveRoom()
    {

    }

    void FillRoom()
    {

    }
}
