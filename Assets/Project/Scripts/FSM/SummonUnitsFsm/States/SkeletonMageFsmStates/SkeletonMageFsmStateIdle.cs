﻿using UnityEngine;

namespace Project.Scripts.States.SkeletonMageFsmStates
{
    public class SkeletonMageFsmStateIdle : SummonUnitFsmStateBase
    {
        public SkeletonMageFsmStateIdle(Animator animator, Fsm fsm) : base(animator, fsm)
        {
        }

        public override void Enter()
        {
            Animator.CrossFade("Idle", 0.25f);
        }
    }
}