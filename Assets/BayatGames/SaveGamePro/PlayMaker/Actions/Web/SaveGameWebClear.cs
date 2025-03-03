using System;
using System.Collections;
using HutongGames.PlayMaker;

using BayatGames.SaveGamePro.Networking;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game web clear action.
	/// </summary>
	[ActionCategory ( "SaveGamePro Cloud" )]
	[Tooltip ( "Clears the user data from the Web." )]
	public class SaveGameWebClear : FsmStateAction
	{

		/// <summary>
		/// The URL.
		/// </summary>
		[RequiredField]
		public FsmString url;
		
		/// <summary>
		/// The secret key.
		/// </summary>
		[RequiredField]
		public FsmString secretKey;
		
		/// <summary>
		/// The username.
		/// </summary>
		[RequiredField]
		public FsmString username;
		
		/// <summary>
		/// The password.
		/// </summary>
		public FsmString password;

		/// <summary>
		/// The request error.
		/// </summary>
		[Title ( "Store Request Error" )]
		[Tooltip ( "The Error that is reported by Request" )]
		[UIHint ( UIHint.Variable )]
		public FsmString requestError;
		
		/// <summary>
		/// The cloud error.
		/// </summary>
		[Title ( "Store Cloud Error" )]
		[Tooltip ( "The Error that is reported by Save Game Pro Cloud" )]
		[UIHint ( UIHint.Variable )]
		public FsmString cloudError;
		
		/// <summary>
		/// The is success event.
		/// </summary>
		public FsmEvent isSuccess;
		
		/// <summary>
		/// The is failed event.
		/// </summary>
		public FsmEvent isFailed;

		public override void OnEnter ()
		{
			StartCoroutine ( DoClear () );
		}

		IEnumerator DoClear ()
		{
			SaveGameWeb web = new SaveGameWeb ( url.Value, secretKey.Value, username.Value, password.Value );
			yield return StartCoroutine ( web.Clear () );
			if ( web.Request.isHttpError || web.Request.isNetworkError )
			{
				requestError.Value = web.Request.error;
				cloudError.Value = web.Request.downloadHandler.text;
				Fsm.Event ( isFailed );
			}
			else
			{
				Fsm.Event ( isSuccess );
			}
			Finish ();
		}

	}

}