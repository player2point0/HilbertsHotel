using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    public Text CostText;
    public Image CostImage;
    public int startCost;
    public int CurrentCost;
    public GameObject[] Skins;

	void Start ()
    {
        CurrentCost = startCost + Random.Range(0, 60);

        CostText.text = "";
        CostImage.enabled = false;

        SelectRandomSkin();
	}

    public void StartPriceDecrease()
    {
        CostImage.enabled = true;
        InvokeRepeating("DecreasePrice", 0, 1);
    }

    private void DecreasePrice()
    {
        CurrentCost--;
        CostText.text = CurrentCost.ToString();

        if(CurrentCost < 0)
        {
            CostImage.color = Color.red;
        }

    }

    void SelectRandomSkin()
    {
        int index = Random.Range(0, Skins.Length);

        Skins[index].SetActive(true);
    }

}
