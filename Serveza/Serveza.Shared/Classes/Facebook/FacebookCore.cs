using Facebook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

namespace Serveza.Classes.Facebook
{
    public class FacebookCore
    {
        FacebookClient _fb = new FacebookClient();
        readonly Uri _callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
        readonly Uri _loginUrl;
        private const string FacebookAppId = "820916864694793";//Enter your FaceBook App ID here       
        private const string FacebookPermissions = "user_about_me";
        public string AccessToken
        {
            get { return _fb.AccessToken; }
        }

        public FacebookClient Client { get { return new FacebookClient(AccessToken); } set { } }

        public FacebookCore()
        {
            _loginUrl = _fb.GetLoginUrl(new
                    {
                        client_id = FacebookAppId,
                        redirect_uri = _callbackUri.AbsoluteUri,
                        scope = FacebookPermissions,
                        display = "popup",
                        response_type = "token"
                    });
            Debug.WriteLine(_callbackUri);//This is useful for fill Windows Store ID in Facebook WebSite
        }
        private void ValidateAndProccessResult(WebAuthenticationResult result)
        {
            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                try
                {
                    var responseUri = new Uri(result.ResponseData.ToString());
                    var facebookOAuthResult = _fb.ParseOAuthCallbackUrl(responseUri);
                    if (string.IsNullOrWhiteSpace(facebookOAuthResult.Error))
                    {
                        _fb.AccessToken = facebookOAuthResult.AccessToken;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    _fb.AccessToken = null;
                    return;
                }
            }
            else if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
               
            }
            else
            {
                _fb.AccessToken = null;
            }
        }
        public void LoginAndContinue()
        {
            WebAuthenticationBroker.AuthenticateAndContinue(_loginUrl);
            Debug.WriteLine("LoginAndContinue : done");
        }
        public void ContinueAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            ValidateAndProccessResult(args.WebAuthenticationResult);
            Debug.WriteLine("ok");
        }

        public ImageBrush getUserImage(string id)
        {
            string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", id, "square", AccessToken);
            Debug.WriteLine(profilePictureUrl);
            return Utils.Utils.UrlToFillSource(profilePictureUrl);
        }
    }
}
