﻿using doma;
using UnityEngine;

namespace Players{
	public class PlayerGuardControll : MonoBehaviour{
		public bool InGuard{ get; private set; }

		private MotionAnimControll motionAnimControll;

		private int checker;
		private int recorder;
		
		private void Start(){
			motionAnimControll = transform.GetComponentInChildren<MotionAnimControll>();
		}

		private void LateUpdate(){
			if (InGuard&&(++recorder != checker)){
				checker = 0;
				recorder = 0;
				InGuard = false;
				DebugLogger.Log("a");
			}
		}

		public void GuardCommand(){
			checker++;
			InGuard = true;
			motionAnimControll.ChangeAnim(motionAnimControll.MyDic.GuardName);
		}
	}
}