using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusStair : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Renderer stairRenderer;
    [SerializeField] private TextMeshPro bonusText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Configure(Color stairColor, string bonusString)
    {
        stairRenderer.material.color = stairColor;
        bonusText.text = bonusString;
    }
}
