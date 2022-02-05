using UnityEngine;

public class SoundsSettings : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
