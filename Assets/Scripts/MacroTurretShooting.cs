using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MacroTurretShooting : MonoBehaviour
{
    public GameObject bullet;

    public float shootForce;
    

    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;

    public int magazineSize;
    public int bulletsPerTap;

    public bool allowButtonHold;

    int bulletsLeft;
    int bulletsShot;
    

    bool shooting;
    bool readyToShoot;
    bool reloading;
    bool isTurretAlligned = true;

    public Camera shipCam;
    public Transform bulletSpawn1, bulletSpawn2, bulletSpawn3, bulletSpawn4;
    private int barrelNo = 1;
    private Transform shootingBarrel;

    public bool allowInvoke = true; // For bug fixing and stuff
    
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;
    private bool continueShoot = true;
    




    public void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        //LayerMask friendlyHitCheck = LayerMask.GetMask("Player1");
    }

    private void Update()
    {

        MyInput();

        //set ammo display
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
        }

       

    }


    private void MyInput()
    {
        //checks if weapon is full or semi auto
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }


        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reloading
        if (bulletsLeft <= 0)
        {
            Reload();
        }


        //actual shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && isTurretAlligned) //&& is simply 'and', but it only works on booleans
        {
            //sets bullets shot to 0
            bulletsShot = 0;
            Shoot();
        }


    }

    public void Shoot()
    {
        readyToShoot = false;

        //to find where to aim turrets using raycast
        Ray rayCastFromCam = shipCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // When using ViewportPointToRay, 0.5 0.5 0 is centre of screen
        RaycastHit hit;

        //checking that ray hits something
        Vector3 targetPoint;

        if (Physics.Raycast(rayCastFromCam, out hit) &! Physics.Raycast(rayCastFromCam, LayerMask.GetMask("Player1")))
        {
            targetPoint = hit.point;
        }

        else
        {
            targetPoint = rayCastFromCam.GetPoint(100); //This is just a far away point from the ship
            //Debug.Log(targetPoint);
        }

        if (barrelNo == 1)
        {
            shootingBarrel = bulletSpawn1;
        }

        if (barrelNo == 2)
        {
            shootingBarrel = bulletSpawn2;
        }

        if (barrelNo == 3)
        {
            shootingBarrel = bulletSpawn3;
        }

        if (barrelNo == 4)
        {
            shootingBarrel = bulletSpawn4;
        }

        if (continueShoot)
        {
            //Now calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - shootingBarrel.position;
            //gameObject.transform.rotation = directionWithoutSpread;

            //to calculate spread
            float xSpread = Random.Range(-spread, spread);
            float ySpread = Random.Range(-spread, spread);

            //Calculate the new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(xSpread, ySpread, 0); //just adds the spread onto the initial direction calculation

            //Instantiate bullet (create)
            GameObject currentBullet = PhotonNetwork.Instantiate("Bullet", shootingBarrel.position, Quaternion.identity); //PhotonNetwork.Instatiate is used for instantiating an object that appears on both screens

            //rotating bullet in right direction
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

            currentBullet.AddComponent<MaxBulletRange>();
            

            //instantiating muzzleflash
            if (muzzleFlash != null)
            {
             Instantiate(muzzleFlash, shootingBarrel.position, Quaternion.identity);
            }
         continueShoot = false;
         Delay();
           
        }   
        
       
        

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting); //'Invoke' simply runs a function, If timeBetweenShooting == 2, then it would wait two seconds, then execute ResetShot()
            allowInvoke = false;
        }

        //allows repeating shoot function if more than one bulletsPerTap
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);      
        }

    }

    public void Delay()
    {
        Invoke("NextBarrel", 0.25f);
        
    }

    


    private void NextBarrel()
    {
        if (barrelNo < 4)
        {
            barrelNo++;
        }
        else
        {
            barrelNo = 1;
        }
        continueShoot = true;
    }
  
    private void ResetShot()
    {
        //Allows shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
