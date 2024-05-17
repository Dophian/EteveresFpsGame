using UnityEngine;

namespace FPSGame
{

    // Scriptable Object로 제작할 플레이어 데이터.
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Create PlayerData")]
    public class PlayerData : ScriptableObject
    {
        // 데이터로 파일로 저장할 플레이어 데이터.
        public float moveSpeed = 3f;            // 이동 속도.
        public float rotationSpeed = 540f;      // 회전 속도.
        public float maxHP = 100f;              // 체력(Health Power).
        public int maxAmmo = 20;                // 탄창에 채울 수 있는 탄약 수.
    }
}