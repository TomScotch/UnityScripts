using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /*
    /// After the set duration has passed Proc will be executed.
    */
    public class CountdownTimer : MonoBehaviour
    {
        public float timeLeft = 3.0f;
        public Action Proc;

        void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Proc();
            }
        }
    }
}