using System;
using System.IO;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game get directories action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Retrieves directories from the given folder and outputs the list to the variable." )]
	public class SaveGameGetDirectories : FsmStateAction
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
			DirectoryInfo [] directories = SaveGame.GetDirectories ( identifier.Value );
			value.stringValues = new string[directories.Length];
			for ( int i = 0; i < directories.Length; i++ )
			{
				value.stringValues [ i ] = directories [ i ].Name;
			}
			Finish ();
		}

	}

}