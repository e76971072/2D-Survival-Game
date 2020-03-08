using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace Helpers
{
    public class GooglePlaySetup : MonoBehaviour
    {
        private void Start()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                // enables saving game progress.
                .EnableSavedGames()
                // requests the email address of the player be available.
                // Will bring up a prompt for consent.
                .RequestEmail()
                // requests a server auth code be generated so it can be passed to an
                //  associated back end server application and exchanged for an OAuth token.
                .RequestServerAuthCode(false)
                // requests an ID token be generated.  This OAuth token can be used to
                //  identify the player to other services such as Firebase.
                .RequestIdToken()
                .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
            
            if (!PlayGamesPlatform.Instance.localUser.authenticated) {
                // Sign in with Play Game Services, showing the consent dialog
                // by setting the second parameter to isSilent=false.
                PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
            } else {
                // Sign out of play games
                PlayGamesPlatform.Instance.SignOut();
                Debug.Log("sign out");
            }
        }

        public void SignInCallback(bool success)
        {
            if (success)
            {
                Debug.Log($"({Social.localUser.userName}) Signed in!");
            }
            else
            {
                Debug.Log("(Lollygagger) Sign-in failed...");
            }
        }
    }
}