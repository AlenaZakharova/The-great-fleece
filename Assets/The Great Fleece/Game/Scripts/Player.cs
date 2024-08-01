using UnityEngine;
using UnityEngine.InputSystem;

namespace The_Great_Fleece.Game.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        private GFPlayerActions actions;
        private Camera camera;


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
           Debug.DrawRay(camera.transform.position, ray.direction, Color.red, 120);
           var hit = new RaycastHit();
           if (Physics.Raycast(ray, out hit, 1000000000/*, LayerMask.NameToLayer("Floor")*/))
           {
               GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
               sphere.transform.position = hit.point;
               Debug.LogError(hit.collider.name);
           }
        }
    
        public void OnDisable()
        {
            actions.Player.Click.started -= GoToPoint;
            actions.Disable();
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
