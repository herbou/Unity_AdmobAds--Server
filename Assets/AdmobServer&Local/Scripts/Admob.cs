using UnityEngine ;
using GoogleMobileAds.Api ;
using GoogleMobileAds.Common ;
using UnityEngine.Events ;

public class Admob : MonoBehaviour {
   [Header ("Admob Ad Units :")]
   [SerializeField] [TextArea (1, 2)] string idBanner = "ca-app-pub-3940256099942544/6300978111" ;
   [SerializeField] [TextArea (1, 2)] string idInterstitial = "ca-app-pub-3940256099942544/1033173712" ;
   [SerializeField] [TextArea (1, 2)] string idReward = "ca-app-pub-3940256099942544/5224354917" ;

   [Header ("Admob Server AD Units :")]
   [SerializeField] [Tooltip ("Check it if you want to load ad units IDs from a server.   if ad units didn't load successfully or some error happened, Admob will use ad units above.")] private bool serverAdUnitsEnabled = false ;

   [HideInInspector] public  BannerView AdBanner ;
   [HideInInspector] public InterstitialAd AdInterstitial ;
   [HideInInspector] public RewardedAd AdReward ;

   public UnityAction OnInitComplete ;

   private void Start () {
      // if server Ad Units is enabled then use server units:
      if (serverAdUnitsEnabled) {
         idBanner = (string.IsNullOrEmpty (AdmobServerAdUnits.BannerId)) ? idBanner : AdmobServerAdUnits.BannerId ;
         idInterstitial = (string.IsNullOrEmpty (AdmobServerAdUnits.InterstitialId)) ? idInterstitial : AdmobServerAdUnits.InterstitialId ;
         idReward = (string.IsNullOrEmpty (AdmobServerAdUnits.RewardId)) ? idReward : AdmobServerAdUnits.RewardId ;
      }
      #if UNITY_EDITOR
      //this block is just for debugging in the editor
      string color ;
      if (AdmobServer.adUnitsLoaded) {
         color = "yellow" ;
         Debug.Log (">><color=yellow>You're using <b>SERVER</b> ad units :</color>") ;
      } else {
         color = "orange" ;
         Debug.Log (">><color=orange>You're using default ad units :</color>") ;
      }
      Debug.Log ("<color=" + color + "><b>Banner       : </b>" + idBanner + "</color>") ;
      Debug.Log ("<color=" + color + "><b>Interstitial : </b>" + idInterstitial + "</color>") ;
      Debug.Log ("<color=" + color + "><b>Reward      : </b>" + idReward + "</color>") ;
      #endif

      RequestConfiguration requestConfiguration =
         new RequestConfiguration.Builder ()
            .SetTagForChildDirectedTreatment (TagForChildDirectedTreatment.Unspecified)
            .build () ;

      MobileAds.SetRequestConfiguration (requestConfiguration) ;


      MobileAds.Initialize (initstatus => {
         MobileAdsEventExecutor.ExecuteInUpdate (() => {
            if (OnInitComplete != null)
               OnInitComplete.Invoke () ;
         }) ;
      }) ;
   }

   private AdRequest CreateAdRequest () {
      return new AdRequest.Builder ()
         .TagForChildDirectedTreatment (false)
         .AddExtra ("npa", PlayerPrefs.GetString ("npa", "1"))
         .Build () ;
   }

   private void OnDestroy () {
      DestroyBannerAd () ;
      DestroyInterstitialAd () ;
   }


   #region Banner Ad ------------------------------------------------------------------------------

   public UnityAction OnBannerAdOpening ;

   public void ShowBannerAd () {
      AdBanner = new BannerView (idBanner, AdSize.Banner, AdPosition.Bottom) ;
      AdBanner.OnAdOpening += (sender, e) => {
         if (OnBannerAdOpening != null)
            OnBannerAdOpening.Invoke () ;
      } ;
      AdBanner.LoadAd (CreateAdRequest ()) ;
   }

   public void DestroyBannerAd () {
      if (AdBanner != null)
         AdBanner.Destroy () ;
   }

   #endregion

   #region Interstitial Ad ------------------------------------------------------------------------

   public UnityAction OnInterstitialAdLoaded ;
   public UnityAction OnInterstitialAdFailedToLoad ;
   public UnityAction OnInterstitialAdOpening ;
   public UnityAction OnInterstitialAdClosed ;

   public void RequestInterstitialAd () {
      AdInterstitial = new InterstitialAd (idInterstitial) ;
      AdInterstitial.OnAdClosed += (sender, e) => {
         if (OnInterstitialAdClosed != null)
            OnInterstitialAdClosed.Invoke () ;
      } ;
      AdInterstitial.OnAdOpening += (sender, e) => {
         if (OnInterstitialAdOpening != null)
            OnInterstitialAdOpening.Invoke () ;
      } ;
      AdInterstitial.OnAdFailedToLoad += (sender, e) => {
         if (OnInterstitialAdFailedToLoad != null)
            OnInterstitialAdFailedToLoad.Invoke () ;
      } ;
      AdInterstitial.OnAdLoaded += (sender, e) => {
         if (OnInterstitialAdLoaded != null)
            OnInterstitialAdLoaded.Invoke () ;
      } ;
      AdInterstitial.LoadAd (CreateAdRequest ()) ;
   }

   public void ShowInterstitialAd () {
      if (AdInterstitial.IsLoaded ()) {
         AdInterstitial.Show () ;
      }
   }

   public void DestroyInterstitialAd () {
      if (AdInterstitial != null)
         AdInterstitial.Destroy () ;
   }

   #endregion

   #region Reward Ad ------------------------------------------------------------------------------

   public UnityAction<Reward> OnRewardAdWatched ;
   public UnityAction OnRewardAdLoaded ;
   public UnityAction OnRewardAdFailedToLoad ;
   public UnityAction OnRewardAdOpening ;
   public UnityAction OnRewardAdClosed ;

   public void RequestRewardAd () {
      AdReward = new RewardedAd (idReward) ;
      AdReward.OnAdClosed += (sender, e) => {
         if (OnRewardAdClosed != null)
            OnRewardAdClosed.Invoke () ;
      } ;
      AdReward.OnAdOpening += (sender, e) => {
         if (OnRewardAdOpening != null)
            OnRewardAdOpening.Invoke () ;
      } ;
      AdReward.OnAdFailedToLoad += (sender, e) => {
         if (OnRewardAdFailedToLoad != null)
            OnRewardAdFailedToLoad.Invoke () ;
      } ;
      AdReward.OnAdLoaded += (sender, e) => {
         if (OnRewardAdLoaded != null)
            OnRewardAdLoaded.Invoke () ;
      } ;
      AdReward.OnUserEarnedReward += (sender, reward) => {
         if (OnRewardAdWatched != null)
            OnRewardAdWatched.Invoke (reward) ;
      } ;
      AdReward.LoadAd (CreateAdRequest ()) ;
   }

   public void ShowRewardAd () {
      if (AdReward.IsLoaded ()) {
         AdReward.Show () ;
      }
   }

   #endregion



}
