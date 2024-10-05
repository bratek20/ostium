using System.Collections;
using System.Collections.Generic;
using B20.Frontend.UiElements;
using B20.Frontend.Windows.Api;
using UnityEngine;

namespace SomeNamespace
{
    public class EmptyModel
    {
    }

    public class EmptyViewModel : UiElement<EmptyModel>
    {

    }
    
    public class EmptyWindow: Window<EmptyWindowState> {} 
}
