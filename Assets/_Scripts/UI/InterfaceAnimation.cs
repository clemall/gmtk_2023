using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.UI
{
    public class InterfaceAnimation : MonoBehaviour
    {
        private Vector2 _defaultPosition;
        private RectTransform _rt;
        [SerializeField]
        private RectTransform _rt_parent;

        void Awake()
        {
            _rt = GetComponent<RectTransform>();
            _defaultPosition = new Vector2(0, _rt_parent.rect.height*0.5f + _rt.rect.height*0.5f);
            _rt.anchoredPosition = _defaultPosition;
        }
        void OnEnable()
        {
            _rt.DOAnchorPosY(0, 0.5f, true).SetEase(Ease.InOutQuad);
        }

        void OnDisable()
        {
            _rt.anchoredPosition = _defaultPosition;
        }
    }
}