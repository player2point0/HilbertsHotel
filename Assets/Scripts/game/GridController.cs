using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public BlockParentController[] blockParents;
    public GameObject[] walls;

    private bool leftTrigger;
    private bool rightTrigger;

	void Start ()
    {
        blockParents = FindObjectsOfType<BlockParentController>();
        walls = GameObject.FindGameObjectsWithTag("Wall");
        checkAllMovement();
        leftTrigger = false;
        rightTrigger = false;
        
	}

    public void deselectAll()
    {
        for(int i=0;i<blockParents.Length;i++)
        {
            blockParents[i].deselectAll();
        }
    }

    private void Update()
    {
        //controller support
        if (Input.GetAxis("RightTrigger") == 1) rightTrigger = true;

        else if (Input.GetAxis("LeftTrigger") == 1) leftTrigger = true;
        
        if (Input.GetAxis("RightTrigger") == 0 && Input.GetAxis("LeftTrigger") == 0)
        {
            if(leftTrigger)
            {
                selectPrevious();
                leftTrigger = false;
            }

            else if(rightTrigger)
            {
                selectNext();
                rightTrigger = false;
            }
        }
    }

    public void selectPrevious()
    {
        for (int i = 0; i < blockParents.Length; i++)
        {
            //deselect current block 
            if (blockParents[i].selected)
            {
                blockParents[i].deselectAll();

                //select previous block in list
                if (i - 1 >= 0)
                {
                    blockParents[i - 1].selectAll();
                    return;
                }
                else if (i - 1 <= -1)
                {
                    blockParents[blockParents.Length - 1].selectAll();
                    return;
                }
            }
        }

        blockParents[0].selectAll();
    }
    public void selectNext()
    {
        for(int i=0;i<blockParents.Length;i++)
        {
            //deselect current block 
            if (blockParents[i].selected)
            {
                blockParents[i].deselectAll();

                //select next block in list
                if (i + 1 < blockParents.Length)
                {
                    blockParents[i + 1].selectAll();
                    return;
                }
                else if (i - 1 >= 0)
                {
                    blockParents[0].selectAll();
                    return;
                }
            }
        }

        blockParents[0].selectAll();
    }

    //loop through parents and search for position in children
    public void blockLeft(Vector2 pos, BlockParentController currentParent, ref bool left)
    {
        for (int i = 0; i < blockParents.Length; i++)
        {
            Vector2 loc = pos + new Vector2(-1, 0);

            wallAt(loc, ref left);

            if (blockParents[i] == currentParent) continue;

            blockParents[i].blockAt(loc, ref left);
        }
    }
    public void blockRight(Vector2 pos, BlockParentController currentParent, ref bool right)
    {

        for (int i = 0; i < blockParents.Length; i++)
        {
            Vector2 loc = pos + new Vector2(1, 0);

            wallAt(loc, ref right);

            if (blockParents[i] == currentParent) continue;

            blockParents[i].blockAt(loc, ref right);
        }
    }
    public void blockUp(Vector2 pos, BlockParentController currentParent, ref bool up)
    {
        for (int i = 0; i < blockParents.Length; i++)
        {
            Vector2 loc = pos + new Vector2(0, 1);

            wallAt(loc, ref up);

            if (blockParents[i] == currentParent) continue;

            blockParents[i].blockAt(loc, ref up);
        }
    }
    public void blockDown(Vector2 pos, BlockParentController currentParent, ref bool down)
    {
        for (int i = 0; i < blockParents.Length; i++)
        {
            Vector2 loc = pos + new Vector2(0, -1);

            wallAt(loc, ref down);

            if (blockParents[i] == currentParent) continue;

            blockParents[i].blockAt(loc, ref down);
        }
    }

    public void wallAt(Vector3 pos, ref bool result)
    {
        for(int i=0;i<walls.Length;i++)
        {
            Vector3 wall = walls[i].transform.position;
            float height = walls[i].GetComponent<BoxCollider>().size.y / 2;//walls[i].transform.localScale.y/2;
            float width = walls[i].GetComponent<BoxCollider>().size.x / 2;//walls[i].transform.localScale.x/2;

            //take into account height
            if ((pos.x < wall.x + width) && (pos.x > wall.x - width))
            {
                //take into account width
                if ((pos.y < wall.y + height) && (pos.y > wall.y - height)) result = false;

            }
        }
    }

    public void checkAllMovement()
    {
        for(int i=0;i<blockParents.Length;i++)
        {
            blockParents[i].checkMovement();
        }
    }

    public void disableBlocks()
    {
        for(int i=0;i<blockParents.Length;i++)
        {
            blockParents[i].disableChildren();
        }
    }
}
