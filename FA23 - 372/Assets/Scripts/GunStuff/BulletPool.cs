using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    private List<GameObject> pooledBullets = new();
    private int poolAmount = 20;

    [SerializeField] GameObject bulletPrefab;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        for(int i = 0; i<poolAmount; i++) {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledBullets.Add(obj);
        }
    }

    public GameObject GetPooledObjects() {
        for (int i = 0; i < pooledBullets.Count; i++) {
            if (!pooledBullets[i].activeInHierarchy) {
                return pooledBullets[i];
            }
        }
        return null;
    }
}
