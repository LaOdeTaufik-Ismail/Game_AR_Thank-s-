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
 
  public class ARHitTester: MonoBehaviour
  {
    /// The camera used to render the scene. Used to get the center of the screen.
   public Camera Camera;

    /// The types of hit test results to filter against when performing a hit test.
    [EnumFlagAttribute]
    public ARHitTestResultType HitTestType = ARHitTestResultType.ExistingPlane;

    /// The object we will place when we get a valid hit test result!
    public GameObject PlacementObjectPf;

    

    public GameObject JoystickControler;
    int jumlahTank = 0;



        /// A list of placed game objects to be destroyed in the OnDestroy method.
    private List<GameObject> _placedObjects = new List<GameObject>();

    /// Internal reference to the session, used to get the current frame to hit test against.
    private IARSession _session;

    private void Start()
    {
      ARSessionFactory.SessionInitialized += OnAnyARSessionDidInitialize;
            JoystickControler.SetActive(false);
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
        if (GameObject.Find("Tank AR(Clone)") != null)
        {
             jumlahTank++;
             Debug.Log(jumlahTank);
             JoystickControler.SetActive(true);
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

      _placedObjects.Add(Instantiate(PlacementObjectPf, hitPosition, Quaternion.identity));

    

            
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
