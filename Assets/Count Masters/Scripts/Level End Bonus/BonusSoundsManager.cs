using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSoundsManager : MonoBehaviour
{
    [Header(" Sounds ")]
    [SerializeField] private AudioClip[] tones;
    private AudioSource source;

    private void Awake() 
    {
        BonusRunnersParent.OnLineDropped += PlayToneSound;    
    }

    private void OnDestroy() 
    {
        BonusRunnersParent.OnLineDropped -= PlayToneSound;    
    }

    // Start is called before the first frame update
    void Start()
    {
        if(source == null)
            source = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayToneSound(int toneIndex)
    {
        toneIndex = Mathf.Min(toneIndex, tones.Length - 1);
        source.clip = tones[toneIndex];
        source.Play();
    }
}
