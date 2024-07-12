using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    bool isDestroied;
    private void OnTriggerEnter(Collider other)
    {
        if (isDestroied)
        {
            return;
        }
        Player player = other.transform.GetComponentInParent<Player>();
        if (player != null)
        {
            isDestroied = true;
            player.ChangeBullet(bulletPrefab);
            Destroy(this.gameObject);
            
        }
    }

}
