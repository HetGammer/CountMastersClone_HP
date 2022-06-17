using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class RunnerSkinManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform skinsParent;
    [SerializeField] private Renderer[] skinsRenderers;
    private int currentSkinIndex;

    private void Awake()
    {
        LoadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSkin(currentSkinIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartRunning()
    {
        skinsParent.GetChild(currentSkinIndex).GetComponent<Animator>().SetInteger("State", 1);
    }

    public void StopRunning()
    {
        skinsParent.GetChild(currentSkinIndex).GetComponent<Animator>().SetInteger("State", 0);
    }

    public void SetSkin(int skinIndex)
    {
        currentSkinIndex = skinIndex;

        for (int i = 0; i < skinsParent.childCount; i++)
            skinsParent.GetChild(i).gameObject.SetActive(i == skinIndex);

        SaveData();
    }

    public void DisableRenderer()
    {
        foreach (Renderer renderer in skinsRenderers)
            renderer.enabled = false;        
    }

    public Color GetColor()
    {
        return skinsRenderers[0].material.GetColor("_BaseColor");
    }

    public int GetSkinIndex()
    {
        return currentSkinIndex;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("LastSkin", currentSkinIndex);
    }

    private void LoadData()
    {
        currentSkinIndex = PlayerPrefs.GetInt("LastSkin");
    }
}
