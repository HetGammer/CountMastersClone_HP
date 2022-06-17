using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationManager : MonoBehaviour
{
    [Header(" Elements ")]
    public Image image;
    public Color onColor;
    public Color offColor;

    [Header(" Settings ")]
    private static bool canVibrate;

    private void Awake()
    {
        canVibrate = PlayerPrefs.GetInt("Vibration") == 0 ? true : false;

        if (canVibrate)
            TurnOnVibration();
        else
            TurnOffVibration();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchVibrationState()
    {
        canVibrate = !canVibrate;

        if (canVibrate)
            TurnOnVibration();
        else
            TurnOffVibration();

        SaveState();
    }

    void SaveState()
    {
        PlayerPrefs.SetInt("Vibration", canVibrate ? 0 : 1);
    }

    private void TurnOnVibration()
    {
        StopAllCoroutines();
        StartCoroutine("SwitchStateCoroutine");
    }

    private void TurnOffVibration()
    {
        StopAllCoroutines();
        StartCoroutine("SwitchStateCoroutine");
    }

    IEnumerator SwitchStateCoroutine()
    {
        float t = 0;
        float duration = 0.3f;

        while (t < duration + Time.deltaTime)
        {
            if (canVibrate)
                image.color = Color.Lerp(offColor, onColor, t / duration);
            else
                image.color = Color.Lerp(onColor, offColor, t / duration);

            t += Time.deltaTime;
            
            yield return null;
        }
    }

    public static bool CanVibrate()
    {
        return canVibrate;
    }
}
