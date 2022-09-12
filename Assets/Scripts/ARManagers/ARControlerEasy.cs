// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System.Collections.Generic;
using Niantic.ARDK.AR;
using Niantic.ARDK.AR.Anchors;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.HitTest;
using Niantic.ARDK.External;
using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Input.Legacy;
using Niantic.ARDK.Utilities.Logging;
using UnityEngine;


namespace Niantic.ARDKExamples.Helpers
{

    public class ARControlerEasy : MonoBehaviour
    {
        public Camera Camera;

        [EnumFlagAttribute]
        public ARHitTestResultType HitTestType = ARHitTestResultType.ExistingPlane;

        public GameObject[] PlacementObjectPf;
        public GameObject PlacementObjectPfEnemyTuret;
        public GameObject PlacementObjectPfEnemyTank;



        public GameObject JoystickControler;
        public GameObject GameManagers;
        int jumlahTank = 0;
        GameObject tankPlace;
        string tankName;

        private List<GameObject> _placedObjects = new List<GameObject>();

        private IARSession _session;

        private void Start()
        {
            ARSessionFactory.SessionInitialized += OnAnyARSessionDidInitialize;
            JoystickControler.SetActive(false);
            GameManagers.SetActive(false);
            int selectedTank = PlayerPrefs.GetInt("selectedTank");
            tankPlace = PlacementObjectPf[selectedTank];
             tankName = tankPlace.name + "(Clone)";
            Debug.Log(tankName);

        }

        private void OnAnyARSessionDidInitialize(AnyARSessionInitializedArgs args)
        {
            _session = args.Session;
            _session.Deinitialized += OnSessionDeinitialized;
        }

        private void OnSessionDeinitialized(ARSessionDeinitializedArgs args)
        {
            ClearObjects();
        }

        private void OnDestroy()
        {
            ARSessionFactory.SessionInitialized -= OnAnyARSessionDidInitialize;

            _session = null;

            ClearObjects();
        }

        private void ClearObjects()
        {
            foreach (var placedObject in _placedObjects)
            {
                Destroy(placedObject);
            }

            _placedObjects.Clear();
        }

        private void Update()
        {
            if (_session == null)
            {
                return;
            }

            if (PlatformAgnosticInput.touchCount <= 0)
            {
                return;
            }

            var touch = PlatformAgnosticInput.GetTouch(0);

            if ((PlatformAgnosticInput.touchCount > 0) && (touch.phase == TouchPhase.Began) && (jumlahTank < 1))
            {
                TouchBegan(touch);
                if (GameObject.Find(tankName) != null)
                {
                    jumlahTank++;
                    Debug.Log(jumlahTank);
                    JoystickControler.SetActive(true);
                    GameManagers.SetActive(true);
                }
            }
        }

        private void TouchBegan(Touch touch)
        {
            var currentFrame = _session.CurrentFrame;
            if (currentFrame == null)
            {
                return;
            }

            var results = currentFrame.HitTest
            (
              Camera.pixelWidth,
              Camera.pixelHeight,
              touch.position,
              HitTestType
            );

            int count = results.Count;


            if (count <= 0)
                return;

            // Get the closest result
            var result = results[0];

            var hitPosition = result.WorldTransform.ToPosition();

            _placedObjects.Add(Instantiate(tankPlace, hitPosition, Quaternion.identity));

            var hitPositionTurret = hitPosition;

            hitPositionTurret.y = hitPosition.y + 0.03f;
            hitPositionTurret.x = Random.Range((hitPosition.x - 0.5f), (hitPosition.x + 1.00f));
            hitPositionTurret.z = Random.Range((hitPosition.z + 0.1f), (hitPosition.z + 0.50f));

            _placedObjects.Add(Instantiate(PlacementObjectPfEnemyTuret, hitPositionTurret, Quaternion.identity));


            var hitPositionTank = hitPosition;

            hitPositionTank.y = hitPosition.y;

            hitPositionTank.x = Random.Range((hitPosition.x + 0.1f), (hitPosition.x + 1.00f));
            hitPositionTank.z = Random.Range((hitPosition.z - 0.5f), (hitPosition.z + 0.50f));
            _placedObjects.Add(Instantiate(PlacementObjectPfEnemyTank, hitPositionTank, Quaternion.identity));
            Debug.Log(hitPositionTank);
            var anchor = result.Anchor;
            Debug.LogFormat
            (
              "Spawning cube at {0} (anchor: {1})",
              hitPosition.ToString("F4"),
              anchor == null
                ? "none"
                : anchor.AnchorType + " " + anchor.Identifier
            );
        }
    }
}
