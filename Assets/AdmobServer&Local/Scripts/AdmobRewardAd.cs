using UnityEngine ;
using UnityEngine.UI ;
using System ;

public class AdmobRewardAd : MonoBehaviour {
   [Header ("Admob script :")]
   [SerializeField] private Admob admob ;

   [Header ("UI References :")]
   [SerializeField] private Button uiWatchAdButton ;
   [SerializeField] private Text uiWatchAdText ;
   [SerializeField] private Text uiCoinsText ;

   [Header ("Reward amount :")]
   [SerializeField] private int rewardAmount ;

   private string defaultWatchAdButtonText ;

   private void Start () {
      defaultWatchAdButtonText = uiWatchAdText.text ;
      uiWatchAdButton.onClick.AddListener (OnWatchAdButtonClicked) ;

      admob.OnRewardAdLoaded += OnRewardAdLoadedHandle ;
      admob.OnRewardAdWatched += OnRewardAdWatchedHandle ;
   }

   private void OnWatchAdButtonClicked () {
      admob.RequestRewardAd () ;

      //disable the button:
      uiWatchAdButton.interactable = false ;
      uiWatchAdText.text = "Loading.." ;
   }

   private void OnRewardAdLoadedHandle () {
      //ad is loaded
      admob.ShowRewardAd () ;
   }

   private void OnRewardAdWatchedHandle (GoogleMobileAds.Api.Reward reward) {
      //ad is watched
      uiCoinsText.text = (int.Parse (uiCoinsText.text) + rewardAmount).ToString () ;
      //use admob reward amount:
      //uiCoinsText.text = (int.Parse (uiCoinsText.text) + reward.Amount).ToString () ;


      //enable the button:
      uiWatchAdButton.interactable = true ;
      uiWatchAdText.text = defaultWatchAdButtonText ;
   }
}
