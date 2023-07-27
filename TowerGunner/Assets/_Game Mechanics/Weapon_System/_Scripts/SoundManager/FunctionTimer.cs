using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeSystem
{
    public class FunctionTimer
    {
        private static List<FunctionTimer> activeTimerList;
        private static GameObject initGameObject;

        private static void InitObjNeeded()
        {
            if (initGameObject == null)
            {
                initGameObject = new GameObject("FunctionTimer_InitGameObject");
                activeTimerList = new List<FunctionTimer>();
            }
        }


        public static FunctionTimer Create(Action action, float timer, string timerName = null)
        {
            InitObjNeeded();
            GameObject gameObject = new GameObject("TimerObject", typeof(MonoBehaviousHook));
            FunctionTimer functionTimer = new FunctionTimer(action, timer, timerName, gameObject);
            gameObject.GetComponent<MonoBehaviousHook>().onUpdate = functionTimer.Update;
            activeTimerList.Add(functionTimer);
            return functionTimer;
        }

        public static FunctionTimer Create(Action action, UnityEngine.UI.Image image, float timer, string timerName = null, bool antiClock = false, bool hold = false)
        {
            InitObjNeeded();
            GameObject gameObject = new GameObject("TimerObject", typeof(MonoBehaviousHook));
            FunctionTimer functionTimer = new FunctionTimer(action, image, timer, timerName, gameObject, antiClock, hold);
            //if (hold)
            //{
            //    gameObject.GetComponent<MonoBehaviousHook>().onUpdate = functionTimer.UpdateImageOnHold;

            //}
            //else
            //{

            gameObject.GetComponent<MonoBehaviousHook>().onUpdate = functionTimer.UpdateImage;
            //}
            activeTimerList.Add(functionTimer);
            return functionTimer;
        }

        private static void RemoveTmer(FunctionTimer function)
        {
            InitObjNeeded();
            activeTimerList.Remove(function);
        }

        public static void StopeTimer(string timerName)
        {
            for (int i = 0; i < activeTimerList.Count; i++)
            {
                if (activeTimerList[i].timerName == timerName)
                {
                    //Cancle the Timer
                    activeTimerList[i].DestroySelf();
                    i--;
                }
            }
        }

        // This class is using monobehavious because we need it to run code in Update
        public class MonoBehaviousHook : MonoBehaviour
        {
            public Action onUpdate;

            private void Update()
            {
                if (onUpdate != null)
                    onUpdate();
            }
        }

        private Action action;
        private UnityEngine.UI.Image image;
        private float timer;
        private float totalTime;
        private string timerName;
        private GameObject obj;
        private bool antiClock;
        private bool isDestroySelf = false;
        private bool hold;

        private FunctionTimer(Action action, float timer, string timerName, GameObject obj)
        {
            this.action = action;
            this.timer = timer;
            this.obj = obj;
            this.timerName = timerName;
            isDestroySelf = false;

        }

        private FunctionTimer(Action action, UnityEngine.UI.Image image, float timer, string timerName, GameObject obj, bool antiClock, bool hold)
        {
            this.action = action;
            this.image = image;
            if (antiClock)
            {
                image.fillAmount = 1;

            }
            else
            {

                image.fillAmount = 0;
            }
            this.timer = timer;
            this.totalTime = timer;
            this.obj = obj;
            this.timerName = timerName;
            this.antiClock = antiClock;
            this.hold = hold;
            isDestroySelf = false;
        }


        public void Update()
        {
            if (!isDestroySelf)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    action();
                    DestroySelf();
                }
            }
        }
        public void UpdateImage()
        {
            if (!isDestroySelf)
            {


                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    action();
                    image.fillAmount = 1;
                    DestroySelf();
                }
                else
                {
                    //image.fillAmount = timer / totalTime;
                    if (antiClock)
                    {
                        image.fillAmount -= Time.deltaTime / totalTime;
                    }
                    else
                    {
                        image.fillAmount += Time.deltaTime / totalTime;
                    }

                }

            }
        }
        //public void UpdateImageOnHold()
        //{
        //    if (!isDestroySelf)
        //    {
        //        if (Input.GetMouseButton(0) && ReferenceManager.instance.bounceCash.isHoldingButton)
        //        {

        //            timer -= Time.deltaTime;
        //            if (timer < 0)
        //            {
        //                action();
        //                image.fillAmount = 1;
        //                DestroySelf();
        //            }
        //            else
        //            {
        //                //image.fillAmount = timer / totalTime;
        //                if (antiClock)
        //                {
        //                    image.fillAmount -= Time.deltaTime / totalTime;
        //                }
        //                else
        //                {
        //                    image.fillAmount += Time.deltaTime / totalTime;
        //                }

        //            }
        //        }
        //    }
        //}

        private void DestroySelf()
        {
            isDestroySelf = true;
            UnityEngine.Object.Destroy(obj);
            RemoveTmer(this);
        }
    }
}
