using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using GameNetcodeStuff;
using UnityEngine.Rendering.HighDefinition;

namespace FreeCammer.Scripts
{
    internal class CammerScript : MonoBehaviour
    {
        public static CammerScript instance;
        public bool inCammer = false;
        public float speed = 10.0f;
        public Camera cammer;
        public AudioListener ears;
        public Light light;
        public PlayerControllerB me;
        public GameObject lightHolder;
        private GameObject myarms;
        private Renderer myhead;
        private LODGroup lodg;
        //private float x;
        //private float y;
        //private float lastFrameX;
        //private float lastFrameY;
        //private float mouseDeltaX;
        //private float mouseDeltaY;
        private Vector3 rotateValue;
        public void Awake()
        {
            MyCammer.mls.LogInfo("Script Active");
            instance = this;    
        }
        public void Update() 
        {
            //lastFrameX = x;
            //lastFrameY = y;
            //x = UnityInput.Current.mousePosition.x;
            //y = UnityInput.Current.mousePosition.y;
            //mouseDeltaX = x - lastFrameX;
            //mouseDeltaY = y - lastFrameY;
            //MyCammer.mls.LogInfo(Mouse.current.delta.value);
            if(me == null)
            {
                foreach(PlayerControllerB p in FindObjectsOfType<PlayerControllerB>())
                {
                    if(p.IsOwner)
                    {
                        me = p;
                        myarms = me.cameraContainerTransform.parent.GetChild(1).gameObject;
                        myhead = GameObject.Find("Systems/Rendering/PlayerHUDHelmetModel/ScavengerHelmet").GetComponent<Renderer>();
                        if (me.GetComponentInChildren<LODGroup>())
                        {
                            lodg = me.GetComponentInChildren<LODGroup>();
                        }
                    }
                }
            }
            if (cammer == null)
            {
                cammer = gameObject.AddComponent<Camera>();
                
                cammer.enabled = false;
                if (gameObject.GetComponent<Camera>() != null)
                {
                    MyCammer.mls.LogInfo("Cammer Added!");
                    cammer.cullingMask = 20649983;
                }
                if(ears == null)
                {
                    MyCammer.mls.LogInfo("ears spawned");
                    ears = gameObject.AddComponent<AudioListener>();
                    ears.enabled = false;
                }
            }
            if (lightHolder == null)
            {
                lightHolder = new GameObject("lightermfer");
                lightHolder.hideFlags = HideFlags.HideAndDontSave;
                DontDestroyOnLoad(lightHolder);
                light = lightHolder.AddComponent<Light>();
                light.type = LightType.Directional;
                lightHolder.AddComponent<HDAdditionalLightData>();
                light.gameObject.GetComponent<HDAdditionalLightData>().intensity = 10f;
                //light.gameObject.GetComponent<HDAdditionalLightData>().EnableShadows(false);
                light.intensity = 50f;
                lightHolder.SetActive(false);
                MyCammer.mls.LogInfo("Light spawned");
            }
            if (UnityInput.Current.GetMouseButton(1) || MyCammer.fpsCam.Value)
            {
                //rotateValue = new Vector3(mouseDeltaY, mouseDeltaX * -1) * 0.1f;
                //rotateValue = new Vector3(y, x * -1) * 0.1f;
                rotateValue = new Vector3(Mouse.current.delta.y.value, Mouse.current.delta.x.value * -1) * 0.1f;
                transform.eulerAngles = transform.eulerAngles - rotateValue;
                
            }
            if (transform.eulerAngles.x + 5 >= 89 && transform.localEulerAngles.x < 270)
            {
                transform.eulerAngles = new Vector3(84, transform.eulerAngles.y, 0);
            }
            if (transform.eulerAngles.x - 5 <= 271 && transform.localEulerAngles.x > 90)
            {
                transform.eulerAngles = new Vector3(276, transform.eulerAngles.y, 0);
            }
            if (light != null && cammer != null)
            {
                light.transform.localEulerAngles = new Vector3(
                    cammer.transform.localEulerAngles.x + 90,
                    cammer.transform.localEulerAngles.y, 0);
            }
            if (UnityInput.Current.GetKeyDown(MyCammer.enterCam.Value))
            {
                if (me != null && !me.isPlayerDead)
                {
                    cammer.transform.position = me.gameplayCamera.transform.position;
                    cammer.transform.rotation = me.gameplayCamera.transform.rotation;
                }
                else if (me != null && me.isPlayerDead)
                {
                    cammer.transform.position = me.playersManager.spectateCamera.transform.position;
                    cammer.transform.rotation = me.playersManager.spectateCamera.transform.rotation;
                }
                cammer.enabled = !cammer.enabled;
                inCammer = !inCammer;

                if (lightHolder != null)
                {
                    lightHolder.SetActive(inCammer);
                }
                ChangeStates();
            }
            if (UnityInput.Current.GetKey(KeyCode.U))
            {
                cammer.transform.position += cammer.transform.forward * speed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.J))
            {
                cammer.transform.position -= cammer.transform.forward * speed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.H))
            {
                cammer.transform.position -= cammer.transform.right * speed * Time.deltaTime;
            }
            if (UnityInput.Current.GetKey(KeyCode.K))
            {
                cammer.transform.position += cammer.transform.right * speed * Time.deltaTime;
            }
        }
        public void ChangeStates()
        {
            me.activeAudioListener.enabled = !inCammer;
            ears.enabled = inCammer;
            //me.inSpecialInteractAnimation = inCammer;
            myarms.SetActive(!inCammer);
            if (!MyCammer.fpsCam.Value)
            {
                Cursor.visible = inCammer;
            }
            myhead.forceRenderingOff = inCammer;
            if (lodg != null)
            {
                lodg.enabled = !inCammer;
            }
            if (inCammer && me != null && !MyCammer.fpsCam.Value)
            {
                Cursor.lockState = CursorLockMode.Confined;

            }
            else if (me != null && !me.quickMenuManager.isMenuOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
