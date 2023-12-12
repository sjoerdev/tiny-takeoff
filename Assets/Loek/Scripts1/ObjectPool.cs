using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;

    private Queue<GameObject> objects = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objects.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objects.Count > 0)
        {
            GameObject obj = objects.Dequeue();
            obj.SetActive(true);
            return obj;
        }

       
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(true);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objects.Enqueue(obj);
    }
}
