using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public delegate void TimerDelegate();

    private List<Timer> timers;


    void Start()
    {
        timers = new List<Timer>();
        AddTimerDelegates(() => { Debug.Log("Logging after 5"); }, 5f, 5);
        AddTimerDelegates(() => { Debug.Log("Jumping after 10"); }, 10f, 7);
    }

    void AddTimerDelegates(TimerDelegate toInvoke, float time, int repeat)
    {
        timers.Add(new Timer(toInvoke, time, repeat));
    }

    void Update()
    {
        RefreshTimers();
    }

    void RefreshTimers()
    {
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            if (timers[i].time <= Time.time)
            {
                for (int j = 0; j < timers[i].repeat; j++)
                {
                    timers[i].timerDelegate.Invoke();
                }

                timers.RemoveAt(i);
            }
        }
    }

    public class Timer
    {
        public float time;
        public TimerDelegate timerDelegate;
        public int repeat;

        public Timer(TimerDelegate timerDelegate, float time, int repeat)
        {
            this.time = time;
            this.timerDelegate = timerDelegate;
            this.repeat = repeat;
        }
    }
}

public static class Extensions
{
    public static string ToString<T>(this IEnumerable<T> enumerable)
    {
        string result = "[";
        foreach (var value in enumerable)
        {
            result += value.ToString();
        }

        result += "]";
        return result;
    }

    public static int Factorial(this int i)
    {
        if (i == 1 || i == 0) return 1;
        else return i * Factorial(i - 1);
    }

    public static List<T> ToList<T>(this T[] target)
    {    
        List<T> result = new List<T>();
        foreach (var item in target)
            result.Add(item);
        return result;
    }
}