using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button mServerBtn;
    [SerializeField] private Button mHostBtn;
    [SerializeField] private Button mClientBtn;

    void Awake()
    {
        mServerBtn.onClick.AddListener(() =>
        { NetworkManager.Singleton.StartServer(); });
        mHostBtn.onClick.AddListener(() => 
        { NetworkManager.Singleton.StartHost(); });
        mClientBtn.onClick.AddListener(() =>
        { NetworkManager.Singleton.StartClient(); });

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
