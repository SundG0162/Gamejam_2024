using System;
using System.Collections;
using Crogen.CrogenPooling;
using UnityEngine;

namespace BSM.Projectile
{
    public class Bullet : MonoBehaviour, IPoolingObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _moveTime;

        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }

        public void OnPop()
        {   
            StartCoroutine(PushWaitTimeCo(_moveTime));
        }

        public void OnPush()
        {

        }

        void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }

        IEnumerator PushWaitTimeCo(float t)
        {
            yield return new WaitForSeconds(t);
            this.Push();
        }
    }
}
