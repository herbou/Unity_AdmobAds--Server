using UnityEngine ;

public class AdmobInterstitialAd : MonoBehaviour {
   [Header ("Admob script :")]
   [SerializeField] private Admob admob ;

   [Header ("Gameover :")]
   [SerializeField] private GameOverSimulation gameoverSimulator ;

   private void Start () {
      admob.OnInitComplete += () => admob.RequestInterstitialAd () ;

      gameoverSimulator.OnGameover += () => admob.ShowInterstitialAd () ;
   }
}
