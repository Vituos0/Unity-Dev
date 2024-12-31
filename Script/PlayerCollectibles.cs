using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    private Text textComponent;
    public int GemNumber;

    // Start is called before the first frame update
    void Start()
    {   
        textComponent = GameObject.FindGameObjectWithTag("GemUI").GetComponentInChildren<Text>();
        UpdateText();

    }

    private void UpdateText()
    {
        textComponent.text = GemNumber.ToString();
    }

    public void GemCollecting()
    {
        GemNumber++;
        UpdateText();
    }
}
