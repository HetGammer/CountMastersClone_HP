using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JetSystems
{
    public class Pomodoro : MonoBehaviour
    {
        public static Pomodoro instance;

        List<PomodoroStruct> pomodoros = new List<PomodoroStruct>();
        List<PomodoroStruct> pomodorosToRemove = new List<PomodoroStruct>();

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        void Update()
        {
            CheckAllTimers();
        }

        public IEnumerator StartNewTimer(float timer, Action onTimerComplete)
        {
            yield return new WaitForEndOfFrame();
            
            pomodoros.Add(new PomodoroStruct(Time.time + timer, onTimerComplete));
        }

        private void CheckAllTimers()
        {
            foreach (PomodoroStruct pomodoro in pomodoros)
                if (Time.time > pomodoro.GetTimer())
                {
                    pomodoro.GetOnTimerCompleteAction()?.Invoke();
                    pomodorosToRemove.Add(pomodoro);
                }

            RemoveElapsedTimers();
        }

        private void RemoveElapsedTimers()
        {
            for (int i = 0; i < pomodorosToRemove.Count; i++)
                pomodoros.Remove(pomodorosToRemove[i]);
        }

        public static void AddTimer(float timer, Action onTimerComplete)
        {
            instance.StartCoroutine(instance.StartNewTimer(timer, onTimerComplete));
        }
    }







    public struct PomodoroStruct
    {
        float timer;
        Action onTimerComplete;

        public PomodoroStruct(float timer, Action onTimerComplete)
        {
            this.timer = timer;
            this.onTimerComplete = onTimerComplete;
        }

        public float GetTimer()
        {
            return timer;
        }

        public Action GetOnTimerCompleteAction()
        {
            return onTimerComplete;
        }
    }
}