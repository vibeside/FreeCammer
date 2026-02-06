using LethalCompanyInputUtils.Api;
using LethalCompanyInputUtils.BindingPathEnums;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

namespace FreeCammer
{
    internal class InputActionsClass : LcInputActions
    {
        [InputAction(KeyboardControl.Minus)]
        public InputAction ActivateCam { get; set; }
    }
}
