using UnityEngine;

namespace DefaultNamespace.Other
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static DontDestroyOnLoad _instance;
        
        private void Awake()
        {
            if (_instance != null && _instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                _instance = this; 
                DontDestroyOnLoad(gameObject);
            }
            
        }
    }
}