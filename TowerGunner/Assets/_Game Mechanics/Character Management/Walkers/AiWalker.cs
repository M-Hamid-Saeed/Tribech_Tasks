using AxisGames.Pooler;
using SWS;
using System.Collections.Generic;
using UnityEngine;
namespace Character_Management
{

    public class AiWalker : MonoBehaviour,IPooled<AiWalker>
    {
        [Header("------ Refrences ------")]
        [SerializeField] splineMove sMove;
        [SerializeField] GameObject pickupObject;
        

        [Space]
        [Header("------ Refrences ------")]
        [SerializeField] int coinFactor = 1;
       
        float    normalSpeed;
        float    currentSpeed;
        Animator _animator;

        public int poolID { get; set; }
        public ObjectPooler<AiWalker> pool { get; set; }

        public void Initialize(PathManager path, splineMove.LoopType loopType, float speed,bool startWalking = false)
        {
            _animator = GetComponentInChildren<Animator>();

            sMove.pathContainer = path;
            sMove.loopType = loopType;

            normalSpeed = speed;
            currentSpeed = normalSpeed;

            sMove.ChangeSpeed(currentSpeed);

            sMove.SetAction(WalkStartEvent,WalkEndEvent);



            if (startWalking) { GameController_onHome(); }
        }

        private void WalkerSpeedMultiplier_onSpeedNormalized(float obj)
        {
            currentSpeed = normalSpeed;

            sMove.ChangeSpeed(currentSpeed);
        }

        private void WalkerSpeedMultiplier_onSpeedMultiplied(float multiplier)
        {
            currentSpeed = normalSpeed * multiplier;
            sMove.ChangeSpeed(currentSpeed);
        }

        private void GameController_onMergeArea()
        {
            EnablePickup(false);
            UpdateAnimator();
            sMove.Stop();
            sMove.ResetToStart();
        }
        public void OnGetDamage(bool MovingState)
        {
            
            UpdateAnimator(!MovingState);
           
            
        }
        private void GameController_onHome()
        {
            UpdateAnimator(walk: true);
            sMove.StartMove();
        }

        public void UpdateAnimator(bool walk = false)
        {
            if (walk) { _animator.SetTrigger("Walk"); }
            else { _animator.SetTrigger("Idle"); }
        }

        private void WalkStartEvent()
        {
            EnablePickup(true);
        }

        private void WalkEndEvent()
        {
            EnablePickup(false);
            //PopupManager.Instance.PopMessage(PopupType.Cash, coinFactor, transform, worldPosition: true);
           // CoinsManager.Instance.CollectCoin(coinFactor,saveCurrentCoins:false);
        }

        private void EnablePickup(bool enabled = false)
        {
          //  pickupObject?.SetActive(enabled);
        }

        public void FreePool()
        {
            if (ReferenceManager.Instance.offscreenIndicator.enemiesList.Contains(this.transform.GetChild(0).gameObject))
            {
                ReferenceManager.Instance.offscreenIndicator.enemiesList.Remove(this.transform.GetChild(0).gameObject);
                ReferenceManager.Instance.offscreenIndicator.shouldIndicateNext = true;
            }
            this.GetComponent<splineMove>().Pause();
            pool.Free(this);

        }
    }
}