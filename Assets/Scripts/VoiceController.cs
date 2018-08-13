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
            "What do you mean you've never seen Blade Runner?",
            "Bear with me, man, I lost my train of thought",
            "This stunning documentary that no one else unfortunately saw",
            "Such beautiful photography, it's worth it for the opening scene",
            "I've played to quiet rooms like this before",
            "Jesus in the day spa, filling out the information form",
            "Technological advances really bloody get me in the mood",
            "Take it easy for a little while",
            "I put a taqueria on the roof",
            "I'm in no position to give advice, I don't want to be nice",
            "Religious iconography giving you the creeps?",
            "I must admit you gave me something momentarily",
            "Mass panic on a not too distant future colony",
            "I want to make a simple point about peace and love",
            "I can recall the glow of your low beams",
            "Have I told you all about the time that I got sucked into a hole",
            "Panoramic windows looking out across your soul",
            "I'll be in a nose dive in my flying shoes",
            "Killer Pink Flamingos, computer controlled",
            "I might look as if I'm deep in thought"
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
