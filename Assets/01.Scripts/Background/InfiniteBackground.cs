using System.Collections.Generic;
using UnityEngine;

// 4방향에 콜라이더를 놓고 해당 콜라이더와 충돌 처리가 나면 그 위치 차이만큼 이동하는 골드메탈식 무한맵 방식을 구현했음.

namespace SSH
{
    public class InfiniteBackground : MonoBehaviour
    {

        public void MovePosition(Vector3 pos)
        {
            transform.position += pos;
        }
        
    }
}
