using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamSet : MonoBehaviour
{
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        int layerMine = LayerMask.NameToLayer("Player1");
        //SetLayerRecursively(gameObject, layerMine);
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Debug.Log(view.ViewID);
        }
        
    }

    void SetLayerRecursively(GameObject gameObject, int layerMine)
    {
        if (view.IsMine)
        {
            //gameObject.layer = layerMine;
            if (null == gameObject)
            {
                return;
            }

            gameObject.layer = layerMine;

            foreach (Transform child in gameObject.transform)
            {
                if (null == child)
                {
                    continue;
                }
                SetLayerRecursively(child.gameObject, layerMine);
            }
        }
        else
        {

        }
    }

}
