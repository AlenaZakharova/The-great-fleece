using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace The_Great_Fleece.Game.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        
        private GFPlayerActions actions;
        private Camera camera;
        private ReactiveProperty<bool> _stoppedMotion = new ReactiveProperty<bool>();
        private const string WalkParameter = "Walk";

        CompositeDisposable disposables = new CompositeDisposable(); 

        public void OnEnable()
        {
            camera = Camera.main;
            actions = new GFPlayerActions();
            actions.Enable();
            actions.Player.Click.started += GoToPoint;
        }

        private void GoToPoint(InputAction.CallbackContext context)
        {
           if (!context.started)
                return;
         
           var ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
           var hit = new RaycastHit();
           if (Physics.Raycast(ray, out hit, 1000000000/*, LayerMask.NameToLayer("Floor")*/))
           {
               _agent.SetDestination(hit.point);
               _animator.SetBool(WalkParameter, true);
               Observable.EveryUpdate().Subscribe(x => ReachedDestinationOrGaveUp()).AddTo(disposables);
           }
        }
        
        private void ReachedDestinationOrGaveUp()
        {
            if (_agent.remainingDistance >= _agent.stoppingDistance) return;
            _animator.SetBool(WalkParameter, false);
        }
    
        public void OnDisable()
        {
            actions.Player.Click.started -= GoToPoint;
            actions.Disable();
            disposables.Dispose();
        }
        
        /*private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                
                if(Physics.Raycast(ray, out hit))
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = hit.point;
                }
            }
        }*/
    }
}
