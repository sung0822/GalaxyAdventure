using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public string poolName { get { return _poolName; } set { _poolName = value; } }
    [SerializeField] string _poolName;
    public Queue<GameObject> objects { get { return _objects; } set { _objects = value; } }
    public Queue<GameObject> _objects;

    private void Awake()
    {
        objects = new Queue<GameObject>();
    }

    public void AddObject(GameObject gameObject)
    {
        gameObject.SetActive(false); // ��Ȱ��ȭ�Ͽ� �ٽ� ����� �� �ֵ��� ��
        objects.Enqueue(gameObject);
    }

    public GameObject GetObject()
    {
        if (objects.Count > 0)
        {
            GameObject obj = objects.Dequeue();
            obj.SetActive(true); // Ȱ��ȭ�Ͽ� ��� ���� ���·� ����
            return obj;
        }
        else
        {
            return null;
        }

    }
    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false); // ��Ȱ��ȭ�Ͽ� �ٽ� ����� �� �ֵ��� ��
        objects.Enqueue(gameObject);
    }

}
