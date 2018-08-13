using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VoiceController : MonoBehaviour
{
    public Text MessageText;
    public Image MessageImage;

    private string[] VoiceLines;

	void Start ()
    {
        VoiceLines = new string[] 
        {
            "Hard Coding for the win",
            "I just wanted to be one of The Strokes",
            "I'm a big name in deep space, ask your mates",
            "Everybody's on a barge",
            "Love came in a bottle with a twist-off cap",
            "It took the light forever to get to your eyes",
            "What do you mean you've never seen Blade Runner?"
        };

        HideLine();

        Invoke("ShowLine", Random.Range(4, 60));
	}
	
    void ShowLine()
    {
        int index = Random.Range(0, VoiceLines.Length);
        string message = VoiceLines[index];

        MessageImage.enabled = true;
        MessageText.text = message;

        Invoke("HideLine", 5);

        Invoke("ShowLine", Random.Range(6, 60));
    }

    void HideLine()
    {
        MessageImage.enabled = false;
        MessageText.text = "";
    }
}
