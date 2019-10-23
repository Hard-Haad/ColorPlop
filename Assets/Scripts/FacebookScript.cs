using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookScript : MonoBehaviour {
	string fastAsResult;
	string timerResult;
	string screenShot_name;
	bool shareImage;

	void Awake(){

		screenShot_name = "ScoreShot";

		if (!FB.IsInitialized) {
			FB.Init ();
		} else {
			FB.ActivateApp ();
		}
	}

	public void Login(){
		FB.LogInWithReadPermissions (callback:OnLoginIn);
	}

	private void OnLoginIn(ILoginResult result){
		if (FB.IsLoggedIn) {
			AccessToken token = AccessToken.CurrentAccessToken;
		}
	}

	public void Share(){
		FB.ShareLink(
			contentTitle:"Haad's Game",
			contentURL:new System.Uri("https://www.facebook.com/ColorPlop-454065885059828"),
			contentDescription:"Color matching accuracy game",
			callback:OnShare);
	}
		

	private void OnShare(IShareResult result){
		if (result.Cancelled || !string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Share link error: " + result.Error); 
		} else if (!string.IsNullOrEmpty (result.PostId)) {
			Debug.Log (result.PostId);
		} else {
			Debug.Log ("Share succeed");
		}
	}

	public void SetResults(string _fastAsResult, string _timerResult){
		timerResult = _timerResult;
		fastAsResult = _fastAsResult;
	}

	public void ShareImage(){
		if (!shareImage) {
			StartCoroutine (ShareImageShot ());
		}

	}

	IEnumerator ShareImageShot(){
		shareImage = true;
		yield return new WaitForEndOfFrame ();

		Texture2D screenTexture = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, true);
		screenTexture.ReadPixels (new Rect (0f, 0f, Screen.width, Screen.height), 0, 0);
		screenTexture.Apply ();

		byte[] dataToSave = screenTexture.EncodeToPNG ();
		string destination = Path.Combine (Application.persistentDataPath, screenShot_name);
		File.WriteAllBytes (destination, dataToSave);

		var wwwForm = new WWWForm ();
		wwwForm.AddBinaryData ("image", dataToSave, "ScoreShot.png");
		FB.API ("/me/photos", HttpMethod.POST, null, wwwForm);

		shareImage = false;
	}

	void OnPhotoShare(IGraphResult result){
		
	}

		
}
