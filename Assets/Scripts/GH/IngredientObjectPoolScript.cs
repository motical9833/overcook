using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientObjectPoolScript : MonoBehaviour
{
    private GameObject prefab;
    private int poolSize = 20;
    private Queue<GameObject> pool;
    private string resourceName = "";

    void Start()
    {
        pool = new Queue<GameObject>();

        resourceName = gameObject.name + "/" + gameObject.name;

        prefab = Resources.Load("3D/Food_Objects/" + resourceName) as GameObject;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            
            SphereCollider col = obj.AddComponent<SphereCollider>();
            col.radius = 0.004f;
            col.center = new Vector3(0.0f, 0.004f,0.0f);
            col.isTrigger = false;

            Rigidbody rigid = obj.AddComponent<Rigidbody>();
            rigid.drag = 0.5f;
            rigid.angularDrag = 0.1f;

            obj.tag = "Ingredient";
            obj.transform.parent = this.gameObject.transform;
            IngredientScript ingredientScript = obj.AddComponent<IngredientScript>();

            ingredientScript.Initialize();
            obj.transform.localPosition = Vector3.zero;
            obj.transform.parent = null;
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            GetIngredientPoolObject();
        }
    }

    public GameObject GetIngredientPoolObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            if(obj == null)
            {
                UnityEngine.Debug.LogError("obj로 Null이 반환되었습니다");
                return null;
            }
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            if (obj == null)
            {
                UnityEngine.Debug.LogError("obj로 Null이 반환되었습니다");
                return null;
            }
            return obj;
        }
    }

    public void PushIngredientPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
