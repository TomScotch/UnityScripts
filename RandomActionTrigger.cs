using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /*
    /// between a minimum and maximum amount of time a random point is choosen.
    /// the Action which is present in Proc will be executed with the string parameter "on".
    /// After the set duration has passed Proc will be called with string parameter "off".
    */
    public class RandomActionTrigger : MonoBehaviour
    {
        public Action<string> Proc1;
        public Action<string> Proc2;

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
            if (duration > 0 && counter >= random)
            {
                duration -= Time.deltaTime;
            }
           
            if (duration <= 0 && counter >= random)
            {
                Proc1("off");
                Proc2("off");
                counter = 0.0f;
                random = 0.0f;
                duration = _duration;
            }

            if (random == 0.0f)
            {
                random = UnityEngine.Random.Range(minimum, maximum);
            }

            if (counter >= random)
            {
                Proc1("on");
                Proc2("on");
            }

            counter += Time.deltaTime;
        }
    }
}