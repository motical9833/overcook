using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObjectPoolScript : MonoBehaviour
{
    private GameObject prefab;
    private int poolSize = 20;
    private Queue<GameObject> pool;
    private string resourceName = "";

    void Start()
    {
        pool = CreateObjectPool();

        if(pool == null)
        {
            Debug.LogError("IngredientPool 객체 풀을 생성하지 못했음");
        }
    }

    // Ingredient 객체 풀 생성 로직
    private Queue<GameObject> CreateObjectPool()
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();
        resourceName = gameObject.name + "/" + gameObject.name;
        prefab = Resources.Load("3D/Food_Objects/" + resourceName) as GameObject;

        if(prefab == null)
        {
            Debug.LogError("prefab을 찾을 수 없음");
            return null;
        }

        // 풀 생성
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);

            SphereCollider col = obj.AddComponent<SphereCollider>();
            col.radius = 0.004f;
            col.center = new Vector3(0.0f, 0.004f, 0.0f);
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
            objectPool.Enqueue(obj);
        }

        return objectPool;
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
