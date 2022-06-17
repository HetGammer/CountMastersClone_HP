using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JetSystems
{

    public class SoundsButton : MonoBehaviour
    {
        [Header(" Settings ")]
        [SerializeField] private Audio_Manager audioManager;
        public Transform soundsParent;
        public static bool state;

        [Header(" UI ")]
        public Image image;
        public Sprite soundOnSprite;
        public Sprite soundOffSprite;
        public Color onColor;
        public Color offColor;

        private void Awake()
        {
            state = PlayerPrefsManager.GetSoundState() == 0 ? true : false;

            if (state)
                TurnSoundOn();
            else
                TurnSoundOff();
        }

        public void SwitchSoundState()
        {
            if (state)
                TurnSoundOff();
            else
                TurnSoundOn();

            SaveState();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void TurnSoundOn()
        {
            SetSounds(1);
            state = true;

            StopAllCoroutines();
            StartCoroutine("SwitchStateCoroutine");
            //image.sprite = soundOnSprite;

            audioManager.soundMute = false;

        }

        void TurnSoundOff()
        {
            SetSounds(0);
            state = false;

            //image.sprite = soundOffSprite;
            StopAllCoroutines();
            StartCoroutine("SwitchStateCoroutine");

            audioManager.soundMute = true;
        }



        void SaveState()
        {
            PlayerPrefsManager.SetSoundState(state ? 0 : 1);
        }

        void SetSounds(int volume)
        {
            if (soundsParent == null) return;

            for (int i = 0; i < soundsParent.childCount; i++)
            {
                soundsParent.GetChild(i).GetComponent<AudioSource>().volume = volume;
            }
        }

        IEnumerator SwitchStateCoroutine()
        {

            float t = 0;
            float duration = 0.3f;
            while (t < duration + Time.deltaTime)
            {
                if (state)
                {
                    image.color = Color.Lerp(offColor, onColor, t / duration);
                }
                else
                {
                    image.color = Color.Lerp(onColor, offColor, t / duration);
                }

                t += Time.deltaTime;
                yield return null;
            }
        }

        public static bool IsSoundOn()
        {
            return state;
        }
    }
}