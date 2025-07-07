using UnityEngine;
using TDS.UI.Visual.General;

namespace TDS.UI.BottomBar
{
    //I wanted a system to be extendable and flexible
    //Originally I put everything into one script but it got crazy messy
    //So I decided to put the customisation aspect into each button accordingly
    //And make this script strictly the more functional side at handling those buttons
    public class BottomBarView : MonoBehaviour
    {
        [Tooltip("All the buttons that we will be toggling")]
        [SerializeField] private ButtonVisualParameter[] buttons;   //NOTE:The order of buttons matter
        [Tooltip("How big each of the buttons should scale up to when being toggled")]
        [SerializeField] private float scaleAmount = 1;
        [Tooltip("How long the tween should take to complete")]
        [SerializeField] private float tweenTime = 0.1f;

        [Tooltip("The degrees to shake the camera")]
        [SerializeField] private float shakeAmt = 100;
        [Tooltip("The period of each shake")]
        [SerializeField] private float shakePeriodTime = 0.4f;
        [Tooltip("How long it takes the shaking to settle down to nothing")]
        [SerializeField] private float dropOffTime = 1.5f;


        [SerializeField] private RectTransform highlighter;
        [SerializeField] private RectTransform baseSprite;

        private bool selected;
        private int prevIndex = -1;

        private void Start()
        {
            InitialiseButtonParameters();
        }

        private void InitialiseButtonParameters()
        {
            //We do not start highlighting at start so this should be zeroed out
            highlighter.sizeDelta = Vector2.zero;

            //We set prevIndex at -1 because buttons can be index 0(Refer to SelectedContent())
            prevIndex = -1;

            //Set the original positions so we know where to go to if none is selected later
            for (int i=0; i<buttons.Length; i++)
            {
                RectTransform rect = buttons[i].Transform;
                buttons[i].OriginalPosition = rect.anchoredPosition;
                buttons[i].ExpandedWidth = buttons[i].DefaultWidth * scaleAmount;
            }
        }

        //When user press button...
        public void SelectedContent(int index)
        {
            LeanTween.reset();

            //Play different animations for locked vs unlocked buttons
            if (buttons[index].Locked)
            {
                ShakeAnimation(buttons[index]);

                return;
            }

            //If what is being selected is the same as the previous selected
            //Then we are probably toggling states
            //Otherwise, we are selecting a new button
            if (prevIndex == index)
            {
                selected = !selected;
            }
            else
            {
                selected = true;
            }

            //Update our last selected(This is also why we start at -1)
            //(That way the first click will always be a new button)
            prevIndex = index;

            //If we clicked to deselect, then this should close
            if (!selected)
            {
                Closed();
                return;
            }

            //But if it's not...

            //Go through every button
            for (int i = 0; i < buttons.Length; i++)
            {
                //If this button is the one being selected...
                if (i == index)
                {
                    SelectedAnimation(buttons[i]);
                    continue;
                }

                //This button is not being selected
                //But we need to move them to make space for the selected growing in size
                NonSelectedAnimation(buttons[i], buttons[index]);
            }
        }

        //Activated a button!
        public void ContentActivated()
        {
            //Do stuff
            Debug.Log("Content Activated! Index -" + prevIndex.ToString());
        }

        //None of the buttons are being activated, so we are closing
        public void Closed()
        {
            Debug.Log("Closed");

            //Added this function so we make sure all our buttons are where they should be
            ResetButtons();
        }

        //Just a very basic tweening to put everyone where they need to be
        private void ResetButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                LeanTween.move(buttons[i].Transform, buttons[i].OriginalPosition, tweenTime);
                LeanTween.scale(buttons[i].Transform, Vector3.one, tweenTime);
            }

            LeanTween.move(highlighter, Vector2.zero, tweenTime);
            LeanTween.size(highlighter, Vector2.zero, tweenTime);
        }


        //All the more complex animation code goes here for cleanliness!
        //=========ANIMATIONS=============
        private void SelectedAnimation(ButtonVisualParameter button)
        {
            //Reset position and increase scale to our target scale
            //We reset the position
            //because the ones that should be moving to make way is the other buttons
            LeanTween.move(button.Transform, button.OriginalPosition, tweenTime);
            Vector2 targetScale = Vector2.one * scaleAmount;
            LeanTween.scale(button.Transform, targetScale, tweenTime);

            //We move the highlight into place to match us
            Vector2 highlightPos = new Vector2(button.OriginalPosition.x, 0);
            LeanTween.move(highlighter, highlightPos, tweenTime);
            Vector2 highlightSize = new Vector2(button.DefaultWidth, button.DefaultHeight);
            LeanTween.size(highlighter, highlightSize * scaleAmount, tweenTime);

            ShakeAnimation(baseSprite);

            //Call Content Activated since we are activating one of the buttons
            //Maybe pass in the index if we need to
            ContentActivated();
        }

        private void NonSelectedAnimation(ButtonVisualParameter button, ButtonVisualParameter selectedButton)
        {
            //Here we are calculating how to move the other buttons
            //Calculating the direction and how much to move...
            Vector2 offsetDir = (button.Transform.position - selectedButton.Transform.position).normalized;
            float widthDifference = selectedButton.ExpandedWidth - selectedButton.DefaultWidth;
            Vector2 offsetAmount = offsetDir * widthDifference;

            //Then we apply that movement and scaling accordingly
            Vector2 targetPosition = button.OriginalPosition + offsetAmount;
            LeanTween.move(button.Transform, targetPosition, tweenTime);
            LeanTween.scale(button.Transform, Vector3.one, tweenTime);
        }

        //This function is to generate a shake animation mainly for locked buttons
        private void ShakeAnimation(ButtonVisualParameter button)
        {
            LTDescr shakeTween = LeanTween.rotateAroundLocal(button.Transform, Vector3.right, shakeAmt, shakePeriodTime)
            .setEase(LeanTweenType.easeShake) // This is a special ease that is good for shaking
            .setLoopClamp()
            .setRepeat(-1);

            // Slow the camera shake down to zero
            LeanTween.value(gameObject, shakeAmt, 0f, dropOffTime).setOnUpdate(
                (float val) => {
                    shakeTween.setTo(Vector3.right * val);
                }
            ).setEase(LeanTweenType.easeOutQuad);
        }

        private void ShakeAnimation(RectTransform button)
        {
            LTDescr shakeTween = LeanTween.rotateAroundLocal(button, Vector3.right, shakeAmt, shakePeriodTime)
            .setEase(LeanTweenType.easeShake) // This is a special ease that is good for shaking
            .setLoopClamp()
            .setRepeat(-1);

            // Slow the camera shake down to zero
            LeanTween.value(gameObject, shakeAmt, 0f, dropOffTime).setOnUpdate(
                (float val) => {
                    shakeTween.setTo(Vector3.right * val);
                }
            ).setEase(LeanTweenType.easeOutQuad);
        }
    }
}