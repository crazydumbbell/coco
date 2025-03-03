using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game load into action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Loads the specified identifier and outputs the value to the variable." )]
	public class SaveGameLoadInto : FsmStateAction
	{
		
		[CompoundArray ( "Count", "Identifier", "Value" )]
		
		/// <summary>
		/// The identifiers.
		/// </summary>
		[RequiredField]
		public FsmString [] identifiers;
		
		/// <summary>
		/// The values.
		/// </summary>
		[Tooltip ( "The object to output value" )]
		public FsmObject [] values;

		public override void Reset ()
		{
			identifiers = new FsmString[1];
			values = new FsmObject[1];
		}

		public override void OnEnter ()
		{
			for ( int i = 0; i < identifiers.Length; i++ )
			{
				if ( !identifiers [ i ].IsNone && !string.IsNullOrEmpty ( identifiers [ i ].Value ) )
				{
					if ( values [ i ].IsNone && values [ i ].Value != null )
					{
						values [ i ].RawValue = SaveGame.Load ( identifiers [ i ].Value, values [ i ].ObjectType );
					}
					else
					{
						SaveGame.LoadInto ( identifiers [ i ].Value, values [ i ].Value );
					}
				}
			}
			Finish ();
		}

	}

}