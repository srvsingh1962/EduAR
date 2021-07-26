using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System;

namespace TMPro
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARTapToPlace : MonoBehaviour
    {
        [SerializeField] GameObject Options;
        [SerializeField] List<GameObject> gameobjectToInstantiate;
        [SerializeField] Transform SpwanedObjectParent;
        [SerializeField] GameObject OptionUI;
        [SerializeField] GameObject AssetLoaderGameObject;

        GameObject spawnedObject;
        private ARRaycastManager _arRaycastManager;
        Vector3 gameobjectPosition = Vector3.zero;
        Quaternion gameobjectRotation = Quaternion.identity;
        Vector3 gameobjectSize = Vector3.one;
        bool spawned = false;
        AssetLoader _assetloader;

        public static event Action onPlacedObject;

        static List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private void Awake()
        {
            _arRaycastManager = GetComponent<ARRaycastManager>();
            _assetloader = AssetLoaderGameObject.GetComponent<AssetLoader>();
        }

        private void Start()
        {
            OptionUI.SetActive(false);
            _assetloader = AssetLoaderGameObject.GetComponent<AssetLoader>();
            if (_assetloader)
            {
                gameobjectToInstantiate = _assetloader.loadedAssets;
            }
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
            touchPosition = default;
            return false;
        }

        // Function to perform after each Option clicked.

        public void OptionClicked()
        {
            GameObject ClickedObject = EventSystem.current.currentSelectedGameObject;
            var letter = ClickedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            int pos = (int)letter.ToCharArray()[0] - 65;
            if (spawnedObject != null)
            {
                spawned = false;
                Destroy(SpwanedObjectParent.gameObject.transform.GetChild(0).gameObject);
                spawnedObject = null;
            }

            if (gameobjectToInstantiate[pos] != null)
            {
                spawnedObject = Instantiate(gameobjectToInstantiate[pos], gameobjectPosition, gameobjectRotation, SpwanedObjectParent);
                spawnedObject.transform.localScale = gameobjectSize;
                spawnedObject.transform.position = gameobjectPosition;
                spawned = true;
            }
        }

        void Update()
        {
            if (spawnedObject != null)
            {
                gameobjectPosition = spawnedObject.transform.position;
                gameobjectRotation = spawnedObject.transform.rotation;
                gameobjectSize = spawnedObject.transform.localScale;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && spawned){
                return;
            }

            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (spawnedObject == null && spawned == false)
                {
                    spawnedObject = Instantiate(gameobjectToInstantiate[0], hitPose.position, Quaternion.identity, SpwanedObjectParent);
                    spawnedObject.transform.localScale = Vector3.one;
                    spawned = true;
                }
                onPlacedObject?.Invoke();
                OptionUI.SetActive(true);
            }
        }
    }
}