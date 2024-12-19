using BSM.Entities;
using Crogen.CrogenPooling;
using DG.Tweening;
using UnityEngine;

namespace BSM.Players
{
    public class GhostTrail : MonoBehaviour, IPoolingObject
    {
        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Entity entity)
        {
            _spriteRenderer.sprite = entity.GetEntityComponent<EntityRenderer>().SpriteRenderer.sprite;
            Color color = _spriteRenderer.color;
            color.a = 0.7f;
            _spriteRenderer.color = color;
            _spriteRenderer.DOFade(0, 0.3f).OnComplete(this.Push);
        }

        public void OnPop()
        {
        }

        public void OnPush()
        {
        }
    }
}
