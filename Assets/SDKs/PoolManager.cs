using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class ObjectToPool
{
    public GameObject pooledObj;
    public int amountToPool;
    public bool canGrow = true; 
}

[System.Serializable]
public class PoolManager : MonoSingleton<PoolManager>
{
    public List<ObjectToPool> itemsToPool;
    public List<GameObject> pooledObjects;


    public override void Init()
    {
        pooledObjects = new List<GameObject>();
        foreach(ObjectToPool item in itemsToPool)            
        for (int i = 0; i < item.amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(item.pooledObj);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectToPool item in itemsToPool)
        {
            if (item.pooledObj.tag == tag)
            {
                if (item.canGrow)
                {
                    GameObject obj = (GameObject)Instantiate(item.pooledObj);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
    /* Use this to call bulletes in.
     * 
    GameObject bullet = PoolManager.Instance.GetPooledObject(); 
  if (bullet != null) {
    bullet.transform.position = turret.transform.position;
    bullet.transform.rotation = turret.transform.rotation;
    bullet.SetActive(true);
    */
}


