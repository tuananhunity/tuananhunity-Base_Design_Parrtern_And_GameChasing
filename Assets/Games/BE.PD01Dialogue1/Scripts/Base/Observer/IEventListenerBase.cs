using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Observer
{
    public interface IEventListenerBase 
    {
        
    }
    public interface EventListener<T> : IEventListenerBase
    {
        void OnMMEvent(T eventType);
    }
}
