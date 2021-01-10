using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommandLineInterface;

namespace Assets.Scripts
{
    /*
    /// between a minimum and maximum amount of time a random point is choosen.
    /// the Action which is present in Proc will be executed with the string parameter "on".
    /// After the set duration has passed Proc will be called with string parameter "off".
    /// If continuous is true 
    */

    public class BloodMoon : MonoBehaviour
    {
        public CommandLineInterface Proc;

        public bool continuous;

        public float duration = 20.0f;

        public float minimum = 60.0f;
        public float maximum = 180.0f;

        private float counter = 0.0f;
        private float random = 0.0f;
        private float _duration = 0.0f;

        private void Start()
        {
            _duration = duration;
        }

        void Update()
        {

            counter += Time.deltaTime;

            if (duration > 0 && counter >= random)
            {
                duration -= Time.deltaTime;
            }

            if (duration <= 0 && counter >= random)
            {

                if (Proc != null)
                {
                    Proc.BloodMoon("off");
                }

                counter = 0.0f;
                random = 0.0f;

                if (continuous)
                {
                    duration = _duration;
                }
            }

            if (random == 0.0f)
            {
                random = UnityEngine.Random.Range(minimum, maximum);
            }

            if (counter >= random)
            {
                if (Proc != null)
                {
                    Proc.BloodMoon("on");
                }
            }
        }
    }
}