using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
    private bool is_updated = false;

    int count = 0;
    int limit = 5;

    // Update is called once per frame
    void Update () {
        int shoot_id = PlayerMovement.shoot_id;
        if(shoot_id != -1 && Input.GetTouch(shoot_id).phase == TouchPhase.Began)
        {
            count = 0;
        }
		if (shoot_id!=-1&&Input.GetTouch(shoot_id).phase==TouchPhase.Stationary&&count>limit)
		{
            shoot();
            count = 0;
        }
        else if (shoot_id != -1 && Input.GetTouch(shoot_id).phase == TouchPhase.Ended)
        {
            shoot();
            count = 0;
        }
        else
        {
            count++;
        }
	}
    
    public void UpdateWeapon()
    {
        is_updated = true;
    }

    private void shoot()
    {
        if (!is_updated)
        {
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
