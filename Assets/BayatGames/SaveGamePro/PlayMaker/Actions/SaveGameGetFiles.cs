using System;
using System.Collections.Generic;
using System.IO;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game get files action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Retrieves files from the given folder and outputs the list to the variable." )]
	public class SaveGameGetFiles : FsmStateAction
	{

		/// <summary>
		/// The identifier.
		/// </summary>
		public FsmString identifier;
		
		/// <summary>
		/// The value.
		/// </summary>
		[ArrayEditor ( VariableType.String )]
		public FsmArray value;

		public override void OnEnter ()
		{
			FileInfo [] files = SaveGame.GetFiles ( identifier.Value );
			List<string> result = new List<string> ();
			for ( int i = 0; i < files.Length; i++ )
			{
				result.Add ( files [ i ].Name );
			}
			value.stringValues = result.ToArray ();
			value.SaveChanges ();
			Finish ();
		}

	}

}