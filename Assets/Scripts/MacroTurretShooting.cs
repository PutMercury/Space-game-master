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
    PhotonView view;
    string teamName;
    int teamNo = TeamSelect.teamNo;
    public Vector3 isAimedCheckValue = RotatingTurrets.isAimedPoint;

    Vector3 targetPoint;
    bool withinFireLine=false;
    float aimSpread = 30;

    public Ray rayCastFromCam;
    RaycastHit hit;



    public void Awake()
    {
        

        view = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
        {
            teamName = "Player1";
        }
        else
        {
            teamName = "Player2";
        }

        bulletsLeft = magazineSize;
        readyToShoot = true;
        //LayerMask friendlyHitCheck = LayerMask.GetMask("Player1");
    }

    private void Update()
    {
        rayCastFromCam = shipCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rayCastFromCam, out hit) & !Physics.Raycast(rayCastFromCam, LayerMask.GetMask(teamName)))
        {
            Debug.DrawRay(rayCastFromCam.origin, hit.point * 10f, Color.green, 10f);
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = rayCastFromCam.GetPoint(100); //This is just a far away point from the ship
            Debug.DrawRay(rayCastFromCam.origin, hit.point * 10f, Color.blue, 10f);
            //Debug.Log(targetPoint);
        }

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
        if (bulletsLeft <= 0 || Input.GetKeyDown(KeyCode.R))
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
        //Ray rayCastFromCam = shipCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        
        // When using ViewportPointToRay, 0.5 0.5 0 is centre of screen
        //RaycastHit hit;

        //checking that ray hits something
        

        if (Physics.Raycast(rayCastFromCam, out hit) &! Physics.Raycast(rayCastFromCam, LayerMask.GetMask(teamName)))
        {
            Debug.DrawRay(rayCastFromCam.origin, hit.point * 10f, Color.green, 10f);
            targetPoint = hit.point;
        }

        else
        {
            targetPoint = rayCastFromCam.GetPoint(100); //This is just a far away point from the ship
            Debug.DrawRay(rayCastFromCam.origin, hit.point * 10f, Color.blue, 10f);
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
            GameObject currentBullet = null;

            //Now calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - shootingBarrel.position;
            //gameObject.transform.rotation = directionWithoutSpread;

            //to calculate spread
            float xSpread = Random.Range(-spread, spread);
            float ySpread = Random.Range(-spread, spread);

            //Calculate the new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(xSpread, ySpread, 0); //just adds the spread onto the initial direction calculation

            //Instantiate bullet (create)

            Debug.Log("reaches if");
            isAimedCheckValue = RotatingTurrets.isAimedPoint;

            Debug.Log(withinFireLine + "before");
            Debug.Log(targetPoint + "targetPoint");
            if (Vector3.Distance(targetPoint, isAimedCheckValue) < aimSpread)// - aimSpread) & (targetPoint < isAimedCheckValue + aimSpread))
            {
                Debug.Log(isAimedCheckValue + "is aimed");

                withinFireLine = true;
                Debug.Log(withinFireLine);

            }
            else
            {
                withinFireLine = false;
                Debug.Log(withinFireLine);
            }

            if (teamNo == 1)
            {
                if (withinFireLine == true)
                {
                    Debug.Log("about to shoot");
                    currentBullet = PhotonNetwork.Instantiate("BulletP1", shootingBarrel.position, Quaternion.identity); //PhotonNetwork.Instatiate is used for instantiating an object that appears on both screens
                    currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
                }
                
            }
            else
            {
                currentBullet = PhotonNetwork.Instantiate("BulletP2", shootingBarrel.position, Quaternion.identity); //PhotonNetwork.Instatiate is used for instantiating an object that appears on both screens
            }

            //rotating bullet in right direction
            

            //currentBullet.AddComponent<MacroBulletBehaviour>();

            

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
