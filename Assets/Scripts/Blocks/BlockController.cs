using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public BlockParentController parent;
 
    private Renderer blockRenderer;

	void Start ()
    {
        parent = GetComponentInParent<BlockParentController>();
        blockRenderer = GetComponent<Renderer>();
        blockRenderer.enabled = false;
    }
	
	void Update ()
    {
        //detects mouse clicks on the block
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    parent.selectAll();
                }
            }
        }
      
    }

    public void select()
    {
        blockRenderer.enabled = true;
    }
    public void deselect()
    {
        blockRenderer.enabled = false;
    }
}
