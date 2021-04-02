using UnityEngine ;

public class AdmobBannerAd : MonoBehaviour {
   [Header ("Admob script :")]
   [SerializeField] private Admob admob ;

   private void Start () {
      //show banner ad when admob sdk is initialized:
      admob.OnInitComplete += () => admob.ShowBannerAd () ;


      //here you can use banner events:
      admob.OnBannerAdOpening += () => {
         Debug.Log ("Banner ad is clicked") ;
      } ;
   }
}
