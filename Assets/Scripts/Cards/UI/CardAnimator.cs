using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using static SoliterGame.Cards.CardModel;

public class CardAnimator : MonoBehaviour
{
    [Serializable]
    public class CardPacksPos
    {
        public int PosIndex;
        public RectTransform Transform;
    }

    [SerializeField] private List<CardPacksPos> _cardPacksPos;
    [SerializeField] private RectTransform _cardComboPos;
    [SerializeField] private SimpleCardView _cardViewPrefab;
    [SerializeField] private float _animDuration = 0.3f;

    private void Start()
    {
        Game.Card.OnStartAnimation += StartAnim;
    }

    private void OnDestroy()
    {
        if (Game.Card != null)
        {
            Game.Card.OnStartAnimation -= StartAnim;
        }
    }

    private void StartAnim(int pos, CardView card)
    {
        var animCard = Instantiate(_cardViewPrefab, _cardPacksPos.Find(x => x.PosIndex == pos).Transform.position, Quaternion.identity, this.transform);
        void OnCompleteAnimation()
        {
            Game.Card.UnpauseLevel();
            Game.Card.UpdateCurrentComboCard(card);
            animCard.DOKill();
            Destroy(animCard.gameObject);
        }
        Game.Card.PauseLevel();
        animCard.InitView(pos, card);
        animCard.GetComponent<RectTransform>().DOAnchorPos(_cardComboPos.anchoredPosition, _animDuration).OnComplete(OnCompleteAnimation);
    }
}
