using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IntroVoiceController : MonoBehaviour
{
    public Text MessageText;
    public Image MessageImage;

    private string[] VoiceLines;
    private int index;
    private SceneController sceneController;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        index = 0;
        VoiceLines = new string[]
        {
            "Welcome to the Hilbert",
            "located in the sea of tranquility",
            "it is the moon's only infinte hotel.",
            "So how do we do it?",
            "Magic?",
            "Maths?",
            "Meth?",
            "It's really a combination of the three.",
            "A customer comes in and we chuck another out",
            "fling the room out of the hotel into space",
            "sorta like filling a bucket wit a hole in it",
            "And to keep the space dosh flowing?",
            "Kick them out before they realise and sue.",
            "And we gotta pay for the room, which is about 10 space doshes.",
            "I've made a tool to estimate how long to keep a customer",
            "It does this by estimating their inteligence",
            "If their shoelaces aren't tied, keep them for a while",
            "If they are tied, don't keep them for too long",
            "And if they aren't wearing shoes,",
            "then they must know something the rest of us don't",
            "and just kick them out asap",
            "try not run out of space dosh",
            "or we won't be able to pay the gravity tax",
            "and we'll just float out into space.",
            "And the delivery bay has to be empty, before you kick out someone"
        };

        HideLine();

        Invoke("ShowLine", 1);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CancelInvoke("ShowLine");
            CancelInvoke("HideLine");

            ShowLine();
        }
    }

    void ShowLine()
    {
        if (index >= VoiceLines.Length) return;

        string message = VoiceLines[index];
        index++;

        MessageImage.enabled = true;
        MessageText.text = message;

        if (index >= VoiceLines.Length)
        {
            sceneController.Invoke("LoadGame", 4);
        }

        else
        {
            Invoke("HideLine", 3);
            Invoke("ShowLine", 4);
        }

    }

    void HideLine()
    {
        MessageImage.enabled = false;
        MessageText.text = "";
    }
}
