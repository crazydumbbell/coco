using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game move action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Moves the identifier to another identifier." )]
	public class SaveGameMove : FsmStateAction
	{
		
		[CompoundArray ( "Count", "From", "To" )]
		
		/// <summary>
		/// From identifiers.
		/// </summary>
		[RequiredField]
		[Tooltip ( "The identifier to move from." )]
		public FsmString [] fromIdentifiers;
		
		/// <summary>
		/// To identifiers.
		/// </summary>
		[RequiredField]
		[Tooltip ( "The identifier to paste on." )]
		public FsmString [] toIdentifiers;

		public override void Reset ()
		{
			fromIdentifiers = new FsmString[1];
			toIdentifiers = new FsmString[1];
		}

		public override void OnEnter ()
		{
			for ( int i = 0; i < fromIdentifiers.Length; i++ )
			{
				if ( !fromIdentifiers [ i ].IsNone && !string.IsNullOrEmpty ( fromIdentifiers [ i ].Value ) &&
				     !toIdentifiers [ i ].IsNone && !string.IsNullOrEmpty ( toIdentifiers [ i ].Value ) )
				{
					SaveGame.Move ( fromIdentifiers [ i ].Value, toIdentifiers [ i ].Value );
				}
			}
			Finish ();
		}

	}

}