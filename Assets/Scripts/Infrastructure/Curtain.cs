using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class Curtain : MonoBehaviour
    {
        public Slider LoadProgressBar;
        [SerializeField] private Image _image;

        public void Initialize()
        {
            ResourceRequest imageCurtain = Resources.LoadAsync<Sprite>("Curtain");
        
            imageCurtain.completed += _ =>
            {
                _image.sprite = imageCurtain.asset as Sprite;
                Debug.Log("Curtain loaded");
            };
        }
    
    }
}
