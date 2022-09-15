using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PlacementHandler : MonoBehaviour
{
    public Camera arCamera;
    [SerializeField] ARRaycastManager aRRaycastManager;
    [SerializeField] Pose placementPose;
    [SerializeField] bool validPosition;
    [SerializeField] bool placed;
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject objectToPlace;
    [SerializeField] GameObject shootingPoint;
    [SerializeField] float range = 100f;
    public GameObject defaultPlane;
    public ARPlaneManager aRPlaneManager;
    private void Awake()
    {
        UIManager.Instance.placeShootBtn.gameObject.SetActive(true);
        UIManager.Instance.shootBtn.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!placed)
        {
            UpdatePose();
            UpdateIndicatorPose();
        }
        //TapToPlaceObject();
    }
    public void UpdatePose()
    {
        print($"VALID POSITION IS::{validPosition}");

        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hit = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hit, TrackableType.Planes);

        validPosition = hit.Count > 0;
        if (validPosition)
        {
            placementPose = hit[0].pose;
        }
    }
    public void UpdateIndicatorPose()
    {
        if (validPosition)
        {
            print($"INDICATOR IS SET TO TRUE");
            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            print($"INDICATOR IS SET TO FALSE");
            indicator.SetActive(false);
        }
    }
    public void Shoot()
    {
        if (GameManager.Instance.score > 1)
        {
            RaycastHit hit;
            if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, range))
            {
                print($"COLLIDED WITH :: {hit.collider.name}");
                GameManager.Instance.score -= 1;
                UIManager.Instance.UpdateScore();
            }
        }
        else
        {
            print("DESTROYED OBJ");
            GameManager.Instance.OnGameComplete();
            Destroy(SpawnedObject);
        }
    }
    [SerializeField] Vector3 touchPosition;
    [SerializeField] List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject SpawnedObject;

    public void TapToPlaceObject()
    {
        /*if (validPosition)
        {
            //Vector3 pos = new Vector3(indicator.transform.position.x, indicator.transform.position.y, indicator.transform.position.z);
            if (SpawnedObject == null)
            {
                print("INSTANTIATE");
                SpawnedObject = Instantiate(objectToPlace, indicator.transform.position, indicator.transform.rotation);
            }
            else
            {
                print("RESET POSITION");
                SpawnedObject.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
            }
        }*/
        if (validPosition)
        {
            SpawnedObject = Instantiate(objectToPlace, indicator.transform.position, indicator.transform.rotation);
            indicator.SetActive(false);

            placed = true;
            UIManager.Instance.shootingPoint.gameObject.SetActive(true);
            UIManager.Instance.placeShootBtn.gameObject.SetActive(false);
            UIManager.Instance.shootBtn.gameObject.SetActive(true);
        }

    }
    public void showHidePlane(bool show)
    {
        defaultPlane.SetActive(show);
        aRPlaneManager.planePrefab = show ? defaultPlane : null;
    }
    public void Checking()
    {
        print("CHECK CHECK CHECK");
    }
}
