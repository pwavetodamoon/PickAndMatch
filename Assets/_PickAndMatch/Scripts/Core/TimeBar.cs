using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using deVoid.Utils;
using DG.Tweening;
using PickandMatch.Scripts.UIs;
using PickandMatch.Scripts.Defines;
namespace PickandMatch.Scripts.Core
{
    public sealed class TimeBar : MonoBehaviour
    {
        public Image TimeBarSlide;
        private Tween _tween;
        private float _aspectTime;
        private List<float> _lsTime = new List<float>() { 0.75f, 0.5f, 0.25f };
        [SerializeField] private StarUIController[] _stars;
        private int _numStar;

        public void DisPatchStarCount(UIGamePlayController uIGamePlay)
        {
            _numStar = GetStars();
            uIGamePlay.SendStarEvent(_numStar);
        }
        public void StartTimeLevel(float timeLevel)
        {
            Debug.Log("Time bar start");
            TimeBarSlide.fillAmount = 1;
            AppearStar();
            if (_tween != null)
            {
                Debug.Log(" Kill Time bar Tween");

                _tween.Kill();
            }
            _tween = TimeBarSlide.DOFillAmount(0.0f, timeLevel).SetEase(Ease.Linear).SetId(this).OnUpdate(() =>
            {
                _aspectTime = TimeBarSlide.fillAmount;
                DisappearStar();
            }).OnComplete(() =>
            {
                Signals.Get<StatusGame>().Dispatch(GameStatus.Lose);
            });

        }
        public void TimeToggle(GameStatus status)
        {
            if (status == GameStatus.Win || status == GameStatus.Lose)
            {
                DOTween.Pause(TimeBarSlide);
                return;
            }
            DOTween.Play(TimeBarSlide);
        }

        private void AppearStar()
        {
            for (int index = 0; index < _stars.Length; index++)
            {
                _stars[index].StarShow();
            }
        }
        private void DisappearStar()
        {
            for (int index = 0; index < _stars.Length; index++)
            {
                if (_aspectTime < _lsTime[index])
                {
                    _stars[index].StarHide();
                }
            }
        }
        public int GetStars()
        {
            int starTotal = 3;
            for (int index = 0; index < _lsTime.Count; index++)
            {
                if (_aspectTime > _lsTime[index])
                {
                    break;
                }
                --starTotal;
            }
            return starTotal;
        }
        private void OnDestroy()
        {
            DOTween.Kill(TimeBarSlide);
        }
    }

}
