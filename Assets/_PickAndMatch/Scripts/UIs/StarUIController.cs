using UnityEngine;
using DG.Tweening;
using PickandMatch.Scripts.Defines;

namespace PickandMatch.Scripts.UIs
{
    public sealed class StarUIController : MonoBehaviour
    {
        public void StarHide()
        {
            transform.DOScale(0, TimeConfig.TimeDuration).SetEase(Ease.Linear).SetId(this);           
        }

        public void StarShow()
        {
            transform.localScale = Vector3.one;
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }

}
