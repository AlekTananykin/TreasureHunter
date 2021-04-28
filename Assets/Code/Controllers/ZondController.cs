using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal class ZondController : IInitialization, ICleanup
    {
        GameObject _cameraView;
        GameObject _zondView;
        GameObject _heroView;
        public ZondController(GameObject frontCameraView, 
            GameObject zondView, GameObject heroView)
        {
            _cameraView = frontCameraView;
            _zondView = zondView;
            _zondView.layer = LayerMask.NameToLayer("Ignore Raycast");
            _heroView = heroView;

        }

        public void Initialize()
        {
            ZondScript zondScript = _zondView.AddComponent<ZondScript>();
            zondScript.On_Zond_Trigger_Enter += SetCameraCarrier;
            zondScript.On_Zond_Trigger_Exit += UnsetCameraCarrier;
        }

        public void Cleanup()
        {
            ZondScript zondScript = _zondView.GetComponent<ZondScript>();
            zondScript.On_Zond_Trigger_Enter -= SetCameraCarrier;
            zondScript.On_Zond_Trigger_Exit -= UnsetCameraCarrier;
        }

        private void UnsetCameraCarrier(GameObject cameraCarrier)
        {
            SetCameraCarrier(_heroView);
        }

        private void SetCameraCarrier(GameObject cameraCarrier)
        {
            cameraCarrier.transform.parent = _cameraView.transform;
            _cameraView.transform.position = Vector3.forward;
            _cameraView.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        public void SetPosition(Vector3 position)
        {
            _zondView.transform.position = position;
        }


    }
}
