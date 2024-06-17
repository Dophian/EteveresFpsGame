using UnityEngine;

namespace FPSGame
{
    // 탄약의 대미지 정보를 가지는 스크립트.
    public class BulletDamage : MonoBehaviour
    {
        // 변수 (필드). (필드는 감춘다.)
        [SerializeField] private float damage = 30f;

        // 소통은 메시지(공개 메소드)를 통해서.
        public float Damage
        {
            get
            {
                return damage;
            }
        }
    }
}