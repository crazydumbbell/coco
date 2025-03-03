using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game save globals action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Saves the global variables with their specified name." )]
	public class SaveGameSaveGlobals : FsmStateAction
	{

		public override void OnEnter ()
		{
			NamedVariable [] variables = FsmVariables.GlobalVariables.GetAllNamedVariables ();
			for ( int i = 0; i < variables.Length; i++ )
			{
				if ( !string.IsNullOrEmpty ( variables [ i ].Name ) )
				{
					SaveGame.Save ( variables [ i ].Name, variables [ i ].RawValue );
				}
			}
			Finish ();
		}

	}

}