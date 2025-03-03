using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game load globals action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Loads the global variables with their specified name." )]
	public class SaveGameLoadGlobals : FsmStateAction
	{

		public override void OnEnter ()
		{
			NamedVariable [] variables = FsmVariables.GlobalVariables.GetAllNamedVariables ();
			for ( int i = 0; i < variables.Length; i++ )
			{
				if ( !string.IsNullOrEmpty ( variables [ i ].Name ) )
				{
					if ( variables [ i ].TypeConstraint == VariableType.Object && variables [ i ].RawValue != null )
					{
						SaveGame.LoadInto ( variables [ i ].Name, variables [ i ].RawValue );
					}
					else
					{
						variables [ i ].RawValue = SaveGame.Load ( variables [ i ].Name, variables [ i ].ObjectType );
					}
				}
			}
			Finish ();
		}

	}

}