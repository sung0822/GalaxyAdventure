using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    Debug.Log("singleton Manger has been created " + _instance.name);
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Debug.Log("this singleton has existed already" + _instance.name);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("기존 싱글턴 오브젝트 파괴됨");
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        Debug.Log(_instance.name + " 파괴됨");
    }

}