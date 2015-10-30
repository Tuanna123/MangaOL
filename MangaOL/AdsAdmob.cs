using GoogleAds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MangaOL
{
    public class AdsAdmob
    {
        internal static string unitID = "ca-app-pub-6696407504648725/5455676093";
        internal static string unitIDFull = "ca-app-pub-6696407504648725/2022701695";
        internal static bool fourceTesting = true;

        internal static bool showAds = true;
        internal static int countLoadAds = 0;

        private static InterstitialAd adsInter;

        internal static void LoadBanner(Grid gridName)
        {
            if (AdsAdmob.showAds)
            {
                AdView adView = new AdView
                {
                    Format = AdFormats.Banner,
                    AdUnitID = AdsAdmob.unitID
                };
                AdRequest adRequest = new AdRequest();
                adRequest.ForceTesting = AdsAdmob.fourceTesting;
                gridName.Children.Add(adView);
                adView.LoadAd(adRequest);
            }
        }

        internal static void LoadSmartBanner(Grid gridName)
        {
            if (AdsAdmob.showAds)
            {
                AdView adView = new AdView
                {
                    Format = AdFormats.SmartBanner,
                    AdUnitID = AdsAdmob.unitID
                };
                AdRequest adRequest = new AdRequest();
                adRequest.ForceTesting = AdsAdmob.fourceTesting;
                gridName.Children.Add(adView);
                adView.LoadAd(adRequest);
            }
        }

        internal static void LoadInterstitialAd()
        {
            if (AdsAdmob.showAds)
            {
                AdsAdmob.countLoadAds = 0;
                AdsAdmob.adsInter = new InterstitialAd(AdsAdmob.unitIDFull);
                AdRequest adRequest = new AdRequest();
                adRequest.ForceTesting = AdsAdmob.fourceTesting;
                AdsAdmob.adsInter.LoadAd(adRequest);
                AdsAdmob.adsInter.ReceivedAd += adsInter_ReceivedAd;
                AdsAdmob.adsInter.DismissingOverlay += adsInter_DismissingOverlay;
               
               
            }
        }

        static void adsInter_DismissingOverlay(object sender, AdEventArgs e)
        {
         //   AdsAdmob.countLoadAds = 0;
        }

        static void adsInter_ReceivedAd(object sender, AdEventArgs e)
        {
            AdsAdmob.adsInter.ShowAd();
        }
     
    }
}
