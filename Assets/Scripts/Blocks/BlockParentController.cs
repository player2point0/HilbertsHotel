using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParentController : MonoBehaviour
{
    public BlockController[] children;
    public Vector3 CustomerPos;

    public bool selected = false;
    private GridController gridController;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;
    private Vector2 touchStart;
    private float timer;

    void Start ()
    {
        children = GetComponentsInChildren<BlockController>();
        gridController = FindObjectOfType<GridController>();
        selected = false;
        timer = 0;
	}

    public void selectAll()
    {
        if(selected)//deselect if already selected
        {
            deselectAll();
            return;
        }

        gridController.deselectAll();
        checkMovement();
        selected = true;

        for(int i=0;i<children.Length;i++)
        {
            children[i].select();
        }
    }
    public void deselectAll()
    {
        selected = false;

        for (int i = 0; i < children.Length; i++)
        {
            children[i].deselect();
        }
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;//timer allows precise movements

        if(selected)
        {
            int x = 0;
            int y = 0;

            if (timer <= 0)
            {
                x = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal"));
                y = Mathf.FloorToInt(Input.GetAxisRaw("Vertical"));

                timer = 0.1f;
            }

            if ((x == -1 && !moveLeft) || (x == 1 && !moveRight)) x = 0;//limits movement 

            if ((y == -1 && !moveDown) || (y == 1 && !moveUp)) y = 0;//limits movement 

            if (x == 0 && y == 0) return;

            move(x, y);
        }

    }

    public void checkMovement()
    {
        moveLeft = true;
        moveRight = true;
        moveUp = true;
        moveDown = true;

        //for each child block check only the immediate surrounding blocks
        for(int i=0;i<children.Length;i++)
        {
            //check left
            gridController.blockLeft(children[i].transform.position, this, ref moveLeft);
            //check right
            gridController.blockRight(children[i].transform.position, this, ref moveRight);
            //check up
            gridController.blockUp(children[i].transform.position, this, ref moveUp);
            //check down
            gridController.blockDown(children[i].transform.position, this, ref moveDown);
        }
    }

    //move the parent by one, using relative positioning 
    public void move(int x, int y)
    { 
        this.transform.position = this.transform.position + new Vector3(x, y, 0);
        //reclacculate at new position
        checkMovement();
        //gridController.checkAllMovement();//not the most efficient way
    }

    public void blockAt(Vector3 pos, ref bool output)
    {
        for(int i=0;i<children.Length;i++)
        {
            if (pos == children[i].transform.position)
            {
                output = false;//opposite
                return;
            }
        }
    }
    
    public void disableChildren()
    {
        deselectAll();

        for(int i=0;i<children.Length;i++)
        {
            children[i].enabled = false;
        }
    }
}
