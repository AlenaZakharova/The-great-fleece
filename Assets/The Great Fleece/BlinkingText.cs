using System;
using UnityEngine;
using UnityEngine.UI;

namespace The_Great_Fleece
{
    
    public class BlinkingText : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private Graphic _graphic;
        void Start()
        {
            _graphic = _text.GetComponent<Graphic>();
        }

        private void Update()
        {
            _graphic.color = Color.Lerp(Color.white, new Color(1,1,1,0), Mathf.PingPong(Time.time, 1));
        }
    }
}