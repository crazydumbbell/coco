using System;
using System.Collections;
using HutongGames.PlayMaker;

using BayatGames.SaveGamePro.Networking;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game web delete action.
	/// </summary>
	[ActionCategory ( "SaveGamePro Cloud" )]
	[Tooltip ( "Deletes the specified identifier from the Web." )]
	public class SaveGameWebDelete : FsmStateAction
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
		/// The identifiers.
		/// </summary>
		[RequiredField]
		public FsmString [] identifiers;
		
		/// <summary>
		/// The break on error.
		/// </summary>
		[Tooltip ( "Stop saving next identifiers and values when an error occures" )]
		public FsmBool breakOnError;
		
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

		public override void Reset ()
		{
			identifiers = new FsmString[1];
			breakOnError = new FsmBool ();
			breakOnError.Value = false;
		}

		public override void OnEnter ()
		{
			StartCoroutine ( DoDelete () );
		}

		IEnumerator DoDelete ()
		{
			SaveGameWeb web = new SaveGameWeb ( url.Value, secretKey.Value, username.Value, password.Value );
			for ( int i = 0; i < identifiers.Length; i++ )
			{
				if ( !identifiers [ i ].IsNone && !string.IsNullOrEmpty ( identifiers [ i ].Value ) )
				{
					yield return StartCoroutine ( web.Delete ( identifiers [ i ].Value ) );
					if ( web.Request.isHttpError || web.Request.isNetworkError )
					{
						requestError.Value = web.Request.error;
						cloudError.Value = web.Request.downloadHandler.text;
						Fsm.Event ( isFailed );
						if ( breakOnError.Value )
						{
							break;
						}
					}
					else
					{
						Fsm.Event ( isSuccess );
					}
				}
			}
			Finish ();
		}

	}

}