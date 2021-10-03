using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
    private bool is_updated = false;
    

    // Update is called once per frame
    void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
            if (!is_updated) {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                var bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                var bullet2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                var bullet3 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet1.transform.position += new Vector3(0.5f, 0.5f, 0);
                bullet3.transform.position += new Vector3(0.5f, -0.5f, 0);
            }

        }
	}
    
    public void UpdateWeapon()
    {
        is_updated = true;
    }
}
