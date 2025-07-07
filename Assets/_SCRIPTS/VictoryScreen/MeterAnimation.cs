using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace TDS.UI.Victory
{
    public class MeterAnimation : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] private int targetValue;
        [SerializeField] private float duration;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Slider meterSlider;

        [SerializeField] private UnityEvent onComplete;

        private float curValue;
        private float elapsedTime;
        private bool isPlaying;

        public void IsPlaying(bool playing)
        {
            isPlaying = playing;
        }

        private void Update()
        {
            if (isPlaying)
            {
                UpdateValues();
            }
        }

        private void UpdateValues()
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            

            if(valueText != null)
            {
                curValue = Mathf.Lerp(0, targetValue, t);
                valueText.text = Mathf.Round(curValue).ToString();
            }

            if (meterSlider != null)
            {
                meterSlider.value = t;
            }



            if (t == 1)
            {
                isPlaying = false;
                onComplete.Invoke();
            }
        }



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