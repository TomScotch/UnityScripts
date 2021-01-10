using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /*
    /// After the set duration has passed RandomActionTrigger is set to member setEnabled.
    /// If continuous RandomActionTrigger returns to its previous state and we start over
    */

    public class CountdownTimer : MonoBehaviour
    {

        public float timeLeft = 30.0f;
        private float _timeLeft;
        public RandomActionTrigger rat;
        private bool continuous = false;
        public bool setEnabled;

        private void Start()
        {
            _timeLeft = timeLeft;
            rat.enabled = !setEnabled;
        }

        void Update()
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }

            if (timeLeft < 0 && timeLeft != 0)
            {
                rat.enabled = setEnabled;

                if (continuous)
                {
                    rat.enabled = !setEnabled;
                    timeLeft = _timeLeft;
                }
                else
                {
                    timeLeft = 0;
                }
            }
        }
    }
}