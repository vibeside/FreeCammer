using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FreeCammer
{
    internal class Freecam : MonoBehaviour
    {
        private static readonly Freecam instance = new();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Freecam()
        {
        }

        private Freecam()
        {
        }

        public static Freecam Instance
        {
            get
            {
                return instance;
            }
        }
        float cameraSpeed = 10f;

        Camera camera;

        void Awake()
        {
            camera = gameObject.AddComponent<Camera>();
        }
        void FixedUpdate()
        {
            // do stuff
        }
    }
}
