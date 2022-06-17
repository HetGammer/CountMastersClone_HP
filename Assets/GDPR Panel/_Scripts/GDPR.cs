using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GDPR : MonoBehaviour
{
    [Header(" Settings ")]
    public string gameName;
    public Text titleText;
    public Text consentText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("GDPR") == 1)
            Destroy(gameObject);
        else
            GetComponent<Canvas>().enabled = true;

        UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel()
    {
        PlayerPrefs.SetInt("GDPR", 1);
        Destroy(gameObject);
    }

    public void UpdateTexts()
    {
        Debug.Log(gameName);

        if(titleText != null)
            titleText.text = gameName + " GDPR";

        if (consentText == null) return;

        consentText.text = "I hereby consent to " + gameName + "'s processing of my personal data to personalize and improve the game and serving targeted advertisements in the game through advertising networks and their partners based on my personal preferences."
            + "\n" + "\n" + "\n" +
            "By clicking the OK button, I confirm that I have read and agreed with "+ gameName +"'s Privacy Policy and I confirm that my age is greater than 13.";
    }
}
