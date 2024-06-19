using UnityEngine;

namespace FPSGame
{
    //
    // 싱글톤 패턴을 제공하는 부모 스크립트.
    // Singleton(싱글톤): 프로젝트 내에서 1개만 존재하도록 강제하는 스크립트.
    // 주로 편의 목적을 위해 사용함.
    // 전역적으로 어디에서나 쉽게 접근이 가능함.
    // 어떤 데이터나 객체를 관리하는 관리자 기능을 만들 때 많이 활용됨.
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // 전역 인스턴스.
        private static T instance = null;
        
        public static T Instance
        {
            get
            {
                return Get();
            }
        }

        // 접근 메소드.
        public static T Get()
        {
            // 구현 방법 #1.
            // → 처음 접근할 때 게임 오브젝트를 생성한 후에 컴포넌트를 추가해서. 값을 할당하는 방법.
            //if (instance == null)
            //{
            //    GameObject go = new GameObject($"{typeof(T)}");
            //    instance = go.AddComponent<T>();
            //}


            // 구현 방법 #2.
            // 일단 씬에 미리 생성해둠.
            // 씬에 생성되어 있는 컴포넌트를 검색해서 할당하는 방법.
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();

                if (instance == null)
                {
                    // 오류 메세지 출력.
                    Debug.LogError($"씬에서 {typeof(T)}를 가진 게임 오브젝트를 찾지 못했습니다.");
                }
            }

            return instance;
        }
    }
}
