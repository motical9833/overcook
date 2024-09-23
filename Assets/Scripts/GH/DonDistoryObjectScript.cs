using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDistoryObjectScript : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
