using UnityEngine;

public class Yandex : MonoBehaviour
{
    public static Yandex Instance;

    public bool isMobile = false;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlatform(string value)
    {
        if (value == "mobile" || value == "tablet")
        {
            isMobile = true;
        }
        else
        {
            isMobile = false;
        }
        
    }
}
