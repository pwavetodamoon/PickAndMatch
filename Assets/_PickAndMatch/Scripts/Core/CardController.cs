using UnityEngine;
using DG.Tweening;
using PickandMatch.Scripts.Defines;
using deVoid.Utils;
using System.Collections;
using System;

namespace PickandMatch.Scripts.Core
{
    public sealed class CardController : MonoBehaviour
    {
        private CardStatus _statusCard = CardStatus.Ready;
        public CardType CardType;
        private float _angleOpenRotate = 180;
        private float _angleCloseRotate = 0;
        [SerializeField] private SpriteRenderer _renderCardChild;
        private static float _lastClick;
        private float _timeSpeed = 0.5f;
        private GameStatus _statusGame = GameStatus.Play;
        private void Awake ()
        {
            Signals.Get<StatusGame>().AddListener(ChangeStatus);
        }
        private void ChangeStatus(GameStatus status)
        {
            _statusGame = status;
        }
        private void OnMouseUpAsButton()
        {
            if (_statusGame == GameStatus.Play && TimeController.IsClickTime(TimeConfig.TimeDuration, ref _lastClick) == true)
            {
                DoSelectCard();
            }
        }        
        public void DoSelectCard()
        {
            if (IsReady(_statusCard))
            {
                ChangeStatusCard(CardStatus.NotReady);
                DOVirtual.DelayedCall(TimeConfig.TimeDuration * _timeSpeed, () =>
                {
                    ChangeSpriteCard();
                }).SetId(this);
                Signals.Get<SelectCard>().Dispatch(this);
            }
        }

        public void BackDefaultCard()
        {
            ChangeStatusCard(CardStatus.Ready);
            DOVirtual.DelayedCall(TimeConfig.TimeDuration * _timeSpeed, () =>
            {
                _renderCardChild.sprite = CacheManager.Instance.GetCardValue(CardType.unknow);
            }).SetId(this);
            transform.DORotate(new Vector3(0, _angleCloseRotate, 0), TimeConfig.TimeDuration, RotateMode.FastBeyond360).SetId(this).SetEase(Ease.Linear);
        }
        public IEnumerator WaitCardFlip(Action callback, bool finalClick)
        {
            Tween cardFlip =
                transform.DORotate(new Vector3(0, _angleOpenRotate, 0), TimeConfig.TimeDuration, RotateMode.FastBeyond360).SetId(this).SetEase(Ease.Linear);
            yield return cardFlip.WaitForCompletion();
            if (finalClick)
                callback?.Invoke();
        }
        private void ChangeSpriteCard()
        {
            Sprite sprite = CacheManager.Instance.GetCardValue(CardType);

            if (sprite != null)
            {
                _renderCardChild.sprite = sprite;
            }
        }
        public void SetUp(CardType cardType)
        {
            CardType = cardType;
        }
        public bool IsReady(CardStatus status)
        {
            return status == CardStatus.Ready;
        }
        private void ChangeStatusCard(CardStatus newStatus)
        {
            _statusCard = newStatus;
        }
        public void Kill()
        {
            transform.DOScale(0, TimeConfig.TimeDuration).OnComplete(() =>
            {
                DOTween.Kill(this);
                Destroy(gameObject);
            }).SetEase(Ease.Linear).SetId(this);
        }
        private void OnDestroy()
        {
            DOTween.Kill(this);
            Signals.Get<StatusGame>().RemoveListener(ChangeStatus);

        }

    }

}
