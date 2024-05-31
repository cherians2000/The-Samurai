using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform LaunchPoint;
    private float scale = 0.2902102f;

    public void FrieProjectile()
    {
       GameObject projectile =Instantiate(projectilePrefab,LaunchPoint.position,projectilePrefab.transform.rotation);
        Vector3 orgScale =projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(
            orgScale.x = transform.localScale.x > 0 ? scale : -scale,
            orgScale.y,
            orgScale.z
            ) ;
    }
}
