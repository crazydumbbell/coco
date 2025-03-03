using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game exists action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Checks whether the specified identifier exists or not." )]
	public class SaveGameExists : FsmStateAction
	{

		/// <summary>
		/// The identifiers.
		/// </summary>
		[RequiredField]
		public FsmString [] identifiers;
		
		/// <summary>
		/// The variable.
		/// </summary>
		[UIHint ( UIHint.Variable )]
		[Title ( "Store Result" )]
		public FsmBool variable;
		
		/// <summary>
		/// The true event.
		/// </summary>
		[Tooltip ( "Event to send if key exists." )]
		public FsmEvent trueEvent;
		
		/// <summary>
		/// The false event.
		/// </summary>
		[Tooltip ( "Event to send if key does not exist." )]
		public FsmEvent falseEvent;

		public override void Reset ()
		{
			identifiers = new FsmString[1];
		}

		public override void OnEnter ()
		{
			bool allIsTrue = true;
			for ( int i = 0; i < identifiers.Length; i++ )
			{
				if ( !identifiers [ i ].IsNone && !string.IsNullOrEmpty ( identifiers [ i ].Value ) )
				{
					allIsTrue &= SaveGame.Exists ( identifiers [ i ].Value );
				}
			}
			variable.Value = allIsTrue;
			Fsm.Event ( variable.Value ? trueEvent : falseEvent );
			Finish ();
		}

	}

}