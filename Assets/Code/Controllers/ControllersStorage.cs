using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Controllers
{
    sealed class ControllersStorage : 
        IExecute, ILateExecute, IInitialization, ICleanup
    {
        List<IExecute> _executeStorage;
        List<IInitialization> _intializeStorage;
        List<ILateExecute> _lateExecuteStorage;
        List<ICleanup> _cleanupStorage;
        
        public ControllersStorage()
        {
            _executeStorage = new List<IExecute>();
            _intializeStorage = new List<IInitialization>();
            _cleanupStorage = new List<ICleanup>();
            _lateExecuteStorage = new List<ILateExecute>();
        }

        public void Add(IInteractionObject interactionObject)
        {
            if (interactionObject is IExecute executeObject)
            {
                _executeStorage.Add(executeObject);   
            }
            if (interactionObject is IInitialization initializationObject)
            {
                _intializeStorage.Add(initializationObject);
            }
            if (interactionObject is ICleanup cleanupObject)
            {
                _cleanupStorage.Add(cleanupObject);
            }
            if (interactionObject is ILateExecute lateObject)
            {
                _lateExecuteStorage.Add(lateObject);
            }
        }

        public void Execute(float deltaTime)
        {
            for (int i = 0; i < _executeStorage.Count; ++i)
            {
                _executeStorage[i].Execute(deltaTime);
            }
        }

        public void Initialize()
        {
            for (int i = 0; i < _intializeStorage.Count; ++i)
            {
                _intializeStorage[i].Initialize();
            }
        }

        public void Cleanup()
        {
            for (int i = 0; i < _cleanupStorage.Count; ++ i)
            {
                _cleanupStorage[i].Cleanup();
            }
        }

        public void LateExecute(float deltaTime)
        {
            for (int i = 0; i < _lateExecuteStorage.Count; ++i)
            {
                _lateExecuteStorage[i].LateExecute(deltaTime);
            }
        }
    }
}
