using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;
    [SerializeField] protected GameObject muzzle;
    [SerializeField] protected bool reloadCorouting = false;
    [SerializeField] protected int ammo = 10;
    protected bool isShooting = false;

    public int Ammo
    {
        get { return ammo; }
        set
        {
            ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity);
            OnAmmoChange?.Invoke(Ammo);
        }
    }
    public bool AmmoFull { get => Ammo >= weaponData.AmmoCapacity; }

    [field: SerializeField]
    public UnityEvent OnShoot { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }

    [field: SerializeField]

    public UnityEvent<int> OnAmmoChange { get; set; }

    private void Start()
    {
        Ammo = weaponData.AmmoCapacity;
    }
    public void TryShooting()
    {
        isShooting = true;
    }
    public void StopShooting()
    {
        isShooting = false;
    }
    public void Reload(int ammo)
    {
        Ammo += ammo;
    }
    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if (isShooting && reloadCorouting == false)
        {
            if (Ammo > 0)
            {
                Ammo--;
                OnShoot?.Invoke();
                for (int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShootingCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootingCoroutine()
    {
        reloadCorouting = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay);
        reloadCorouting = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotaion)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, rotaion);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spred = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpredRotation = Quaternion.Euler(new Vector3(0, 0, spred));
        return muzzle.transform.rotation * bulletSpredRotation;
    }
}
