using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game delete action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Deletes the specified identifier." )]
	public class SaveGameDelete : FsmStateAction
	{

		/// <summary>
		/// The identifiers.
		/// </summary>
		[RequiredField]
		public FsmString [] identifiers;

		public override void OnEnter ()
		{
			for ( int i = 0; i < identifiers.Length; i++ )
			{
				if ( !identifiers [ i ].IsNone && !string.IsNullOrEmpty ( identifiers [ i ].Value ) )
				{
					SaveGame.Delete ( identifiers [ i ].Value );
				}
			}
			Finish ();
		}

	}

}