using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Map
{
    public class ArmyPanel : MapObjectPanelBase
    {
        [SerializeField] private TMP_Text soldierName;
        [SerializeField] private Image startLocationImage;
        [SerializeField] private TMP_Text startLocationText;
        [SerializeField] private Image destinationLocationImage;
        [SerializeField] private TMP_Text destinationLocationText;
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TMP_Text progressText;

        public override void InitializePanel() 
        {
        }

        public void UpdateArmyPanelFeudatories(FeudatoryDataSC startFeudatory, FeudatoryDataSC destinationFeudatory, float progress, float journeyDuration, ArmyData data)
        {
            soldierName.text = data.armyName;
        
            if(startLocationImage != null)
            {
                startLocationImage.sprite = startFeudatory.Icon;
                startLocationText.text = startFeudatory.Name;
            }
        
            if(destinationLocationImage != null)
            {
                destinationLocationImage.sprite = destinationFeudatory.Icon;
                destinationLocationText.text = destinationFeudatory.Name;
            }

            UpdateProgress(progress, journeyDuration);
        }

        public void UpdateProgress(float progress, float journeyDuration)
        {
            if(progressSlider != null)
                progressSlider.value = progress;
        
            float remainingTime = journeyDuration * (1f - progress);
            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);

            if(progressText != null)
                progressText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}