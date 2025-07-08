using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace TDS.UI.Victory
{
    //Here is a script that we can use for the score bar/meter
    public class MeterAnimation : MonoBehaviour
    {
        [Header("SETTINGS")]
        [Tooltip("This value is what we would set as our target score value")]
        [SerializeField] private int targetValue;
        [Tooltip("How long would the animation take to get to the target value")]
        [SerializeField] private float duration;

        [Header("COMPONENTS")]
        [Tooltip("Our text mesh to display the score. Optional")]
        [SerializeField] private TextMeshProUGUI valueText;
        [Tooltip("Our slider element to display the score. Optional")]
        [SerializeField] private Slider meterSlider;

        [Tooltip("Unity Event to call when the animation is finished")]
        [SerializeField] private UnityEvent onComplete;

        private float curValue;
        private float elapsedTime;
        private bool isPlaying;

        //This is how we trigger playing it
        //Set it to true to play and set it to false to stop
        public void IsPlaying(bool playing)
        {
            isPlaying = playing;

            //Maybe can implement a Stop() function if needed or a Unity Event for that
            //There isn't any need in this case so I didn't
        }

        private void Update()
        {
            //If it's playing, then we need to update its values
            if (isPlaying)
            {
                UpdateValues();
            }
        }

        //Updating the values
        private void UpdateValues()
        {
            //Doing some math to figure out how to do the lerping
            //so we can make sure our animation finish on the correct time
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            
            //Displaying the values on the text mesh and slider accordingly
            if(valueText != null)
            {
                curValue = Mathf.Lerp(0, targetValue, t);
                valueText.text = Mathf.Round(curValue).ToString();
            }

            if (meterSlider != null)
            {
                meterSlider.value = t;
            }


            //If t is 1, that means we have completed the animation
            if (t == 1)
            {
                isPlaying = false;
                onComplete.Invoke();
            }
        }



        //Helper function so that we can play the anim again when we trigger the victory screen
        public void ResetValues()
        {
            curValue = 0;
            elapsedTime = 0;

            if(valueText != null)
            {
                valueText.text = "0";
            }

            if (meterSlider != null)
            {
                meterSlider.value = 0;
            }

            isPlaying = false;
        }
    }
}