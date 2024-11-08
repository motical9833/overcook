using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTextureSetting : MonoBehaviour
{
    void Start()
    {
        IngredientsDataScript ingredientsData = GameObject.FindWithTag("StageManager").GetComponent<IngredientsDataScript>();
        Vector2 offset = ingredientsData.GetIngredientOffset(this.name);
        Renderer skinnedMeshRenderer = gameObject.GetComponent<Transform>().GetChild(2).GetComponent<Renderer>();
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

        skinnedMeshRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetVector("_MainTex_ST", new Vector4(0.5f, 0.5f, offset.x, offset.y));
        skinnedMeshRenderer.SetPropertyBlock(propertyBlock);
    }
}