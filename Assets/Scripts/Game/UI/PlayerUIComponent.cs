using System;
using System.Globalization;
using Game.GameManager;
using Game.Utils;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerUIComponent: MonoBehaviour
    {
        [SerializeField] 
        private GameState gameState;
        
        [SerializeField] 
        private Text positionText;
        
        [SerializeField] 
        private Text angleText;
        
        [SerializeField] 
        private Text speedText;
        
        [SerializeField] 
        private Text laserChargeText;

        [SerializeField]
        private Image laserChargeImage;
        
        [SerializeField]
        private Image laserChargeMaxImage;
        private void Start()
        {
            BindText(speedText, gameState.speed, FloatFormatter);
            BindText(positionText, gameState.position, Vector2Formatter);
            BindText(angleText, gameState.angle, FloatFormatter);
            BindText(laserChargeText, gameState.laserCharge, FloatFormatter);
            BindLaserBarImage();
        }

        private void BindLaserBarImage()
        {
            gameState.laserCharge.OnValueChanged += value =>
            {
                laserChargeImage.fillAmount = value;
                laserChargeMaxImage.enabled = Math.Abs(value - 1.0f) < 0.001;
            };
        }

        private static string Vector2Formatter(Vector2 value)
        {
            return $"[{value.x:0.00} : {value.y:0.00}]";
        }
        
        private static string FloatFormatter(float value)
        {
            return $"{value:0.00}";
        }
        
        private static string DefaultFormatter<T>(T value)
        {
            return value.ToString();
        }
        
        private static void BindText<T>(Text text, ObservableValue<T> value, Func<T, string> formatter = null) where T: IEquatable<T>
        {
            formatter ??= DefaultFormatter;
            
            text.text = formatter(value.Get());
            
            value.OnValueChanged += v =>
            {
                text.text = formatter(v);
            };
        }
    }
}