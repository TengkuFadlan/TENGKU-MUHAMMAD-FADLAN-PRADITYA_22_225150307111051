using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{

    public Transform barrel;
    public GameObject bulletPrefab;
    public AudioClip gunAudioClip;
    public GameObject gunBar;
    public Slider rifleSlider;
    public Slider shotgunSlider;

    public float bulletSpeed = 20f;
    public float bulletLifeTime = 2f;
    public float rifleRateOfFire = 100f;
    public float rifleDamage = 11f;
    public float shotgunRateOfFire = 100f;
    public float shotgunBullets = 5f;
    public float shotgunDamage = 9f;
    public float shotgunSpreadAngleDegree = 10f;

    float lastRifleShot = 0f;
    float lastShotgunShot = 0f;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        gunBar.SetActive(true);
    }

    void OnDisable()
    {
        if (!gunBar)
            return;

        gunBar.SetActive(false);
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (Input.GetButton("Fire1") && Time.time - lastRifleShot >= 60f / rifleRateOfFire)
        {
            lastRifleShot = Time.time;
            FireRifle();
        }

        if (Input.GetButtonDown("Fire2") && Time.time - lastShotgunShot >= 60f / shotgunRateOfFire)
        {
            lastShotgunShot = Time.time;
            FireShotgun();
        }

        float shotgunBarSize = Math.Clamp((Time.time-lastShotgunShot)/(60f / shotgunRateOfFire), 0, 1);
        float rifleBarSize = Math.Clamp((Time.time-lastRifleShot)/(60f / rifleRateOfFire), 0, 1);

        rifleSlider.value = rifleBarSize;
        shotgunSlider.value = shotgunBarSize;
    }

    void FireRifle()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);

        PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
        playerBullet.damage = rifleDamage;

        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(-barrel.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bullet, bulletLifeTime);

        audioSource.PlayOneShot(gunAudioClip);
    }

    void FireShotgun()
    {
        for (int i = 0; i < shotgunBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);

            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            playerBullet.damage = shotgunDamage;

            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-shotgunSpreadAngleDegree, shotgunSpreadAngleDegree));
            Vector2 bulletDirection = randomRotation * -barrel.up;
            bulletRB.AddForce(bulletDirection * bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, bulletLifeTime);
        }

        audioSource.PlayOneShot(gunAudioClip);
    }
}
