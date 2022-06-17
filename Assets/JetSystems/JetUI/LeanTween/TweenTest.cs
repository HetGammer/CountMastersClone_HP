using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, Vector3.one * 3, 1).setRepeat(20).setOnComplete(OnComplete).setEase(LeanTweenType.easeInElastic);

        Shneider shneider = new Shneider();

        Debug.Log(new Shneider().number);
        Debug.Log(new Shneider().AddOne().number);
        Debug.Log(new Shneider().AddOne().MultiplyByTwo().number);
    }

    private void OnComplete()
    {
        Debug.Log("Finished");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Shneider
{
    public int number = 0;

    public Shneider AddOne()
    {
        number++;
        return this;
    }

    public Shneider MultiplyByTwo()
    {
        number *= 2;
        return this;
    }
}
